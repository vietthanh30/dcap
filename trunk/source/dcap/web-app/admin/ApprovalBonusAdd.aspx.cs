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
    public partial class ApprovalBonusAdd : System.Web.UI.Page
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

        protected void ApprovalBonusAdd_Search(object sender, EventArgs e)
        {
            OnSearchBonusApproval();
        }

        private void OnSearchBonusApproval()
        {
            var accountNumber = IdThanhVienSearch.Value.Trim();
            var userName = UserNameSearch.Value.Trim();
            var isApproved = "N";
            var bonusApprovalDtos = DcapServiceUtil.SearchBonusApproval(accountNumber, userName, isApproved);
            if (bonusApprovalDtos.Length > 0)
            {
                LoadBonusApproval(bonusApprovalDtos);
                InvalidCredentialsMessage.Visible = false;
            }
            else
            {
                InvalidCredentialsMessage.Text = "Không tìm thấy thưởng thêm thỏa mãn";
                InvalidCredentialsMessage.Visible = true;
                ResetGvBonusApproval();
            }
        }

        private void ResetGvBonusApproval()
        {
            gvBonusApproval.DataSource = new BonusApprovalDto[0];
            gvBonusApproval.DataBind();
        }

        private void LoadBonusApproval(BonusApprovalDto[] bonusApprovalDtos)
        {
            gvBonusApproval.DataSource = bonusApprovalDtos;
            gvBonusApproval.DataBind();
        }

        int _stt = 1;

        public string GetStt()
        {
            return Convert.ToString(_stt++);
        }

        protected void gvBonusApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBonusApproval.PageIndex = e.NewPageIndex;
            int pageIndex = e.NewPageIndex;
            int rowCount = gvBonusApproval.PageSize;
            _stt = pageIndex * rowCount + 1;
            OnSearchBonusApproval();
        }

        protected void OnClosePopupWindow(object sender, EventArgs e)
        {
            ApprovalBonusPopup.HidePopupWindow();
        }

        protected void imgBtnApprove_Click(object sender, ImageClickEventArgs e)
        {
            var row = (GridViewRow)(sender as Control).Parent.Parent;
            var accountNumber = row.Cells[1].Text;
            var userName = row.Cells[2].Text;
            AccountNumberApproval.Value = accountNumber;
            ApprovalBonusLabel.Text = "Duyệt thưởng thêm cho thành viên " + userName + " [" + accountNumber + "]?";
            ApprovalBonusPopup.ShowPopupWindow();
        }

        protected void ApprovalBonusAdd_ApproveBonusAdd(object sender, EventArgs e)
        {
            InvalidCredentialsMessage.Visible = false;
			BonusApprovalDto dto;
			if (GetBonusApprovalDto(out dto))
			{
				dto.ApprovedBy = User.Identity.Name;
				dto.BonusType = "ADD";
				var returnCode = DcapServiceUtil.UpdateBonusApproval(dto);
				if (string.Compare(returnCode, "0", true) == 0)
				{
					OnSearchBonusApproval();
					InvalidCredentialsMessage.Text = "Duyệt thưởng thêm thành công!";
				}
				else
				{
					InvalidCredentialsMessage.Text = "Duyệt thưởng thêm không thành công!";
				}
			}
            else
            {
                InvalidCredentialsMessage.Text = "Duyệt thưởng thêm không thành công!";
            }
            InvalidCredentialsMessage.Visible = true;
            OnClosePopupWindow(sender, e);
        }
		
		private bool GetBonusApprovalDto(out BonusApprovalDto dto)
		{
			dto = new BonusApprovalDto();
			long accountNumber;
            if (!long.TryParse(AccountNumberApproval.Value, out accountNumber))
			{
				return false;
			}
			dto.AccountNumber = accountNumber;
			return true;
		}
    }
}