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

        public static string CreateUser(String parentId, String directParentId, String userName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            return dcapService.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
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

        public static BangKeDto[] SearchBangKeExt(DateTime? beginDate, DateTime? endDate)
        {
            return dcapService.SearchBangKeExt(beginDate, endDate);
        }

        public static HoaHongMemberDto[] SearchBangKeHoaHong(long accountNumber, DateTime? thangKeKhai)
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

        public static bool IsContainMemberNode(long rootNumber, string accountNumber)
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

        public static long GetManagerAmount()
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
    }
}