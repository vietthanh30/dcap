using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.admin
{
    public partial class DuyetCapQuanLy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            if (!UserUtil.IsQthtRole(userDto))
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
        }

        protected void DuyetCapQuanLy_Search(object sender, EventArgs e)
        {
            OnSearchManagerApproval();
        }

        private void OnSearchManagerApproval()
        {
            var capQuanLy = CapQuanLySearch.SelectedValue;
			int capQuanLyVal;
			if (!int.TryParse(capQuanLy, out capQuanLyVal))
			{
                InvalidCredentialsMessage.Text = "Cấp quản lý không có trong danh sách.";
                InvalidCredentialsMessage.Visible = true;
                ResetGvManagerApproval();
				return;
			}
            var idThanhVien = IdThanhVienSearch.Value.Trim();
            var managerApprovalDtos = DcapServiceUtil.SearchManagerApproval(capQuanLy, idThanhVien);
            if (managerApprovalDtos.Length > 0)
            {
                LoadManagerApproval(managerApprovalDtos);
                InvalidCredentialsMessage.Visible = false;
            }
            else
            {
                InvalidCredentialsMessage.Text = "Không tìm thấy duyệt cấp quản lý thỏa mãn";
                InvalidCredentialsMessage.Visible = true;
                ResetGvManagerApproval();
            }
        }

        private void ResetGvManagerApproval()
        {
            gvManagerApproval.DataSource = new ManagerApprovalDto[0];
            gvManagerApproval.DataBind();
        }

        private void LoadManagerApproval(ManagerApprovalDto[] managerApprovalDtos)
        {
            gvManagerApproval.DataSource = managerApprovalDtos;
            gvManagerApproval.DataBind();
        }

        int _stt = 1;

        public string GetStt()
        {
            return Convert.ToString(_stt++);
        }

        protected void gvManagerApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvManagerApproval.PageIndex = e.NewPageIndex;
            int pageIndex = e.NewPageIndex;
            int rowCount = gvManagerApproval.PageSize;
            _stt = pageIndex * rowCount + 1;
            OnSearchManagerApproval();
        }

        protected void OnClosePopupWindow(object sender, EventArgs e)
        {
            ManagerApprovalPopup.HidePopupWindow();
        }

        protected void imgBtnApprove_Click(object sender, ImageClickEventArgs e)
        {
            var row = (GridViewRow)(sender as Control).Parent.Parent;
			var managerLevel = row.Cells[1].Text;
            var userName = row.Cells[2].Text;
            var accountNumber = row.Cells[3].Text;
			ManagerLevelApproval.Value = managerLevel;
            AccountNumberApproval.Value = accountNumber;
            ManagerApprovalLabel.Text = "Duyệt cấp quản lý " + managerLevel + " cho thành viên " + userName + " [" + accountNumber + "]?";
            ManagerApprovalPopup.ShowPopupWindow();
        }

        protected void DuyetCapQuanLy_ApproveManager(object sender, EventArgs e)
        {
            InvalidCredentialsMessage.Visible = false;
			ManagerApprovalDto dto;
			if (GetManagerApprovalDto(out dto))
			{
				dto.ApprovedBy = User.Identity.Name;
				var returnCode = DcapServiceUtil.UpdateManagerApproval(dto);
				if (string.Compare(returnCode, "0", true) == 0)
				{
					OnSearchManagerApproval();
					InvalidCredentialsMessage.Text = "Duyệt cấp quản lý thành công!";
				}
				else
				{
					InvalidCredentialsMessage.Text = "Duyệt cấp quản lý không thành công!";
				}
			}
            else
            {
                InvalidCredentialsMessage.Text = "Duyệt cấp quản lý không thành công!";
            }
            InvalidCredentialsMessage.Visible = true;
            OnClosePopupWindow(sender, e);
        }
		
		private bool GetManagerApprovalDto(out ManagerApprovalDto dto)
		{
			dto = new ManagerApprovalDto();
			int managerLevel;
			long accountNumber;
			if (!int.TryParse(ManagerLevelApproval.Value, out managerLevel))
			{
				return false;
			}
            if (!long.TryParse(AccountNumberApproval.Value, out accountNumber))
			{
				return false;
			}
			dto.ManagerLevel = managerLevel;
			dto.AccountNumber = accountNumber;
			return true;
		}
    }
}