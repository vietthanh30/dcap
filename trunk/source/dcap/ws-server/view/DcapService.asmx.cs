using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using domain_lib.controller;
using domain_lib.dto;

namespace ws_server.view
{
    /// <summary>
    /// Summary description for DcapService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DcapService : System.Web.Services.WebService
    {
        private Controller controller = new Controller();

        [WebMethod]
        public UserDto login(String userName, String password)
        {

            return controller.login(userName, password);
        }

        [WebMethod]
        public string changePassword(String userName, String oldPassword, String newPassword, String confirmPassword)
        {
            return controller.changePassword(userName, oldPassword, newPassword, confirmPassword);
        }

        [WebMethod]
        public string CreateUser(String parentId, String directParentId, String userName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            return controller.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
        }

        [WebMethod]
        public string UpdateUser(String userName, String fullName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl)
        {
            return controller.UpdateUser(userName, fullName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl);
        }

        [WebMethod]
        public string CreateUserAdmin(String userName, String fullName, string roleCode, string createdBy)
        {
            return controller.CreateUserAdmin(userName, fullName, roleCode, createdBy);
        }

        [WebMethod]
        public string SearchUser(String parentId, String directParentId, String userName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH)
        {
            return controller.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH);
        }

        [WebMethod]
        public int CalculateAccountLog()
        {
            return controller.CalculateAccountLog();
        }

        [WebMethod]
        public int CalculateBonusOfAccountTree()
        {
            return controller.CalculateBonusOfAccountTree();
        }
    }
}