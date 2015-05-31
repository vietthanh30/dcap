using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web_app.DcapServiceReference;

namespace web_app.common
{
    public class DcapServiceUtil
    {
        private static DcapServiceReference.DcapServiceSoapClient dcapService = new DcapServiceReference.DcapServiceSoapClient();

        public static UserDto login(string userName, string password)
        {
            return dcapService.login(userName, password);
        }

        public static string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            return dcapService.changePassword(userName, oldPassword, newPassword, confirmPassword);
        }

        public static string CreateUser(String parentId, String directParentId, String userName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            return dcapService.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
        }

        public static string SearchUser(String parentId, String directParentId, String userName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH)
        {
            return dcapService.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH);
        }
    }
}