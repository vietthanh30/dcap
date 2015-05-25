using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ws_server.model;
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

        public string login(string userName, string password)
        {
            var userId = m_PersistenceManager.checkUser(userName, password);
            if ("-1".Equals(userId))
            {
                return "Khong ton tai user";
            }
            return "User ID: " + userId;
        }

        public string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            var userId = m_PersistenceManager.changePassword(userName, oldPassword, newPassword, confirmPassword);
            if ("-1".Equals(userId))
            {
                return "Chua nhap ten dang nhap";
            }
            if ("-2".Equals(userId))
            {
                return "Chua nhap mat khau cu";
            }
            if ("-3".Equals(userId))
            {
                return "Mat khau cu khong dung";
            }
            if ("-4".Equals(userId))
            {
                return "Chua nhap mat khau moi";
            }
            if ("-5".Equals(userId))
            {
                return "Mat khau moi trung mat khau cu";
            }
            if ("-6".Equals(userId))
            {
                return "Chua nhap xac nhan mat khau moi";
            }
            if ("-7".Equals(userId))
            {
                return "Mat khau moi va xac nhan mat khau moi khong khop nhau";
            }
            return "Change password User (ID=" + userId + ") success";
        }

        public string createUser(string userName, string password, string confirmPassword)
        {
            var returnCode = m_PersistenceManager.createUser(userName, password, confirmPassword);
            if ("-1".Equals(returnCode))
            {
                return "Chua nhap ten dang nhap";
            }
            if ("-2".Equals(returnCode))
            {
                return "Chua nhap mat khau";
            }
            if ("-3".Equals(returnCode))
            {
                return "Chua nhap xac nhan mat khau";
            }
            if ("-4".Equals(returnCode))
            {
                return "Mat khau va xac nhan mat khau khong khop nhau";
            }
            if ("-5".Equals(returnCode))
            {
                return "Da ton tai user trong he thong";
            }
            return "Create User success";
        }

        #endregion
    }
}