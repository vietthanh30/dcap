using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_app.common
{
    public class DcapServiceUtil
    {
        private static DcapServiceReference.DcapServiceSoapClient dcapService = new DcapServiceReference.DcapServiceSoapClient();

        public static string login(string userName, string password)
        {
            return dcapService.login(userName, password);
        }

        public static string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            return dcapService.changePassword(userName, oldPassword, newPassword, confirmPassword);
        }

        public static string CreateUser(String parentId, String directParentId, String userName, String ngaySinh, String soCmnd, String diaChi, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            return dcapService.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, diaChi, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
        }

        public static string SearchUser(String parentId, String directParentId, String userName, String ngaySinh, String soCmnd, String diaChi, String soTaiKhoan,
            String chiNhanhNH)
        {
            return dcapService.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, diaChi, soTaiKhoan, chiNhanhNH);
        }
    }
}