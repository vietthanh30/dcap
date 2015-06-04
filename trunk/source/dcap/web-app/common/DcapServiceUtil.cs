﻿using System;
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

        public static string UpdateUser(String userName, String fullName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl)
        {
            return dcapService.UpdateUser(userName, fullName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl);
        }

        public static string SearchUser(String parentId, String directParentId, String userName, DateTime? ngaySinh, String soCmnd, DateTime? ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH)
        {
            return dcapService.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH);
        }

        public static string CreateUserAdmin(string userName, string fullName, string roleCode, string createdBy)
        {
            return dcapService.CreateUserAdmin(userName, fullName, roleCode, createdBy);
        }

        public static BangKeDto[] SearchBangKe(DateTime? thangKeKhai)
        {
            return dcapService.SearchBangKe(thangKeKhai);
        }

        public static UserDto[] SearchUserInfo(string soCmnd, string idThanhVien, string hoTen)
        {
            return dcapService.SearchUserInfo(soCmnd, idThanhVien, hoTen);
        }

        public static MemberNodeDto[] SearchMemberNodeDto(string idMember)
        {
            return dcapService.SearchMemberNodeDto(idMember);
        }

        public static long GetParentIdBy(string accountNumber)
        {
            return dcapService.GetParentIdBy(accountNumber);
        }
    }
}