using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ws_server.persistence;

namespace ws_server.controller
{
    public class Controller
    {
        #region Declarations

        // Member variables
        private PersistenceManager m_PersistenceManager = new PersistenceManager();
        
        // Property variables

        #endregion

        #region Constructor

        public Controller()
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        /// <summary>
        /// Clears all records from the database.
        /// </summary>
        /// <remarks>We use this method to reset the database at the beginning or each run.</remarks>
        public void ClearDatabase()
        {
            m_PersistenceManager.ClearDatabase();
        }

        public string login(string userName, string password)
        {
            return "login success";
        }

        public string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            return "change password success";
        }

        public string createUser(string userName, string password, string confirmPassword)
        {
            return "create user success";
        }

        #endregion
    }
}