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
            return m_PersistenceManager.checkUser(userName, password);
        }

        public string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            return m_PersistenceManager.changePassword(userName, oldPassword, newPassword, confirmPassword);
        }

        public string CreateUser(String parentId, String directParentId, String userName, String ngaySinh, String soCmnd, String diaChi, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            return m_PersistenceManager.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, diaChi, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
        }

        public string SearchUser(String parentId, String directParentId, String userName, String ngaySinh, String soCmnd, String diaChi, String soTaiKhoan,
            String chiNhanhNH)
        {
            return m_PersistenceManager.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, diaChi, soTaiKhoan, chiNhanhNH);
        }


        public IList<AccountLog> CalculateAccountLog()
        {
            return m_PersistenceManager.GetAccountLog();
        }
        
        #endregion
    }
}