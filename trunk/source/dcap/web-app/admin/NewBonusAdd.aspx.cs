﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.admin
{
    public partial class NewBonusAdd : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                OnSearchBonusApproval();
            }
        }

        protected void NewBonusAdd_Search(object sender, EventArgs e)
        {
            OnSearchBonusApproval();
        }

        private void OnSearchBonusApproval()
        {
            var accountNumber = IdThanhVienSearch.Value.Trim();
            if (!string.IsNullOrEmpty(accountNumber) && !DcapServiceUtil.IsValidAccountNumber(accountNumber))
            {
                InvalidCredentialsMessage.Text = "Id thành viên không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                ResetGvBonusApproval();
                return;
            }
            var userName = UserNameSearch.Value.Trim();
            var isApproved = IsApprovedSearch.SelectedValue;
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

        protected void NewBonusAdd_PreAddNewBonus(object sender, EventArgs e)
        {
            NewBonusAddPopup.ShowPopupWindow();
        }

        protected void OnClosePopupWindow(object sender, EventArgs e)
        {
            NewBonusAddPopup.HidePopupWindow();
        }

        protected void NewBonusAdd_AddNewBonus(object sender, EventArgs e)
        {
            var accountNumber = AccountNumber.Value.Trim();
            var bonusAmount = BonusAmount.Value.Trim();
			var bonusType = "ADD";
			var isApproved = "N";
			double bonusAmountVal;
			if (!DcapServiceUtil.IsValidAccountNumber(accountNumber))
			{
                InvalidCredentialsMessage2.Text = "Id thành viên không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage2.Visible = true;
				return;
			}
			if (!double.TryParse(bonusAmount, out bonusAmountVal))
			{
                InvalidCredentialsMessage2.Text = "Điểm thưởng không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage2.Visible = true;
				return;
			}
            if (accountNumber.Length > 10)
            {
                InvalidCredentialsMessage2.Text = "Id thành viên quá dài (Nhiều hơn 10 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage2.Visible = true;
                return;
            }
            if (bonusAmount.Length > 10)
            {
                InvalidCredentialsMessage2.Text = "Điểm thưởng quá dài (Nhiều hơn 10 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage2.Visible = true;
                return;
            }
            var userName = User.Identity.Name;
			var dto = CreateBonusApprovalDto(accountNumber, bonusAmountVal, bonusType, isApproved, userName);
            var returnCode = DcapServiceUtil.CreateBonusApproval(dto);
            int code;
            var status = int.TryParse(returnCode, out code);
            if (status && code == 0)
            {
                InvalidCredentialsMessage2.Visible = false;
				OnClosePopupWindow(sender, e);
				OnSearchBonusApproval();
                InvalidCredentialsMessage.Text = "Thêm mới Thưởng thêm thành công.";
                InvalidCredentialsMessage.Visible = true;
            }
            else
            {
                switch (code)
                {
                    case -1:
                        InvalidCredentialsMessage2.Text = "Thành viên không tồn tại.";
                        break;
                    default:
                        InvalidCredentialsMessage2.Text = "Thêm mới không thành công.";
                        break;
                }
                InvalidCredentialsMessage2.Visible = true;
            }
        }
		
		private BonusApprovalDto CreateBonusApprovalDto(string accountNumber, double bonusAmount, string bonusType, string isApproved, string createdBy)
		{
			var dto = new BonusApprovalDto();
			dto.AccountNumber = accountNumber;
			dto.BonusAmount = bonusAmount;
			dto.BonusType = bonusType;
			dto.IsApproved = isApproved;
			dto.CreatedBy = createdBy;
			return dto;
		}
    }
}