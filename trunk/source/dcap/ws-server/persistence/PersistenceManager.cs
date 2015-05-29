using System;
using System.Collections.Generic;
using core_lib.common;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Expression;
using ws_server.model;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ws_server.persistence
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

        public string checkUser(string userName, string password)
        {
            if (String.Empty.Equals(userName))
            {
                return "-1";
            }
            if (String.Empty.Equals(password))
            {
                return "-2";
            }
            var users = RetrieveEquals<Users>("UserName", userName.ToUpper());
            if (users.Count == 0)
            {
                return "-3";
            }
            var user = users[0];
            if (string.Compare(MD5Util.EncodeMD5(password), user.Password, true) != 0)
            {
                return "-4";
            }
            return "0";
        }

        public string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            if (String.Empty.Equals(userName))
            {
                return "-1";
            }
            if (String.Empty.Equals(oldPassword))
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
            if (String.Empty.Equals(newPassword))
            {
                return "-5";
            }
            if (string.Compare(oldPassword,newPassword, true) == 0)
            {
                return "-6";
            }
            if (String.Empty.Equals(confirmPassword))
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

        private bool IsExistingAccount(string accountId)
        {
            var accounts = RetrieveEquals<Account>("AccountId", Convert.ToInt64(accountId));
            return accounts.Count > 0;
        }

        private int CountAccountByParentId(string parentId)
        {
            var accounts = RetrieveEquals<Account>("ParentId", Convert.ToInt64(parentId));
            return accounts.Count;
        }

        public string CreateUser(String parentId, String directParentId, String userName, String ngaySinh, String soCmnd, String diaChi, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            if (String.Empty.Equals(userName))
            {
                return "-1";
            }
            if (String.Empty.Equals(soCmnd))
            {
                return "-1";
            }
            if (!String.Empty.Equals(parentId) & !IsExistingAccount(parentId))
            {
                return "-1";
            }
            if (!String.Empty.Equals(directParentId) & !IsExistingAccount(directParentId))
            {
                return "-1";
            }
            if (!String.Empty.Equals(parentId) & CountAccountByParentId(parentId) > 3)
            {
                return "-1";
            }

            var memberInfos = RetrieveEquals<MemberInfo>("SoCmnd", soCmnd);
            if (memberInfos.Count > 0)
            {
                return CreateAccountForExistingMember(memberInfos[0], parentId, directParentId, photoUrl, createdBy);
            }
            return CreateAccountForNewMember(parentId, directParentId, userName, ngaySinh, soCmnd, diaChi, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
        }

        private string CreateAccountForExistingMember(MemberInfo memberInfo, String parentId, String directParentId, string photoUrl, string createdBy)
        {
            var memberID = memberInfo.MemberID;
            var accountAmount = GetAccountAmountBy(memberID);
            var tenDangNhap = GetNextTenDangNhap(memberID, accountAmount);
            
            memberInfo.ImageUrl = photoUrl;
            Save(memberInfo);

            var user = new Users { UserName = tenDangNhap, Password = MD5Util.EncodeMD5(ConstUtil.DEFAULT_PASSWORD) };

            // Save user
            Save(user);

            var users = RetrieveEquals<Users>("UserName", tenDangNhap);
            if (users.Count == 0)
            {
                return "-1";
            }
            user = users[0];

            var account = new Account();
            account.AccountNumber = GetNextAccountNumber();
            account.MemberId = memberID;
            if (!String.Empty.Equals(parentId))
            {
                account.ParentId = Convert.ToInt64(parentId);
            }
            if (!String.Empty.Equals(directParentId))
            {
                account.ParentId = Convert.ToInt64(directParentId);
            }
            account.UserId = user.UserID;
            account.ChildIndex = accountAmount;
            account.IsActive = ConstUtil.ACTIVE_STATUS;
            account.CreatedBy = createdBy;
            account.CreatedDate = DateTime.Now;

            Save(account);

            return user.UserName;
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

        private string GetTenDangNhapBy(long memberId)
        {
            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select u.UserName from Users u, Account a "
                    + " where u.UserID = a.UserId and a.MemberId = :memberId");
                query.SetParameter("memberId", memberId);

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
            var tenDangNhap = GetTenDangNhapBy(memberId);
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
                    + " where u.UserName like ':username'");
                query.SetParameter("username", username + "%");

                // Get the matching objects
                var count = query.UniqueResult();

                // Set return value
                return Convert.ToInt32(count);
            }
        }
        private string GetValidTenDangNhapBy(string fullname)
        {
            var tenDangNhap = fullname.Replace(" ", "").ToUpper();
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
            return tenDangNhap;
        }

        private string CreateAccountForNewMember(String parentId, String directParentId, String userName, String ngaySinh, String soCmnd, String diaChi, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            var tenDangNhap = GetValidTenDangNhapBy(userName);

            // Init member object
            var memberInfo = new MemberInfo
                                 {
                                     HoTen = userName,
                                     NgaySinh = DateUtil.GetDateTime(ngaySinh),
                                     SoCmnd = soCmnd,
                                     NgayCap = DateTime.Now,
                                     DiaChi = diaChi,
                                     SoTaiKhoan = soTaiKhoan,
                                     ChiNhanhNH = chiNhanhNH,
                                     ImageUrl = photoUrl
                                 };
            // Save memberInfo
            Save(memberInfo);

            var memberInfos = RetrieveEquals<MemberInfo>("SoCmnd", soCmnd);
            if (memberInfos.Count == 0)
            {
                return "-1";
            }
            memberInfo = memberInfos[0];


            var user = new Users { UserName = tenDangNhap, Password = MD5Util.EncodeMD5(ConstUtil.DEFAULT_PASSWORD) };

            // Save user
            Save(user);

            var users = RetrieveEquals<Users>("UserName", tenDangNhap);
            if (users.Count == 0)
            {
                return "-1";
            }
            user = users[0];

            var account = new Account();
            account.AccountNumber = GetNextAccountNumber();
            account.MemberId = memberInfo.MemberID;
            if (!String.Empty.Equals(parentId))
            {
                account.ParentId = Convert.ToInt64(parentId);
            }
            if (!String.Empty.Equals(directParentId))
            {
                account.ParentId = Convert.ToInt64(directParentId);
            }
            account.UserId = user.UserID;
            account.ChildIndex = 0;
            account.IsActive = ConstUtil.ACTIVE_STATUS;
            account.CreatedBy = createdBy;
            account.CreatedDate = DateTime.Now;

            Save(account);

            return user.UserName;
        }

        public string SearchUser(String parentId, String directParentId, String userName, String ngaySinh, String soCmnd, String diaChi, String soTaiKhoan,
            String chiNhanhNH)
        {
            if (String.Empty.Equals(userName))
            {
                return "-1";
            }
            if (String.Empty.Equals(soCmnd))
            {
                return "-1";
            }

            var allTenDangNhap = string.Empty;

            using (ISession session = m_SessionFactory.OpenSession())
            {
                var query = session.CreateQuery("select u.UserName from MemberInfo m, Users u, Account a "
                    + " where m.MemberID = a.MemberId and u.UserID = a.UserId and m.SoCmnd = :soCmnd");
                query.SetParameter("soCmnd", soCmnd);

                // Get the matching objects
                var list = query.List();

                foreach (var tenDangNhap in list)
                {
                    if (String.Empty.Equals(allTenDangNhap))
                    {
                        allTenDangNhap = tenDangNhap.ToString();   
                    }
                    else
                    {
                        allTenDangNhap = allTenDangNhap + ";" + tenDangNhap;
                    }
                }
            }

            // Set return value
            return allTenDangNhap;
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