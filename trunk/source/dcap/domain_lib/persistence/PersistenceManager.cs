using System;
using System.Collections.Generic;
using core_lib.common;
using domain_lib.dto;
using domain_lib.model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Expression;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace domain_lib.persistence
{
    /// <summary>
    /// Specifies whether to begin a new session, continue an existing session, or end an existing session.
    /// </summary>
    public enum SessionAction { Begin, Continue, End, BeginAndEnd }

    public class PersistenceManager : IDisposable
    {
        #region Declarations

        // Member variables
        private ISessionFactory m_SessionFactory = null;
        private ISession m_Session = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PersistenceManager()
        {
            this.ConfigureLog4Net();
            this.ConfigureNHibernate();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            m_SessionFactory.Dispose();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Close this Persistence Manager and release all resources (connection pools, etc). It is the responsibility of the application to ensure that there are no open Sessions before calling Close().
        /// </summary>
        public void Close()
        {
            m_SessionFactory.Close();
        }

        /// <summary>
        /// Deletes an object of a specified type.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <typeparam name="T">The type of object to delete.</typeparam>
        public void Delete<T>(T item)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    session.Delete(item);
                    session.Transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Deletes objects of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public void Delete<T>(IList<T> itemsToDelete)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                foreach (T item in itemsToDelete)
                {
                    using (session.BeginTransaction())
                    {
                        session.Delete(item);
                        session.Transaction.Commit();
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves all objects of a given type.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <returns>A list of all objects of the specified type.</returns>
        public IList<T> RetrieveAll<T>(SessionAction sessionAction)
        {
            /* Note that NHibernate guarantees that two object references will point to the
             * same object only if the references are set in the same session. For example,
             * Order #123 under the Customer object Able Inc and Order #123 in the Orders
             * list will point to the same object only if we load Customers and Orders in 
             * the same session. If we load them in different sessions, then changes that
             * we make to Able Inc's Order #123 will not be reflected in Order #123 in the
             * Orders list, since the references point to different objects. That's why we
             * maintain a session as a member variable, instead of as a local variable. */

            // Open a new session if specified
            if ((sessionAction == SessionAction.Begin) || (sessionAction == SessionAction.BeginAndEnd))
            {
                m_Session = m_SessionFactory.OpenSession();
            }

            // Retrieve all objects of the type passed in
            ICriteria targetObjects = m_Session.CreateCriteria(typeof(T));
            IList<T> itemList = targetObjects.List<T>();

            // Close the session if specified
            if ((sessionAction == SessionAction.End) || (sessionAction == SessionAction.BeginAndEnd))
            {
                m_Session.Close();
                m_Session.Dispose();
            }

            // Set return value
            return itemList;
        }

        /// <summary>
        /// Retrieves objects of a specified type where a specified property equals a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="propertyName">The name of the property to be tested.</param>
        /// <param name="propertyValue">The value that the named property must hold.</param>
        /// <returns>A list of all objects meeting the specified criteria.</returns>
        public IList<T> RetrieveEquals<T>(string propertyName, object propertyValue)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                // Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(T));
                criteria.Add(Expression.Eq(propertyName, propertyValue));

                // Get the matching objects
                IList<T> matchingObjects = criteria.List<T>();

                // Set return value
                return matchingObjects;
            }
        }

        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
        public void Save<T>(T item)
        {
            using (var session = m_SessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    session.SaveOrUpdate(item);
                    session.Transaction.Commit();
                }
            }
        }

        public UserDto checkUser(string userName, string password)
        {
            var userDto = new UserDto();
            if (String.IsNullOrEmpty(userName))
            {
                userDto.Message = "Chưa nhập tên đăng nhập. Vui lòng thử lại.";
                return userDto;
            }
            if (String.IsNullOrEmpty(password))
            {
                userDto.Message = "Chưa nhập mật khẩu. Vui lòng thử lại.";
                return userDto;
            }
            var users = RetrieveEquals<Users>("UserName", userName.ToUpper());
            if (users.Count == 0)
            {
                userDto.Message = "Người dùng chưa đăng ký. Vui lòng thử lại.";
                return userDto;
            }
            var user = users[0];
            if (string.Compare(MD5Util.EncodeMD5(password), user.Password, true) != 0)
            {
                userDto.Message = "Mật khẩu xác nhận không khớp. Vui lòng thử lại.";
                return userDto;
            }
            userDto.UserID = user.UserID;
            userDto.UserName = user.UserName;
            userDto.FullName = user.FullName;
            LoadUserInfo(userDto);
            LoadUserRole(userDto);
            return userDto;
        }

        private void LoadUserInfo(UserDto userDto)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select new MemberInfo(a.AccountNumber, m.HoTen, m.NgaySinh, m.SoCmnd, m.NgayCap, m.SoDienThoai, m.DiaChi, "
                    + " m.GioiTinh, m.SoTaiKhoan, m.ChiNhanhNH, m.ImageUrl, m.CreatedDate, m.CreatedBy) from MemberInfo m, Account a "
                    + " where m.MemberID = a.MemberId and a.UserId = :userId");
                query.SetParameter("userId", userDto.UserID);
                query.SetMaxResults(1);

                // Get the matching objects
                var memberInfo = (MemberInfo) query.UniqueResult();

                // Update userDto
                if (memberInfo != null)
                {
                    userDto.AccountNumber = memberInfo.AccountNumber;
                    userDto.FullName = memberInfo.HoTen;
                    userDto.NgaySinh = memberInfo.NgaySinh;
                    userDto.SoCmnd = memberInfo.SoCmnd;
                    userDto.NgayCap = memberInfo.NgayCap;
                    userDto.SoDienThoai = memberInfo.SoDienThoai;
                    userDto.DiaChi = memberInfo.DiaChi;
                    userDto.GioiTinh = memberInfo.GioiTinh;
                    userDto.SoTaiKhoan = memberInfo.SoTaiKhoan;
                    userDto.ChiNhanhNH = memberInfo.ChiNhanhNH;
                    userDto.ImageUrl = memberInfo.ImageUrl;
                    userDto.CreatedDate = memberInfo.CreatedDate;
                    userDto.CreatedBy = memberInfo.CreatedBy;
                }
            }
        }

        private void LoadUserRole(UserDto userDto)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select new Roles(r.RoleID, r.RoleCode, r.Description) from Roles r, UserRole ur "
                    + " where r.RoleID = ur.RoleID and r.Status = 1 and ur.IsActive = :status and ur.UserID = :userId");
                query.SetParameter("status", true);
                query.SetParameter("userId", userDto.UserID);

                // Get the matching objects
                var allRoleInfos = query.List();

                // Update Role info
                var listRoleDtos = new List<RoleDto>();
                foreach (Roles roleInfo in allRoleInfos)
                {

                    var roleDto = new RoleDto
                                      {
                                          RoleID = roleInfo.RoleID,
                                          RoleCode = roleInfo.RoleCode,
                                          Description = roleInfo.Description
                                      };
                    listRoleDtos.Add(roleDto);
                }
                userDto.AllRoles = listRoleDtos.ToArray();
            }
        }

        public string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return "-1";
            }
            if (String.IsNullOrEmpty(oldPassword))
            {
                return "-2";
            }
            var users = RetrieveEquals<Users>("UserName", userName.ToUpper());
            if (users.Count == 0)
            {
                return "-3";
            }
            var user = users[0];
            oldPassword = MD5Util.EncodeMD5(oldPassword);
            newPassword = MD5Util.EncodeMD5(newPassword);
            confirmPassword = MD5Util.EncodeMD5(confirmPassword);
            if (string.Compare(oldPassword, user.Password, true) != 0)
            {
                return "-4";
            }
            if (String.IsNullOrEmpty(newPassword))
            {
                return "-5";
            }
            if (string.Compare(oldPassword,newPassword, true) == 0)
            {
                return "-6";
            }
            if (String.IsNullOrEmpty(confirmPassword))
            {
                return "-7";
            }
            if (string.Compare(newPassword,confirmPassword,true) != 0)
            {
                return "-8";
            }

            // Update new password
            user.Password = newPassword;

            // Save user
            Save(user);
            return "0";
        }

        private long GetAccountIdBy(string userName)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select a.AccountId from Users u, Account a "
                    + " where u.UserID = a.UserId and u.UserName = :userName");
                query.SetParameter("userName", userName.ToUpper());

                // Get the matching objects
                var accountId = query.UniqueResult();

                // Set return value
                if (accountId == null)
                {
                    return -1;
                }
                return Convert.ToInt64(accountId);
            }
        }

        private int CountAccountByParentId(long parentAccountId)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(u.UserName) from Users u, Account a "
                    + " where u.UserID = a.UserId and a.ParentId = :parentAccountId ");
                query.SetParameter("parentAccountId", parentAccountId);

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);
            }
        }

        public string CreateUserAdmin(String userName, String fullName, String roleCode, String createdBy)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return "-1";
            }
            if (String.IsNullOrEmpty(fullName))
            {
                return "-1";
            }
            if (String.IsNullOrEmpty(roleCode))
            {
                return "-1";
            }
            var users = RetrieveEquals<Users>("UserName", userName.ToUpper());
            if (users.Count > 0)
            {
                return "-1";
            }
            var user = new Users
                           {
                               UserName = userName,
                               FullName = fullName,
                               Password = MD5Util.EncodeMD5(ConstUtil.DEFAULT_PASSWORD)
                           };
            Save(user);

            users = RetrieveEquals<Users>("UserName", userName.ToUpper());
            if (users.Count == 0)
            {
                return "-1";
            }
            user = users[0];

            var roles = RetrieveEquals<Roles>("RoleCode", roleCode.ToUpper());
            if (roles.Count == 0)
            {
                return "-1";
            }
            var role = roles[0];

            var userRole = new UserRole { UserID = user.UserID, RoleID = role.RoleID, IsActive = true };
            Save(userRole);

            return user.UserName;
        }

        public string UpdateUser(String userName, String fullName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return "-1";
            }
            if (String.IsNullOrEmpty(soCmnd))
            {
                return "-1";
            }
            if (default(DateTime).Equals(ngaySinh))
            {
                ngaySinh = null;
            }
            if (default(DateTime).Equals(ngayCap))
            {
                ngayCap = null;
            }
            var memberInfos = RetrieveEquals<MemberInfo>("SoCmnd", soCmnd);
            if (memberInfos.Count == 0)
            {
                return "-1";
            }
            var memberInfo = memberInfos[0];

            memberInfo.HoTen = fullName;
            memberInfo.NgaySinh = ngaySinh;
            memberInfo.SoCmnd = soCmnd;
            memberInfo.NgayCap = ngayCap;
            memberInfo.SoDienThoai = soDienThoai;
            memberInfo.DiaChi = diaChi;
            memberInfo.GioiTinh = gioiTinh;
            memberInfo.SoTaiKhoan = soTaiKhoan;
            memberInfo.ChiNhanhNH = chiNhanhNH;
            memberInfo.ImageUrl = photoUrl;
            Save(memberInfo);

            return "0";
        }

        public string CreateUser(String parentId, String directParentId, String userName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return "-1";
            }
            if (String.IsNullOrEmpty(soCmnd))
            {
                return "-1";
            }
            long parentIdVal = -1;
            long directParentIdVal = -1;
            if (!String.IsNullOrEmpty(parentId))
            {
                parentIdVal = GetAccountIdBy(parentId);
                if (parentIdVal == -1)
                {
                    return "-1";
                }
            }
            if (!String.IsNullOrEmpty(directParentId))
            {
                directParentIdVal = GetAccountIdBy(directParentId);
                if (directParentIdVal == -1)
                {
                    return "-1";
                }
            }
            if (!String.IsNullOrEmpty(parentId))
            {
                if (parentIdVal == -1 || (CountAccountByParentId(parentIdVal) > 3))
                {
                    return "-1";
                }
            }
            if (default(DateTime).Equals(ngaySinh))
            {
                ngaySinh = null;
            }
            if (default(DateTime).Equals(ngayCap))
            {
                ngayCap = null;
            }

            var memberInfos = RetrieveEquals<MemberInfo>("SoCmnd", soCmnd);
            if (memberInfos.Count > 0)
            {
                return CreateAccountForExistingMember(memberInfos[0], parentIdVal, directParentIdVal, photoUrl, createdBy);
            }
            return CreateAccountForNewMember(parentIdVal, directParentIdVal, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
        }

        private string CreateAccountForExistingMember(MemberInfo memberInfo, long parentId, long directParentId, string photoUrl, string createdBy)
        {
            var memberID = memberInfo.MemberID;
            var accountAmount = GetAccountAmountBy(memberID);
            var tenDangNhap = GetNextTenDangNhap(memberID, accountAmount);
            if (String.IsNullOrEmpty(tenDangNhap))
            {
                tenDangNhap = GetValidTenDangNhapBy(memberInfo.HoTen); 
            }
            memberInfo.ImageUrl = photoUrl;
            Save(memberInfo);

            var user = new Users { UserName = tenDangNhap, FullName = memberInfo.HoTen, Password = MD5Util.EncodeMD5(ConstUtil.DEFAULT_PASSWORD) };

            // Save user
            Save(user);

            var users = RetrieveEquals<Users>("UserName", tenDangNhap);
            if (users.Count == 0)
            {
                return "-1";
            }
            user = users[0];

            var roles = RetrieveEquals<Roles>("RoleCode", ConstUtil.QLTV);
            if (roles.Count == 0)
            {
                return "-1";
            }
            var role = roles[0];

            var userRole = new UserRole { UserID = user.UserID, RoleID = role.RoleID, IsActive = true };
            Save(userRole);

            var account = new Account();
            account.AccountNumber = GetNextAccountNumber();
            account.MemberId = memberID;
            account.ParentId = parentId;
            account.ParentId = directParentId;
            account.UserId = user.UserID;
            account.ChildIndex = accountAmount;
            account.IsActive = ConstUtil.ACTIVE_STATUS;
            account.CreatedBy = createdBy;
            account.CreatedDate = DateTime.Now;

            Save(account);

            return account.AccountNumber + "|" + user.UserName;
        }

        private long GetNextAccountNumber()
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select max(a.AccountNumber) from Account a ");

                // Get the matching objects
                var max = query.UniqueResult();

                // Set return value
                if (max == null)
                {
                    return 1;
                }
                return Convert.ToInt32(max) + 1;
            }
        }

        private string GetTenDangNhapByMemberId(long memberId)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select u.UserName from Users u, Account a "
                    + " where u.UserID = a.UserId and a.MemberId = :memberId order by a.ChildIndex asc");
                query.SetParameter("memberId", memberId);
                query.SetMaxResults(1);

                // Get the matching objects
                var tenDangNhap = (string)query.UniqueResult();

                // Set return value
                return tenDangNhap;
            }
        }

        private int GetAccountAmountBy(long memberId)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(a.MemberId) from Account a "
                    + " where a.MemberId = :memberId");
                query.SetParameter("memberId", memberId);

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);
            }
        }

        private string GetNextTenDangNhap(long memberId, int accountAmount)
        {
            var tenDangNhap = GetTenDangNhapByMemberId(memberId);
            if (String.IsNullOrEmpty(tenDangNhap))
            {
                return string.Empty;
            }
            if (char.IsNumber(tenDangNhap[tenDangNhap.Length-1]))
            {
                tenDangNhap = tenDangNhap.Substring(0, tenDangNhap.Length - 2);
            }
            return tenDangNhap + string.Format("{0:00}", accountAmount);
        }

        private int CountUserNameBy(string username)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(u.UserName) from Users u "
                    + " where u.UserName like :userName");
                query.SetParameter("userName", username + "%");

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);
            }
        }
        private string GetValidTenDangNhapBy(string fullname)
        {
            var tenKhongDau = VnStringHelper.toEnglish(fullname);
            var tenDangNhap = tenKhongDau.Replace(" ", "").ToUpper();

            var count = CountUserNameBy(tenDangNhap);
            if (count == 0)
            {
                return tenDangNhap;
            }
            char achar = 'A';
            char zchar = 'Z';
            for (int i = 0; i <= count/26; i++)
            {
                if (i < count / 26)
                {
                    tenDangNhap = tenDangNhap + zchar;
                }
                else
                {
                    tenDangNhap = tenDangNhap + (achar + (count % 26) - 1);
                }
            }
            while (true)
            {
                count = CountUserNameBy(tenDangNhap);
                if (count == 0)
                {
                    break;
                }
                while (count > 0 && tenDangNhap[tenDangNhap.Length - 1] != zchar)
                {
                    tenDangNhap = tenDangNhap.Substring(0, tenDangNhap.Length - 1) + (tenDangNhap[tenDangNhap.Length - 1] + 1);
                    count--;
                }
                if (count == 0)
                {
                    continue;
                }
                for (int i = 0; i <= count / 26; i++)
                {
                    if (i < count / 26)
                    {
                        tenDangNhap = tenDangNhap + zchar;
                    }
                    else
                    {
                        tenDangNhap = tenDangNhap + (achar + (count % 26) - 1);
                    }
                }
            }
            return tenDangNhap;
        }

        private string CreateAccountForNewMember(long parentId, long directParentId, String userName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            var tenDangNhap = GetValidTenDangNhapBy(userName);

            // Init member object
            var memberInfo = new MemberInfo
                                 {
                                     HoTen = userName,
                                     NgaySinh = ngaySinh,
                                     SoCmnd = soCmnd,
                                     NgayCap = ngayCap,
                                     SoDienThoai = soDienThoai,
                                     DiaChi = diaChi,
                                     GioiTinh = gioiTinh,
                                     SoTaiKhoan = soTaiKhoan,
                                     ChiNhanhNH = chiNhanhNH,
                                     ImageUrl = photoUrl,
                                     CreatedDate = DateTime.Now,
                                     CreatedBy = createdBy
                                 };
            // Save memberInfo
            Save(memberInfo);

            var memberInfos = RetrieveEquals<MemberInfo>("SoCmnd", soCmnd);
            if (memberInfos.Count == 0)
            {
                return "-1";
            }
            memberInfo = memberInfos[0];


            var user = new Users { UserName = tenDangNhap, FullName = userName, Password = MD5Util.EncodeMD5(ConstUtil.DEFAULT_PASSWORD) };

            // Save user
            Save(user);

            var users = RetrieveEquals<Users>("UserName", tenDangNhap);
            if (users.Count == 0)
            {
                return "-1";
            }
            user = users[0];

            var roles = RetrieveEquals<Roles>("RoleCode", ConstUtil.QLTV);
            if (roles.Count == 0)
            {
                return "-1";
            }
            var role = roles[0];

            var userRole = new UserRole { UserID = user.UserID, RoleID = role.RoleID, IsActive = true };
            Save(userRole);

            var account = new Account();
            account.AccountNumber = GetNextAccountNumber();
            account.MemberId = memberInfo.MemberID;
            account.ParentId = parentId;
            account.ParentId = directParentId;
            account.UserId = user.UserID;
            account.ChildIndex = 0;
            account.IsActive = ConstUtil.ACTIVE_STATUS;
            account.CreatedBy = createdBy;
            account.CreatedDate = DateTime.Now;

            Save(account);

            return account.AccountNumber + "|" + user.UserName;
        }

        public string SearchUser(String parentId, String directParentId, String userName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return "-1";
            }
            if (String.IsNullOrEmpty(soCmnd))
            {
                return "-1";
            }

            var allResults = string.Empty;

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select a.AccountNumber, u.UserName from MemberInfo m, Users u, Account a "
                    + " where m.MemberID = a.MemberId and u.UserID = a.UserId and m.SoCmnd = :soCmnd");
                query.SetParameter("soCmnd", soCmnd);

                // Get the matching objects
                var list = query.List();

                foreach (var row in list)
                {
                    var values = (Object[]) row;
                    var accountNumber = values[0];
                    var tenDangNhap = values[1];
                    if (String.IsNullOrEmpty(allResults))
                    {
                        allResults = accountNumber + "|" + tenDangNhap;   
                    }
                    else
                    {
                        allResults = allResults + ";" + accountNumber + "|" + tenDangNhap;
                    }
                }
            }

            // Set return value
            return allResults;
        }

				public AccountBonus SaveAccountBonus(long accountId, double bonusAmount, string bonusType)
        {
            DateTime now = DateTime.Now;
            var bonus = new AccountBonus
                            {
                                AccountId = accountId,
                                BonusAmount = bonusAmount,
                                BonusType = bonusType,
                                Month = now.ToString("yyyyMM"),
                                CreatedDate = now
                            };

            Save(bonus);
            return bonus;
        }
        
        public ManagerL1 InsertQl1Tree(long accountId)
        {
            throw new NotImplementedException();
        }

        public ManagerL2 InsertQl2Tree(long calcAccountId)
        {
            throw new NotImplementedException();
        }
				
        public IList<AccountPreCalc> GetPreCalcQueue()
        {
            return RetrieveEquals<AccountPreCalc>("IsCalculated", "N");
        }

        public long CountUpLevel(long calcAccountId, int accountLevel)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {

                var query = session.CreateQuery("select count(*) from AccountPreCalc a "
                   + " where a.CalcAccountId = :CalcAccountId and Account_Level < :AccountLevel");
                query.SetParameter("CalcAccountId", calcAccountId);
                query.SetParameter("AccountLevel", accountLevel);

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);

                /*// Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(AccountPreCalc));
                criteria.Add(Expression.Eq("CalcAccountId", calcAccountId));
                criteria.Add(Expression.Lt("AccountLevel", accountLevel));

                var count = criteria.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);*/
            }
        }

        public int CountLeft(long calcAccountId, int accountLevel, long levelIndex)
        {
            
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(*) from AccountPreCalc a "
                   + " where a.CalcAccountId = :CalcAccountId and Account_Level = :AccountLevel and Level_Index < :LevelIndex");
                query.SetParameter("CalcAccountId", calcAccountId);
                query.SetParameter("AccountLevel", accountLevel);
                query.SetParameter("LevelIndex", levelIndex);

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);

                /*// Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(AccountPreCalc));
                criteria.Add(Expression.Eq("CalcAccountId", calcAccountId));
                criteria.Add(Expression.Eq("AccountLevel", accountLevel));
                criteria.Add(Expression.Lt("LevelIndex", levelIndex));

                var count = criteria.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);*/

            }
        }

        public AccountPreCalc GetLatestPreCalcQueue(long calcAccountId)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {

                // Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(AccountPreCalc));
                criteria.Add(Expression.Eq("CalcAccountId", calcAccountId));
                criteria.AddOrder(Order.Desc("AccountLevel")).AddOrder(Order.Desc("LevelIndex"));
                criteria.SetMaxResults(1);

                var all = criteria.List<AccountPreCalc>();
                if (all == null || all.Count == 0)
                    return null;
                // Set return value
                return all[0];
            }
        }

        public Object GetQlLatestChild<T>()
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {

                // Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(T));
                criteria.AddOrder(Order.Desc("Level")).AddOrder(Order.Desc("LevelIndex"));
                criteria.SetMaxResults(1);

                var all = criteria.List<T>();
                if (all == null || all.Count == 0)
                    return null;
                // Set return value
                return all[0];
            }
        }

        public Object FindQlByLocation<T>(long level, long levelIndex)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                // Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(T));
                criteria.Add(Expression.Eq("Level", level));
                criteria.Add(Expression.Eq("LevelIndex", levelIndex));

                var all = criteria.List<T>();
                if (all == null || all.Count == 0)
                    return null;
                // Set return value
                return all[0];
            }
        }
				
        public IList<AccountLog> GetAccountLog()
        {
            return RetrieveAll<AccountLog>(SessionAction.BeginAndEnd);
        }

        public Account GetAccount(long accountId)
        {
            IList<Account> retrieveEquals = RetrieveEquals<Account>("AccountId", accountId);
            if (retrieveEquals == null || retrieveEquals.Count == 0)
                return null;
            return retrieveEquals[0];
        }

        public ManagerL1 GetManagerL1(long accountId)
        {
            IList<ManagerL1> retrieveEquals = RetrieveEquals<ManagerL1>("AccountId", accountId);
            if (retrieveEquals == null || retrieveEquals.Count == 0)
                return null;
            return retrieveEquals[0];
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Configures Log4Net to work with NHibernate.
        /// </summary>
        private void ConfigureLog4Net()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Configures NHibernate and creates a member-level session factory.
        /// </summary>
        private void ConfigureNHibernate()
        {
            // Initialize
            var cfg = new Configuration();
            cfg.Configure();

            /* Note: The AddAssembly() method requires that mappings be 
             * contained in hbm.xml files whose BuildAction properties 
             * are set to ‘Embedded Resource’. */

            // Create session factory from configuration object
            m_SessionFactory = cfg.BuildSessionFactory();
        }

        #endregion
    }
}