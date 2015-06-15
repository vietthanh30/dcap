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
        public string CreateUser(String parentId, String directParentId, String userName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            return controller.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
        }

        [WebMethod]
        public string UpdateUser(String userName, String fullName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
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
        public string SearchUser(String parentId, String directParentId, String userName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH)
        {
            return controller.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH);
        }

        [WebMethod]
        public BangKeDto[] SearchBangKe(DateTime? thangKeKhai)
        {
            return controller.SearchBangKe(thangKeKhai);
        }

        [WebMethod]
        public BangKeDto[] SearchBangKeExt(string accountNumber, string userName, DateTime? beginDate, DateTime? endDate)
        {
            return controller.SearchBangKeExt(accountNumber, userName, beginDate, endDate);
        }

        [WebMethod]
        public HoaHongMemberDto[] SearchBangKeHoaHong(long accountNumber, DateTime? thangKeKhai)
        {
            return controller.SearchBangKeHoaHong(accountNumber, thangKeKhai);
        }

        [WebMethod]
        public UserDto[] SearchUserInfo(string soCmnd, string idThanhVien, string hoTen)
        {
            return controller.SearchUserInfo(soCmnd, idThanhVien, hoTen);
        }

        [WebMethod]
        public MemberNodeDto[] SearchMemberNodeDto(string accountNumber)
        {
            return controller.SearchMemberNodeDto(accountNumber);
        }

        [WebMethod]
        public MemberNodeDto[] SearchManagerNodeDto(string capQuanLy, string accountNumber)
        {
            return controller.SearchManagerNodeDto(capQuanLy, accountNumber);
        }

        [WebMethod]
        public bool IsContainMemberNode(long rootNumber, string accountNumber)
        {
            return controller.IsContainMemberNode(rootNumber, accountNumber);
        }

        [WebMethod]
        public MemberNodeDto GetNodeDto(string accountNumber)
        {
            return controller.GetNodeDto(accountNumber);
        }

        [WebMethod]
        public MemberNodeDto GetParentNodeByChildNo(string accountNumber, string parentField)
        {
            return controller.GetParentNodeByChildNo(accountNumber, parentField);
        }

        [WebMethod]
        public MemberNodeDto GetParentManagerNodeByChildNo(string capQuanLy, string accountNumber, string parentField)
        {
            return controller.GetParentManagerNodeByChildNo(capQuanLy, accountNumber, parentField);
        }

        [WebMethod]
        public string UpdatePaid(BangKeDto[] bangKeDtos)
        {
            return controller.UpdatePaid(bangKeDtos);
        }

        [WebMethod]
		public ManagerApprovalDto[] SearchManagerApproval(string capQuanLy, string accountNumber)
		{
            return controller.SearchManagerApproval(capQuanLy, accountNumber);
		}
		
        [WebMethod]
		public string UpdateManagerApproval(ManagerApprovalDto dto)
		{
            return controller.UpdateManagerApproval(dto);
		}

        [WebMethod]
		public BonusApprovalDto[] SearchBonusApproval(string accountNumber, string userName, string isApproved)
		{
            return controller.SearchBonusApproval(accountNumber, userName, isApproved);
		}
		
        [WebMethod]
		public string CreateBonusApproval(BonusApprovalDto dto)
		{
            return controller.CreateBonusApproval(dto);
		}

        [WebMethod]
		public string UpdateBonusApproval(BonusApprovalDto dto)
		{
            return controller.UpdateBonusApproval(dto);
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

        [WebMethod]
        public int ExecuteApprovedManager()
        {
            return controller.ExecuteApprovedManager();
        }

        [WebMethod]
        public int CalculateBonusOfManagerTree()
        {
            return controller.CalculateBonusOfManagerTree();
        }

        [WebMethod]
        public long GetMemberAmount()
        {
            return controller.GetMemberAmount();
        }

        [WebMethod]
        public long GetAccountAmount()
        {
            return controller.GetAccountAmount();
        }

        [WebMethod]
        public string GetManagerAmount()
        {
            return controller.GetManagerAmount();
        }

        [WebMethod]
        public long GetManagerL6Amount()
        {
            return controller.GetManagerL6Amount();
        }

        [WebMethod]
        public UserDto[] GetNewMemberList()
        {
            return controller.GetNewMemberList();
        }

        [WebMethod]
        public UserDto[] GetNewManagerList()
        {
            return controller.GetNewManagerList();
        }

        [WebMethod]
        public int GetReportYear()
        {
            return controller.GetReportYear();
        }

        [WebMethod]
        public AccountBonusDto[] GetAcountBonusList()
        {
            return controller.GetAcountBonusList();
        }

    }
}