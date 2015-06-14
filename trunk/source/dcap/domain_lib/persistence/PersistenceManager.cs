using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using core_lib.common;
using domain_lib.dto;
using domain_lib.model;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Expression;
using NHibernate.Transform;

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
        /// <param name="mapParams">The map of the property to be tested.</param>
        /// <returns>A list of all objects meeting the specified criteria.</returns>
        public IList<T> RetrieveEquals<T>(Hashtable mapParams)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                // Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(T));
                foreach (DictionaryEntry entry in mapParams)
                {
                    criteria.Add(Expression.Eq(entry.Key.ToString(), entry.Value));
                }

                // Get the matching objects
                IList<T> matchingObjects = criteria.List<T>();

                // Set return value
                return matchingObjects;
            }
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
        /// Retrieves objects of a specified type where a specified property equals a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="propertyName">The name of the property to be tested.</param>
        /// <param name="propertyValue">The value that the named property must hold.</param>
        /// <returns>A list of all objects meeting the specified criteria.</returns>
        public IList<T> RetrieveEqualsWithOrder<T>(string propertyName, object propertyValue, string orderField, string orderType)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                // Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(T));
                criteria.Add(Expression.Eq(propertyName, propertyValue));
                if (string.Compare(orderType, "ASC", true) == 0)
                {
                    criteria.AddOrder(Order.Asc(orderField));
                }
                else
                {
                    criteria.AddOrder(Order.Desc(orderField));
                }

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

        /// <summary>
        /// Saves an new object and its persistent children.
        /// </summary>
        public void SaveNew<T>(T item)
        {
            using (var session = m_SessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    session.Save(item);
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
                userDto.Message = "Mật khẩu không khớp. Vui lòng thử lại.";
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
                var memberInfo = (MemberInfo)query.UniqueResult();

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
            if (string.Compare(oldPassword, newPassword, true) == 0)
            {
                return "-6";
            }
            if (String.IsNullOrEmpty(confirmPassword))
            {
                return "-7";
            }
            if (string.Compare(newPassword, confirmPassword, true) != 0)
            {
                return "-8";
            }

            // Update new password
            user.Password = newPassword;

            // Save user
            Save(user);
            return "0";
        }

        private long GetAccountIdBy(string accountNumber)
        {
            long accountNumberVal;
            if (!long.TryParse(accountNumber, out accountNumberVal))
            {
                return -1;
            }
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select a.AccountId from Account a "
                    + " where a.AccountNumber = :accountNumber");
                query.SetParameter("accountNumber", accountNumberVal);

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

		private long GetAccountIdByUserName(string userName)
		{
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select a.AccountId from Account a, Users u "
                    + " where a.UserId = u.UserID and u.UserName = :userName");
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
		
        private long GetAccountIdBy(string modelName, string accountNumber)
        {
            long accountNumberVal;
            if (!long.TryParse(accountNumber, out accountNumberVal))
            {
                return -1;
            }
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select a.AccountId from Account a, " + modelName + " b "
                    + " where a.AccountId = b.AccountId and a.AccountNumber = :accountNumber");
                query.SetParameter("accountNumber", accountNumberVal);

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

        private Hashtable GetMapAccountIdBy(string[] userNames)
        {
            var result = new Hashtable();
            if (userNames.Length == 0)
            {
                return result;
            }
            var strSql = "select u.UserName, a.AccountId from Account a, Users u "
                         + " where a.UserId = u.UserID and u.UserName in (";
            var mapParams = new Hashtable();
            for (int i = 0; i < userNames.Length; i++)
            {
                if (i > 0)
                {
                    strSql += ",";
                }
                var paramKey = "userName" + (i + 1);
                strSql += ":" + paramKey;
                mapParams.Add(paramKey, userNames[i]);
            }
            strSql += ")";
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(strSql);
                foreach (DictionaryEntry param in mapParams)
                {
                    query.SetParameter(param.Key.ToString(), param.Value);
                }

                // Get the matching objects
                var list = query.List();

                foreach (object[] aRow in list)
                {
                    var userName = aRow[0].ToString();
                    var accountId = long.Parse(aRow[1].ToString());
                    result.Add(userName, accountId);
                }

                // Return
                return result;
            }
        }

        private int CountAccountByParentId(long parentAccountId)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(a.AccountId) from Account a "
                    + " where a.ParentId = :parentAccountId ");
                query.SetParameter("parentAccountId", parentAccountId);

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);
            }
        }

        private int CountAccountByParentId(string modelName, long parentAccountId)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(a.AccountId) from " + modelName + " a "
                    + " where a.ParentId = :parentAccountId ");
                query.SetParameter("parentAccountId", parentAccountId);

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);
            }
        }

        private int CountAccountBySoCmnd(string soCmnd)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(a.AccountId) from MemberInfo m, Account a "
                    + " where m.MemberID = a.MemberId and m.SoCmnd = :soCmnd ");
                query.SetParameter("soCmnd", soCmnd);

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
                return "-2";
            }
            if (String.IsNullOrEmpty(roleCode))
            {
                return "-3";
            }
            var users = RetrieveEquals<Users>("UserName", userName.ToUpper());
            if (users.Count > 0)
            {
                return "-4";
            }
            var user = new Users
                           {
                               UserName = userName,
                               FullName = fullName,
                               Password = MD5Util.EncodeMD5(ConstUtil.DEFAULT_PASSWORD)
                           };
            SaveNew(user);

            users = RetrieveEquals<Users>("UserName", userName.ToUpper());
            if (users.Count == 0)
            {
                return "-5";
            }
            user = users[0];

            var roles = RetrieveEquals<Roles>("RoleCode", roleCode.ToUpper());
            if (roles.Count == 0)
            {
                return "-6";
            }
            var role = roles[0];

            var userRole = new UserRole { UserID = user.UserID, RoleID = role.RoleID, IsActive = true };
            SaveNew(userRole);

            return user.UserName;
        }

        public string UpdateUser(String userName, String fullName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return "-1";
            }
            if (String.IsNullOrEmpty(soCmnd))
            {
                return "-2";
            }
            var dateNgaySinh = DateUtil.GetDateTime(ngaySinh);
            var dateNgayCap = DateUtil.GetDateTime(ngayCap);
            var memberInfos = RetrieveEquals<MemberInfo>("SoCmnd", soCmnd);
            if (memberInfos.Count == 0)
            {
                return "-3";
            }
            var memberInfo = memberInfos[0];

            memberInfo.HoTen = fullName;
            memberInfo.HoTenKd = VnStringHelper.toEnglish(fullName);
            memberInfo.NgaySinh = dateNgaySinh;
            memberInfo.SoCmnd = soCmnd;
            memberInfo.NgayCap = dateNgayCap;
            memberInfo.SoDienThoai = soDienThoai;
            memberInfo.DiaChi = diaChi;
            memberInfo.GioiTinh = gioiTinh;
            memberInfo.SoTaiKhoan = soTaiKhoan;
            memberInfo.ChiNhanhNH = chiNhanhNH;
            memberInfo.ImageUrl = photoUrl;
            Save(memberInfo);

            return "0";
        }

        public string CreateUser(String parentId, String directParentId, String userName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return "-1";
            }
            if (String.IsNullOrEmpty(soCmnd))
            {
                return "-2";
            }
            long parentIdVal = -1;
            long directParentIdVal = -1;
            if (!String.IsNullOrEmpty(parentId))
            {
                parentIdVal = GetAccountIdBy(parentId);
                if (parentIdVal == -1)
                {
                    return "-3";
                }
            }
            if (!String.IsNullOrEmpty(directParentId))
            {
                directParentIdVal = GetAccountIdBy(directParentId);
                if (directParentIdVal == -1)
                {
                    return "-4";
                }
            }
            if (!String.IsNullOrEmpty(parentId))
            {
                if (parentIdVal == -1 || (CountAccountByParentId(parentIdVal) >= 3))
                {
                    return "-5";
                }
            }
            if (!String.IsNullOrEmpty(soCmnd) && CountAccountBySoCmnd(soCmnd) >= 40)
            {
                return "-9";
            }
            var dateNgaySinh = DateUtil.GetDateTime(ngaySinh);
            var dateNgayCap = DateUtil.GetDateTime(ngayCap);

            var memberInfos = RetrieveEquals<MemberInfo>("SoCmnd", soCmnd);
            if (memberInfos.Count > 0)
            {
                return CreateAccountForExistingMember(memberInfos[0], parentIdVal, directParentIdVal, photoUrl, createdBy);
            }
            return CreateAccountForNewMember(parentIdVal, directParentIdVal, userName, dateNgaySinh, soCmnd, dateNgayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
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
            SaveNew(user);

            var users = RetrieveEquals<Users>("UserName", tenDangNhap);
            if (users.Count == 0)
            {
                return "-7";
            }
            user = users[0];

            var roles = RetrieveEquals<Roles>("RoleCode", ConstUtil.QLTV);
            if (roles.Count == 0)
            {
                return "-8";
            }
            var role = roles[0];

            var userRole = new UserRole { UserID = user.UserID, RoleID = role.RoleID, IsActive = true };
            SaveNew(userRole);

            var account = new Account();
            account.AccountNumber = GetNextAccountNumber();
            account.MemberId = memberID;
            account.ParentId = parentId;
            account.ParentDirectId = directParentId;
            account.UserId = user.UserID;
            var childIndex = GetChildIndexBy(parentId);
            account.ChildIndex = childIndex;
            account.IsActive = ConstUtil.ACTIVE_STATUS;
            account.CreatedBy = createdBy;
            account.CreatedDate = DateTime.Now;

            SaveNew(account);

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

        private int GetChildIndexBy(long parentId)
        {
            if (parentId == -1)
            {
                return 0;
            }
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(a.ParentId) from Account a "
                    + " where a.ParentId = :parentId");
                query.SetParameter("parentId", parentId);

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count)+1;
            }
        }

        private string GetNextTenDangNhap(long memberId, int accountAmount)
        {
            var tenDangNhap = GetTenDangNhapByMemberId(memberId);
            if (String.IsNullOrEmpty(tenDangNhap))
            {
                return string.Empty;
            }
            int index = tenDangNhap.Length - 1;
            while (char.IsNumber(tenDangNhap[index]))
            {
                index--;
            }
            return tenDangNhap.Substring(0, index + 1) + accountAmount;
        }

        private IList<string> GetAllUserNameBy(string username)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select u.UserName from Users u "
                    + " where u.UserName = :userName1 or u.UserName like :userName2");
                query.SetParameter("userName1", username);
                query.SetParameter("userName2", username + "[_]%[^0-9]");

                // Get the matching objects
                var list = query.List<string>();

                // Set return value
                return list;
            }
        }
        private string GetValidTenDangNhapBy(string fullname)
        {
            var tenKhongDau = VnStringHelper.toEnglish(fullname);
            var tenDangNhap = tenKhongDau.Replace(" ", "").ToUpper();

            var list = GetAllUserNameBy(tenDangNhap);
            if (list.Count == 0)
            {
                return tenDangNhap;
            }
            char achar = 'A';
            char zchar = 'Z';
            var latestUserName = tenDangNhap;
            foreach (var oneName in list)
            {
                if (!Char.IsNumber(oneName[oneName.Length - 1]) && (latestUserName.Length < oneName.Length
                    || (latestUserName.Length == oneName.Length && string.Compare(latestUserName, oneName) < 0)))
                {
                    latestUserName = oneName;
                }
            }
            if (string.Compare(tenDangNhap, latestUserName) == 0)
            {
                tenDangNhap = tenDangNhap + "_" + Char.ToString(achar);
            }
            else
            {
                if (latestUserName[latestUserName.Length-1] == zchar)
                {
                    tenDangNhap = latestUserName + Char.ToString(achar);
                }
                else
                {
                    tenDangNhap = latestUserName.Substring(0, latestUserName.Length - 1)
                                  + Char.ToString((char)(latestUserName[latestUserName.Length - 1] + 1));
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
                                     HoTenKd = VnStringHelper.toEnglish(userName),
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
            SaveNew(memberInfo);

            var memberInfos = RetrieveEquals<MemberInfo>("SoCmnd", soCmnd);
            if (memberInfos.Count == 0)
            {
                return "-6";
            }
            memberInfo = memberInfos[0];


            var user = new Users { UserName = tenDangNhap, FullName = userName, Password = MD5Util.EncodeMD5(ConstUtil.DEFAULT_PASSWORD) };

            // Save user
            SaveNew(user);

            var users = RetrieveEquals<Users>("UserName", tenDangNhap);
            if (users.Count == 0)
            {
                return "-7";
            }
            user = users[0];

            var roles = RetrieveEquals<Roles>("RoleCode", ConstUtil.QLTV);
            if (roles.Count == 0)
            {
                return "-8";
            }
            var role = roles[0];

            var userRole = new UserRole { UserID = user.UserID, RoleID = role.RoleID, IsActive = true };
            SaveNew(userRole);

            var account = new Account();
            account.AccountNumber = GetNextAccountNumber();
            account.MemberId = memberInfo.MemberID;
            account.ParentId = parentId;
            account.ParentDirectId = directParentId;
            account.UserId = user.UserID;
            var childIndex = GetChildIndexBy(parentId);
            account.ChildIndex = childIndex;
            account.IsActive = ConstUtil.ACTIVE_STATUS;
            account.CreatedBy = createdBy;
            account.CreatedDate = DateTime.Now;

            SaveNew(account);

            return account.AccountNumber + "|" + user.UserName;
        }

        public string SearchUser(String parentId, String directParentId, String userName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
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
                    var values = (Object[])row;
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

        public BangKeDto[] SearchBangKe(DateTime? thangKeKhai)
        {
            var strThang = DateUtil.GetDateTimeAsStringWithEnProvider(thangKeKhai, ConstUtil.MONTH_FORMAT);

            List<BangKeDto> allResults;

            using (ISession session = m_SessionFactory.OpenSession())
            {
                // Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(BangKeVW));
                criteria.Add(Expression.Eq("Thang", strThang));
                criteria.AddOrder(Order.Desc("SoTien"));

                // Get the matching objects
                var list = criteria.List<BangKeVW>();

                // Set return value
                allResults = CreateAllBangKeDto(list);
            }
            return allResults.ToArray();
        }

        public BangKeDto[] SearchBangKeExt(string accountNumber, string userName, DateTime? beginDate, DateTime? endDate)
        {
            List<BangKeDto> allResults;

            long accountNumberVal;
            if (!long.TryParse(accountNumber, out accountNumberVal))
            {
                accountNumberVal = -1;
            }
			if (accountNumberVal == -1 && !string.IsNullOrEmpty(userName))
			{
				accountNumberVal = GetAccountIdByUserName(userName);
                if (accountNumberVal == -1)
                {
                    return new BangKeDto[0];
                }
			}
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.GetNamedQuery("GetBangKeAdvance");
                query.SetParameter("pAccountNumber", accountNumberVal);
                query.SetParameter("pStart", beginDate);
                query.SetParameter("pEnd", endDate);
                // Get the matching objects
                var list = query.List<BangKeVW>();
                
                // Set return value
                allResults = CreateAllBangKeDto(list);
            }
            return allResults.ToArray();
        }

        private List<BangKeDto> CreateAllBangKeDto(IEnumerable<BangKeVW> allBangKeVw)
        {
            var allBangKeDto = new List<BangKeDto>();
            var stt = 0;
            foreach (var row in allBangKeVw)
            {
                stt++;
                var hoTen = row.HoTen;
                var userName = row.UserName;
                var maGioiTinh = row.GioiTinh;
                var soCmnd = row.SoCmnd;
                var ngayCap = row.NgayCap;
                var diaChi = row.DiaChi;
                var soTaiKhoan = row.SoTaiKhoan;
                var chiNhanhNH = row.ChiNhanhNH;
                var soDienThoai = row.SoDienThoai;
                var ngayDangKy = row.NgayDangKy;
                var soTien = row.SoTien;
                var thang = row.Thang;
                var heThong = row.HeThong;
                var quanLy = row.QuanLy;
                var thuongThem = row.ThuongThem;

                var bangKeDto = new BangKeDto();
                bangKeDto.STT = stt;
                bangKeDto.HoTen = hoTen;
                bangKeDto.UserName = userName;
                bangKeDto.GioiTinh = GioiTinhUtil.DecodeGioitinh(maGioiTinh);
                bangKeDto.SoCmnd = soCmnd;
                bangKeDto.NgayCap = DateUtil.GetDateTimeAsDdmmyyyy(ngayCap);
                bangKeDto.DiaChi = diaChi;
                bangKeDto.SoTaiKhoan = soTaiKhoan;
                bangKeDto.ChiNhanhNH = chiNhanhNH;
                bangKeDto.SoDienThoai = soDienThoai;
                bangKeDto.NgayDangKy = DateUtil.GetDateTimeAsDdmmyyyy(ngayDangKy);
                bangKeDto.SoTien = soTien;
                bangKeDto.Thang = thang.Substring(4, 2) + "/" + thang.Substring(0, 4);
                bangKeDto.HeThong = heThong;
                bangKeDto.QuanLy = quanLy;
                bangKeDto.ThuongThem = thuongThem;

                allBangKeDto.Add(bangKeDto);
            }
            UpdatePaidStatus(allBangKeDto);
            return allBangKeDto;
        }

        private void UpdatePaidStatus(List<BangKeDto> allResults)
        {
            var userNames = new List<string>();
            foreach (var bangKeDto in allResults)
            {
                userNames.Add(bangKeDto.UserName);
            }
            var accountIds = GetMapAccountIdBy(userNames.ToArray());
            foreach (BangKeDto bangKeDto in allResults)
            {
                var mapParams = new Hashtable();
                mapParams.Add("AccountId", accountIds[bangKeDto.UserName]);
                var month = DateUtil.GetDateTimeAsStringWithEnProvider(DateUtil.GetDateTime(bangKeDto.Thang),
                                                                       ConstUtil.MONTH_FORMAT);
                mapParams.Add("Month", month);
                bangKeDto.IsPaid = IsAccountPaidBy(mapParams);
            }
        }

        private long IsAccountPaidBy(Hashtable mapParams)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(*) from AccountBonus a "
                   + " where a.AccountId = :AccountId and a.Month = :Month and a.IsPaid = :IsPaid");
                query.SetParameter("AccountId", mapParams["AccountId"]);
                query.SetParameter("Month", mapParams["Month"]);
                query.SetParameter("IsPaid", 1);

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count) > 0 ? 1 : -1;
            }
        }

        public HoaHongMemberDto[] SearchBangKeHoaHong(long accountNumber, DateTime? thangKeKhai)
        {
            var strThang = DateUtil.GetDateTimeAsStringWithEnProvider(thangKeKhai, ConstUtil.MONTH_FORMAT);
            var accountId = GetAccountIdBy(accountNumber.ToString());

            var allResults = new List<HoaHongMemberDto>();

            using (ISession session = m_SessionFactory.OpenSession())
            {
                // Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(HoaHongMemberVW));
                criteria.Add(Expression.Eq("Thang", strThang));
                criteria.Add(Expression.Eq("AccountId", accountId));

                // Get the matching objects
                IList<HoaHongMemberVW> list = criteria.List<HoaHongMemberVW>();
                
                // Set return value
                double trucTiep = 0;
                double canCap = 0;
                double heThong = 0;
                double quanLy = 0;
                double thuongThem = 0;
                double tong;
                foreach (var row in list)
                {
                    if (string.Compare(row.BonusType, "TT", true) == 0)
                    {
                        trucTiep += row.Tong;
                    }
                    if (string.Compare(row.BonusType, "CC", true) == 0)
                    {
                        canCap += row.Tong;
                    }
                    if (string.Compare(row.BonusType, "HT", true) == 0)
                    {
                        heThong += row.Tong;
                    }
                    if ((string.Compare(row.BonusType, "CC1", true) == 0)
                        || (string.Compare(row.BonusType, "QL1", true) == 0))
                    {
                        quanLy += row.Tong;
                    }
                    if (string.Compare(row.BonusType, "ADD", true) == 0)
                    {
                        thuongThem += row.Tong;
                    }
                }
                tong = trucTiep + canCap + heThong + quanLy + thuongThem;
                if (tong != 0)
                {
                    var dto = new HoaHongMemberDto();
                    dto.STT = 1;
                    dto.AccountId = accountId;
                    dto.Thang = DateUtil.GetDateTimeAsStringWithEnProvider(thangKeKhai, "MM/yyyy");
                    dto.TrucTiep = trucTiep;
                    dto.CanCap = canCap;
                    dto.HeThong = heThong;
                    dto.QuanLy = quanLy;
                    dto.ThuongThem = thuongThem;
                    dto.Tong = tong;
                    allResults.Add(dto);
                }
            }

            return allResults.ToArray();
        }

        private long GetAccountNumberBy(long accountId)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select a.AccountNumber from Account a "
                    + " where a.AccountId = :accountId");
                query.SetParameter("accountId", accountId);

                // Get the matching objects
                var accountNumber = query.UniqueResult();

                // Set return value
                if (accountNumber == null)
                {
                    return -1;
                }
                return Convert.ToInt64(accountNumber);
            }
        }

        public UserDto[] SearchUserInfo(string soCmnd, string idThanhVien, string hoTen)
        {
            var sqlStr = "select new MemberInfo(a.AccountNumber, m.HoTen, u.UserName, a.ParentId, a.ParentDirectId, m.NgaySinh, m.SoCmnd, m.NgayCap, m.SoDienThoai, m.DiaChi, "
                    + " m.GioiTinh, m.SoTaiKhoan, m.ChiNhanhNH, m.ImageUrl, m.CreatedDate, m.CreatedBy) from MemberInfo m, Account a, Users u "+
                    " where m.MemberID = a.MemberId and a.UserId = u.UserID";
            var sqlParams = new Hashtable();
            if (!string.IsNullOrEmpty(soCmnd))
            {
                sqlStr += " and m.SoCmnd = :soCmnd";
                sqlParams.Add("soCmnd", soCmnd);
            }
            if (!string.IsNullOrEmpty(idThanhVien))
            {
                long idThanhVienVal;
                var status = long.TryParse(idThanhVien, out idThanhVienVal);
                if (status)
                {
                    sqlStr += " and a.AccountNumber = :accountNumber";
                    sqlParams.Add("accountNumber", idThanhVienVal);
                }
                else
                {
                    sqlStr += " and 1=0";
                }
            }
            if (!string.IsNullOrEmpty(hoTen))
            {
                sqlStr += " and m.HoTenKd like :hoTenKd";
                sqlParams.Add("hoTenKd", "%" + VnStringHelper.normalizeTen(hoTen, true).Replace(" ", "%") + "%");
            }

            var allUserDto = new List<UserDto>();
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);
                foreach (var key in sqlParams.Keys)
                {
                    query.SetParameter(key.ToString(), sqlParams[key]);
                }

                // Get the matching objects
                var list = query.List();

                foreach (var oneRow in list)
                {
                    var memberInfo = oneRow as MemberInfo;
                    if (memberInfo == null)
                    {
                        continue;
                    }
                    var userDto = new UserDto();
                    userDto.AccountNumber = memberInfo.AccountNumber;
                    userDto.FullName = memberInfo.HoTen;
                    userDto.UserName = memberInfo.UserName;
                    userDto.ParentId = GetAccountNumberBy(memberInfo.ParentId);
                    userDto.ParentDirectId = GetAccountNumberBy(memberInfo.ParentDirectId);
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
                    allUserDto.Add(userDto);
                }
            }

            return allUserDto.ToArray();
        }

        public MemberNodeDto[] SearchManagerNodeDto(string capQuanLy, string accountNumber)
        {
            var modelName = GetManagerModelName(capQuanLy);
            var memberNodeDtos = new List<MemberNodeDto>();
            var rootId = GetAccountIdBy(modelName, accountNumber);
            var childNumber = CountAccountByParentId(modelName, rootId);
            if (childNumber == 0)
            {
                MemberNodeDto rootDto = GetNodeDto(modelName, accountNumber);
                if (rootDto == null)
                {
                    return memberNodeDtos.ToArray();
                }
                memberNodeDtos.Add(rootDto);
            }
            else
            {
                var mapDtoNode = GetMapManagerNodeDto(modelName);
                Hashtable mapParentNode = GetMapParentNode(mapDtoNode);
                memberNodeDtos = GetMemberNodeDtoLoop(rootId, mapDtoNode, mapParentNode);
            }

            return memberNodeDtos.ToArray();
        }

        public string GetManagerModelName(string capQuanLy)
        {
            int capQuanLyVal;
            var code = int.TryParse(capQuanLy, out capQuanLyVal);
            if (!code)
            {
                return string.Empty;
            }
            return "ManagerL" + capQuanLyVal;
        }

        public MemberNodeDto[] SearchMemberNodeDto(string accountNumber)
        {
            var memberNodeDtos = new List<MemberNodeDto>();
            var rootId = GetAccountIdBy(accountNumber);
            var childNumber = CountAccountByParentId(rootId);
            if (childNumber == 0)
            {
                MemberNodeDto rootDto = GetNodeDto(accountNumber);
                if (rootDto == null)
                {
                    return memberNodeDtos.ToArray();
                }
                memberNodeDtos.Add(rootDto);
            }
            else
            {
                var mapDtoNode = GetMapMemberNodeDto();
                Hashtable mapParentNode = GetMapParentNode(mapDtoNode);
                memberNodeDtos = GetMemberNodeDtoLoop(rootId, mapDtoNode, mapParentNode);
            }

            return memberNodeDtos.ToArray();
        }

        private Hashtable GetMapParentNode(Hashtable mapDtoNode)
        {
            var mapParentNode = new Hashtable();
            foreach (DictionaryEntry entry in mapDtoNode)
            {
                MemberNodeDto dto = (MemberNodeDto) entry.Value;
                var accountId = dto.AccountId;
                var parentId = dto.ParentId;
                if (mapParentNode.ContainsKey(parentId))
                {
                    var allChild = (HashSet<long>) mapParentNode[parentId];
                    allChild.Add(accountId);
                }
                else
                {
                    var allChild = new HashSet<long>();
                    allChild.Add(accountId);
                    mapParentNode.Add(parentId, allChild);
                }
            }
            return mapParentNode;
        }

        public bool IsContainMemberNode(long rootNumber, string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber) || string.Compare(rootNumber.ToString(), accountNumber) == 0)
            {
                return true;
            }
            var rootId = GetAccountIdBy(rootNumber.ToString());
            var accountId = GetAccountIdBy(accountNumber);
            var childNumber = CountAccountByParentId(rootId);
            if (childNumber == 0)
            {
                return false;
            }
            else
            {
                var mapDtoNode = GetMapMemberNodeDto();
                Hashtable mapParentNode = GetMapParentNode(mapDtoNode);
                return IsContainMemberNodeLoop(rootId, accountId, mapParentNode);
            }
        }

        private bool IsContainMemberNodeLoop(long rootId, long accountId, Hashtable mapParentNode)
        {
            HashSet<long> setChildIds = mapParentNode[rootId] as HashSet<long>;
            if (setChildIds != null && setChildIds.Contains(accountId))
            {
                return true;
            }
            if (setChildIds == null)
            {
                return false;
            }
            foreach (var childId in setChildIds)
            {
                if (IsContainMemberNodeLoop(childId, accountId, mapParentNode))
                {
                    return true;
                }
            }
            return false;
        }

        private List<MemberNodeDto> GetMemberNodeDtoLoop(long rootId, Hashtable mapDtoNode, Hashtable mapParentNode)
        {
            var listMemberNodeDto = new List<MemberNodeDto>();
            var rootDto = (MemberNodeDto)mapDtoNode[rootId];
            if (rootDto != null)
            {
                listMemberNodeDto.Add(rootDto);
            }
            if (mapParentNode.ContainsKey(rootId))
            {
                var setDto = (HashSet<long>)mapParentNode[rootId];
                foreach (var childId in setDto)
                {
                    listMemberNodeDto.AddRange(GetMemberNodeDtoLoop(childId, mapDtoNode, mapParentNode));
                } 
            }
            return listMemberNodeDto;
        }

        private Hashtable GetMapMemberNodeDto()
        {
            var sqlStr = "select new MemberInfo(a.AccountId, a.ParentId, a.AccountNumber, m.HoTen, u.UserName) from MemberInfo m, Account a, Users u " +
                    " where m.MemberID = a.MemberId and a.UserId = u.UserID";

            var mapMemberNodeDto = new Hashtable();

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);

                // Get the matching objects
                var allMemberInfo = query.List<MemberInfo>();

                foreach (var memberInfo in allMemberInfo)
                {
                    var memberNodeDto = new MemberNodeDto();
                    memberNodeDto.AccountId = memberInfo.AccountId;
                    memberNodeDto.ParentId = memberInfo.ParentId;
                    memberNodeDto.Description = memberInfo.HoTen + "|" + memberInfo.UserName + " [" + memberInfo.AccountNumber + "]";
                    mapMemberNodeDto.Add(memberNodeDto.AccountId, memberNodeDto);
                }
            }

            return mapMemberNodeDto;
        }

        private Hashtable GetMapManagerNodeDto(string modelName)
        {
            var sqlStr = "select new MemberInfo(b.AccountId, b.ParentId, a.AccountNumber, m.HoTen, u.UserName) from MemberInfo m, Account a, " + modelName + " b, Users u " +
                    " where m.MemberID = a.MemberId and a.AccountId = b.AccountId and a.UserId = u.UserID";

            var mapMemberNodeDto = new Hashtable();

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);

                // Get the matching objects
                var allMemberInfo = query.List<MemberInfo>();

                foreach (var memberInfo in allMemberInfo)
                {
                    var memberNodeDto = new MemberNodeDto();
                    memberNodeDto.AccountId = memberInfo.AccountId;
                    memberNodeDto.ParentId = memberInfo.ParentId;
                    memberNodeDto.Description = memberInfo.HoTen + "|" + memberInfo.UserName + " [" + memberInfo.AccountNumber + "]";
                    mapMemberNodeDto.Add(memberNodeDto.AccountId, memberNodeDto);
                }
            }

            return mapMemberNodeDto;
        }

        public MemberNodeDto GetNodeDto(string accountNumber)
        {
            var sqlStr = "select new MemberInfo(a.AccountId, a.ParentId, a.AccountNumber, m.HoTen, u.UserName) from MemberInfo m, Account a, Users u " +
                    " where m.MemberID = a.MemberId and a.UserId = u.UserID";
            var sqlParams = new Hashtable();
            if (!string.IsNullOrEmpty(accountNumber))
            {
                long accountNumberVal;
                var status = long.TryParse(accountNumber, out accountNumberVal);
                if (status)
                {
                    sqlStr += " and a.AccountNumber = :accountNumber";
                    sqlParams.Add("accountNumber", accountNumberVal);
                }
                else
                {
                    sqlStr += " and 1=0";
                }
            }
            else
            {
                sqlStr += " and 1=0";
            }

            MemberNodeDto memberNodeDto = null;

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);
                foreach (var key in sqlParams.Keys)
                {
                    query.SetParameter(key.ToString(), sqlParams[key]);
                }

                // Get the matching objects
                MemberInfo memberInfo = (MemberInfo)query.UniqueResult();

                if (memberInfo != null)
                {
                    memberNodeDto = new MemberNodeDto();
                    memberNodeDto.AccountId = memberInfo.AccountNumber;
                    memberNodeDto.ParentId = memberInfo.ParentId;
                    memberNodeDto.Description = memberInfo.HoTen + "|" + memberInfo.UserName + " [" + memberInfo.AccountNumber + "]";
                }
            }

            return memberNodeDto;
        }

        public MemberNodeDto GetParentNodeByChildNo(string accountNumber, string parentField)
        {
            var sqlStr = "select new MemberInfo(a.AccountId, a.ParentId, a.AccountNumber, m.HoTen, u.UserName) from MemberInfo m, Account a, Users u, Account a2 " +
                    " where m.MemberID = a.MemberId and a.UserId = u.UserID and a.AccountId = a2." + parentField;
            var sqlParams = new Hashtable();
            if (!string.IsNullOrEmpty(accountNumber))
            {
                long accountNumberVal;
                var status = long.TryParse(accountNumber, out accountNumberVal);
                if (status)
                {
                    sqlStr += " and a2.AccountNumber = :accountNumber";
                    sqlParams.Add("accountNumber", accountNumberVal);
                }
                else
                {
                    sqlStr += " and 1=0";
                }
            }
            else
            {
                sqlStr += " and 1=0";
            }

            MemberNodeDto memberNodeDto = null;

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);
                foreach (var key in sqlParams.Keys)
                {
                    query.SetParameter(key.ToString(), sqlParams[key]);
                }

                // Get the matching objects
                MemberInfo memberInfo = (MemberInfo)query.UniqueResult();

                if (memberInfo != null)
                {
                    memberNodeDto = new MemberNodeDto();
                    memberNodeDto.AccountId = memberInfo.AccountNumber;
                    memberNodeDto.ParentId = memberInfo.ParentId;
                    memberNodeDto.Description = memberInfo.HoTen + "|" + memberInfo.UserName + " [" + memberInfo.AccountNumber + "]";
                }
            }

            return memberNodeDto;
        }

        public MemberNodeDto GetNodeDto(string modelName, string accountNumber)
        {
            var sqlStr = "select new MemberInfo(b.AccountId, b.ParentId, a.AccountNumber, m.HoTen, u.UserName) from MemberInfo m, Account a, " + modelName + " b, Users u " +
                    " where m.MemberID = a.MemberId and a.AccountId = b.AccountId and a.UserId = u.UserID";
            var sqlParams = new Hashtable();
            if (!string.IsNullOrEmpty(accountNumber))
            {
                long accountNumberVal;
                var status = long.TryParse(accountNumber, out accountNumberVal);
                if (status)
                {
                    sqlStr += " and a.AccountNumber = :accountNumber";
                    sqlParams.Add("accountNumber", accountNumberVal);
                }
                else
                {
                    sqlStr += " and 1=0";
                }
            }
            else
            {
                sqlStr += " and 1=0";
            }

            MemberNodeDto memberNodeDto = null;

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);
                foreach (var key in sqlParams.Keys)
                {
                    query.SetParameter(key.ToString(), sqlParams[key]);
                }

                // Get the matching objects
                MemberInfo memberInfo = (MemberInfo)query.UniqueResult();

                if (memberInfo != null)
                {
                    memberNodeDto = new MemberNodeDto();
                    memberNodeDto.AccountId = memberInfo.AccountNumber;
                    memberNodeDto.ParentId = memberInfo.ParentId;
                    memberNodeDto.Description = memberInfo.HoTen + "|" + memberInfo.UserName + " [" + memberInfo.AccountNumber + "]";
                }
            }

            return memberNodeDto;
        }

        public MemberNodeDto GetParentNodeByChildNo(string modelName, string accountNumber, string parentField)
        {
            var sqlStr = "select new MemberInfo(b.AccountId, b.ParentId, a.AccountNumber, m.HoTen, u.UserName) from MemberInfo m, Account a, " + modelName + " b, Users u, " + modelName + " b2, Account a2 " +
                    " where m.MemberID = a.MemberId and a.AccountId = b.AccountId and a.UserId = u.UserID and a2.AccountId = b2.AccountId and b.AccountId = b2." + parentField;
            var sqlParams = new Hashtable();
            if (!string.IsNullOrEmpty(accountNumber))
            {
                long accountNumberVal;
                var status = long.TryParse(accountNumber, out accountNumberVal);
                if (status)
                {
                    sqlStr += " and a2.AccountNumber = :accountNumber";
                    sqlParams.Add("accountNumber", accountNumberVal);
                }
                else
                {
                    sqlStr += " and 1=0";
                }
            }
            else
            {
                sqlStr += " and 1=0";
            }

            MemberNodeDto memberNodeDto = null;

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);
                foreach (var key in sqlParams.Keys)
                {
                    query.SetParameter(key.ToString(), sqlParams[key]);
                }

                // Get the matching objects
                MemberInfo memberInfo = (MemberInfo)query.UniqueResult();

                if (memberInfo != null)
                {
                    memberNodeDto = new MemberNodeDto();
                    memberNodeDto.AccountId = memberInfo.AccountNumber;
                    memberNodeDto.ParentId = memberInfo.ParentId;
                    memberNodeDto.Description = memberInfo.HoTen + "|" + memberInfo.UserName + " [" + memberInfo.AccountNumber + "]";
                }
            }

            return memberNodeDto;
        }

        public string UpdatePaid(BangKeDto[] bangKeDtos)
        {
            var userNames = new List<string>();
            foreach (BangKeDto bangKeDto in bangKeDtos)
            {
                userNames.Add(bangKeDto.UserName);
            }
            var mapAccountIds = GetMapAccountIdBy(userNames.ToArray());
            if (mapAccountIds.Count == 0)
            {
                return "-1";
            }
            var allAccountBonus = new List<AccountBonus>();
            // List all AccountBonus
            foreach (BangKeDto bangKeDto in bangKeDtos)
            {
                var mapParams = new Hashtable();
                mapParams.Add("AccountId", mapAccountIds[bangKeDto.UserName]);
                var month = DateUtil.GetDateTimeAsStringWithEnProvider(DateUtil.GetDateTime(bangKeDto.Thang),
                                                                       ConstUtil.MONTH_FORMAT);
                mapParams.Add("Month", month);
                var listAccountBonus = RetrieveEquals<AccountBonus>(mapParams);
                foreach (AccountBonus accountBonus in listAccountBonus)
                {
                    accountBonus.IsPaid = bangKeDto.IsPaid;
                }
                allAccountBonus.AddRange(listAccountBonus);
            }
            // Save AccountBonus
            foreach (AccountBonus accountBonus in allAccountBonus)
            {
                Save(accountBonus);
            }
            return "0";
        }

		public ManagerApprovalDto[] SearchManagerApproval(string capQuanLy, string accountNumber)
		{
            List<ManagerApprovalDto> allResults;

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select new ManagerApproval(ma.Id, a.AccountNumber, ma.ManagerLevel, u.UserName) "
					+ " from ManagerApproval ma, Account a, Users u "
                    + " where ma.IsApproved = :isApproved and ma.AccountId = a.AccountId and a.UserId = u.UserID"
					+ " order by ma.CreatedDate asc");
				query.SetParameter("isApproved", "N");

                // Get the matching objects
                var list = query.List<ManagerApproval>();

                // Set return value
                allResults = CreateAllManagerApprovalDto(list);
            }
            return allResults.ToArray();
		}
		
		private List<> CreateAllManagerApprovalDto(IEnumerable<ManagerApproval> list)
		{
			List<ManagerApprovalDto> allResults new List<ManagerApprovalDto>();
			foreach(ManagerApproval model in list)
			{
				ManagerApprovalDto dto = new ManagerApprovalDto();
				dto.Id = model.Id;
				dto.AccountNumber = model.AccountNumber;
				dto.ManagerLevel = model.ManagerLevel;
				dto.UserName = model.UserName;
				allResults.Add(dto);
			}
			return allResults;
		}
		
		public string UpdateManagerApproval(ManagerApprovalDto dto)
		{
			var mapParams = new Hashtable();
			mapParams.Add("ManagerLevel", dto.ManagerLevel);
			var accountId = GetAccountIdBy(dto.AccountNumber);
			mapParams.Add("AccountId", accountId);
			mapParams.Add("IsApproved", "N");
			var list = RetrieveEquals<ManagerApproval>(mapParams);
			if (list.Count == 0)
			{
				return "-1";
			}
			foreach(ManagerApproval model in list)
			{
				model.IsApproved = "I";
				model.ApprovedBy = dto.ApprovedBy;
				model.ApprovedDate = DateTime.Now;
                Save(model);
			}
            return "0";
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

            SaveNew(bonus);
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
            return RetrieveEqualsWithOrder<AccountPreCalc>("IsCalculated", "N", "Id", "ASC");
        }

        public long CountUpLevel(long calcAccountId, int upAccountLevel)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {

                var query = session.CreateQuery("select count(*) from AccountPreCalc a "
                   + " where a.CalcAccountId = :CalcAccountId and Account_Level = :AccountLevel");
                query.SetParameter("CalcAccountId", calcAccountId);
                query.SetParameter("AccountLevel", upAccountLevel);

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

        public int CountCalculatedByLevel(long calcAccountId, int accountLevel)
        {

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(*) from AccountPreCalc a "
                   + " where a.CalcAccountId = :calcAccountId and a.AccountLevel = :accountLevel and a.IsCalculated = :isCalculated");
                query.SetParameter("calcAccountId", calcAccountId);
                query.SetParameter("accountLevel", accountLevel);
                query.SetParameter("isCalculated", "Y");

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);
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

        public IList<ManagerLevelLog> GetManagerLog()
        {
            return RetrieveAll<ManagerLevelLog>(SessionAction.BeginAndEnd);
        }

        public IList<ManagerApproval> GetApprovedManager()
        {
            return RetrieveEqualsWithOrder<ManagerApproval>("IsApproved", "Y", "Id", "Asc");
        }

        public Account GetAccount(long accountId)
        {
            IList<Account> retrieveEquals = RetrieveEquals<Account>("AccountId", accountId);
            if (retrieveEquals == null || retrieveEquals.Count == 0)
                return null;
            return retrieveEquals[0];
        }

        public Object GetManagerLevel<T>(long accountId)
        {
            IList<T> retrieveEquals = RetrieveEquals<T>("AccountId", accountId);
            if (retrieveEquals == null || retrieveEquals.Count == 0)
                return null;
            return retrieveEquals[0];
        }

        public long GetRowCount(string tableName)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select count(*) from " + tableName);

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt64(count);
            }
        }

        public UserDto[] GetNewMemberList()
        {
            var sqlStr = "select new MemberInfo(a.AccountNumber, m.HoTen, u.UserName, a.ParentId, a.ParentDirectId, m.NgaySinh, m.SoCmnd, m.NgayCap, m.SoDienThoai, m.DiaChi, "
                    + " m.GioiTinh, m.SoTaiKhoan, m.ChiNhanhNH, m.ImageUrl, m.CreatedDate, m.CreatedBy) from MemberInfo m, Account a, Users u " +
                    " where m.MemberID = a.MemberId and a.UserId = u.UserID order by m.CreatedDate desc";

            var allUserDto = new List<UserDto>();
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);
                query.SetMaxResults(16);

                // Get the matching objects
                var list = query.List();

                foreach (var oneRow in list)
                {
                    var memberInfo = oneRow as MemberInfo;
                    if (memberInfo == null)
                    {
                        continue;
                    }
                    var userDto = new UserDto();
                    userDto.AccountNumber = memberInfo.AccountNumber;
                    userDto.FullName = memberInfo.HoTen;
                    userDto.UserName = memberInfo.UserName;
                    userDto.ParentId = GetAccountNumberBy(memberInfo.ParentId);
                    userDto.ParentDirectId = GetAccountNumberBy(memberInfo.ParentDirectId);
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
                    allUserDto.Add(userDto);
                }
            }

            return allUserDto.ToArray();
        }

        public UserDto[] GetNewManagerList()
        {
            var sqlStr = "select new MemberInfo(a.AccountNumber, m.HoTen, u.UserName, a.ParentId, a.ParentDirectId, m.NgaySinh, m.SoCmnd, m.NgayCap, m.SoDienThoai, m.DiaChi, "
                    + " m.GioiTinh, m.SoTaiKhoan, m.ChiNhanhNH, m.ImageUrl, m.CreatedDate, m.CreatedBy) from MemberInfo m, Account a, Users u, ManagerL1 m1 " +
                    " where m.MemberID = a.MemberId and a.UserId = u.UserID and a.AccountId = m1.AccountId order by m.CreatedDate desc";

            var allUserDto = new List<UserDto>();
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);
                query.SetMaxResults(8);

                // Get the matching objects
                var list = query.List();

                foreach (var oneRow in list)
                {
                    var memberInfo = oneRow as MemberInfo;
                    if (memberInfo == null)
                    {
                        continue;
                    }
                    var userDto = new UserDto();
                    userDto.AccountNumber = memberInfo.AccountNumber;
                    userDto.FullName = memberInfo.HoTen;
                    userDto.UserName = memberInfo.UserName;
                    userDto.ParentId = GetAccountNumberBy(memberInfo.ParentId);
                    userDto.ParentDirectId = GetAccountNumberBy(memberInfo.ParentDirectId);
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
                    allUserDto.Add(userDto);
                }
            }

            return allUserDto.ToArray();
        }

        public int GetReportYear()
        {
            return DateTime.Now.Year;
        }

        public AccountBonusDto[] GetAcountBonusList()
        {
            var sqlStr = "select new AccountBonus(a.IsPaid, sum(a.BonusAmount), a.Month) from AccountBonus a " +
                    " group by a.IsPaid, a.Month "
                    + " where a.Month >= :monthBegin and a.Month <= :monthEnd "
                    + " order by a.IsPaid asc, a.Month desc";

            var allAccountBonusDto = new List<AccountBonusDto>();
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sqlStr);
                var reportYear = GetReportYear();
                query.SetParameter("monthBegin", reportYear + "01");
                query.SetParameter("monthEnd", reportYear + "12");
                query.SetMaxResults(8);

                // Get the matching objects
                var list = query.List();

                foreach (var oneRow in list)
                {
                    var accountBonus = oneRow as AccountBonus;
                    if (accountBonus == null)
                    {
                        continue;
                    }
                    var accountBonusDto = new AccountBonusDto();
                    accountBonusDto.IsPaid = accountBonus.IsPaid;
                    accountBonusDto.Thang = accountBonus.Month.Substring(4, 2) + "/" + accountBonus.Month.Substring(0, 4); ;
                    accountBonusDto.Tong = accountBonus.BonusAmount;
                    allAccountBonusDto.Add(accountBonusDto);
                }
            }

            return allAccountBonusDto.ToArray();
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