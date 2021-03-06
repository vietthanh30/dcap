﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using core_lib.common;
using web_app.DcapServiceReference;

namespace web_app.common
{
    public class DcapServiceUtil
    {
        private static DcapServiceReference.DcapServiceSoapClient dcapService = new DcapServiceReference.DcapServiceSoapClient();

        public static bool IsValidAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber) || accountNumber.Length != 7)
            {
                return false;
            }
            if (!char.IsLetter(accountNumber[0]))
            {
                return false;
            }
            long accountNumberVal;
            if (!long.TryParse(accountNumber.Substring(3), out accountNumberVal))
            {
                return false;
            }
            return true;
        }

        public static string GetPrefixAccountNumber()
        {
            return ParameterUtil.GetParameter("PrefixAccountNumber");
        }

        public static UserDto login(string userName, string password)
        {
            return dcapService.login(userName, password);
        }

        public static string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            return dcapService.changePassword(userName, oldPassword, newPassword, confirmPassword);
        }

        public static string CreateUser(String parentId, String directParentId, String userName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            return dcapService.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan,
                chiNhanhNH, photoUrl, createdBy, GetPrefixAccountNumber());
        }

        public static string UpdateUser(String userName, String fullName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl)
        {
            return dcapService.UpdateUser(userName, fullName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl);
        }

        public static string SearchUser(String parentId, String directParentId, String userName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
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

        public static BangKeDto[] SearchBangKeExt(string accountNumber, string userName, DateTime? beginDate, DateTime? endDate)
        {
            return dcapService.SearchBangKeExt(accountNumber, userName, beginDate, endDate);
        }

        public static HoaHongMemberDto[] SearchBangKeHoaHong(string accountNumber, DateTime? thangKeKhai)
        {
            return dcapService.SearchBangKeHoaHong(accountNumber, thangKeKhai);
        }

        public static UserDto[] SearchUserInfo(string soCmnd, string idThanhVien, string hoTen)
        {
            return dcapService.SearchUserInfo(soCmnd, idThanhVien, hoTen);
        }

        public static MemberNodeDto[] SearchMemberNodeDto(string accountNumber)
        {
            return dcapService.SearchMemberNodeDto(accountNumber);
        }

        public static MemberNodeDto[] SearchManagerNodeDto(string capQuanLy, string accountNumber)
        {
            return dcapService.SearchManagerNodeDto(capQuanLy, accountNumber);
        }

        public static bool IsContainMemberNode(string rootNumber, string accountNumber)
        {
            return dcapService.IsContainMemberNode(rootNumber, accountNumber);
        }

        public static MemberNodeDto GetNodeDto(string accountNumber)
        {
            return dcapService.GetNodeDto(accountNumber);
        }

        public static MemberNodeDto GetParentManagerNodeByChildNo(string capQuanLy, string accountNumber)
        {
            return dcapService.GetParentManagerNodeByChildNo(capQuanLy, accountNumber, "ParentId");
        }

        public static MemberNodeDto GetParentNodeByChildNo(string accountNumber)
        {
            return dcapService.GetParentNodeByChildNo(accountNumber, "ParentId");
        }

        public static MemberNodeDto GetParentDirectNodeByChildNo(string accountNumber)
        {
            return dcapService.GetParentNodeByChildNo(accountNumber, "ParentDirectId");
        }

        public static long GetMemberAmount()
        {
            return dcapService.GetMemberAmount();
        }

        public static long GetAccountAmount()
        {
            return dcapService.GetAccountAmount();
        }

        public static string GetManagerAmount()
        {
            return dcapService.GetManagerAmount();
        }

        public static long GetManagerL6Amount()
        {
            return dcapService.GetManagerL6Amount();
        }

        public static UserDto[] GetNewMemberList()
        {
            return dcapService.GetNewMemberList();
        }

        public static UserDto[] GetNewManagerList()
        {
            return dcapService.GetNewManagerList();
        }

        public static int GetReportYear()
        {
            return dcapService.GetReportYear();
        }

        public static AccountBonusDto[] GetAcountBonusList()
        {
            return dcapService.GetAcountBonusList();
        }

        public static string UpdatePaid(BangKeDto[] bangKeDtos)
        {
            return dcapService.UpdatePaid(bangKeDtos);
        }
		
		public static ManagerApprovalDto[] SearchManagerApproval(string capQuanLy, string accountNumber)
		{
            return dcapService.SearchManagerApproval(capQuanLy, accountNumber);
		}
		
		public static string UpdateManagerApproval(ManagerApprovalDto dto)
		{
            return dcapService.UpdateManagerApproval(dto);
		}

		public static BonusApprovalDto[] SearchBonusApproval(string accountNumber, string userName, string isApproved)
		{
            return dcapService.SearchBonusApproval(accountNumber, userName, isApproved);
		}
		
		public static string CreateBonusApproval(BonusApprovalDto dto)
		{
            return dcapService.CreateBonusApproval(dto);
		}
		
		public static string UpdateBonusApproval(BonusApprovalDto dto)
		{
            return dcapService.UpdateBonusApproval(dto);
		}
    }
}