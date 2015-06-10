using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.main_pages
{
    public partial class tong_quan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                FillToPage();
            }
        }

        private void FillToPage()
        {
            long memberAmount = DcapServiceUtil.GetMemberAmount();
            MemberAmount.Text = memberAmount.ToString();
            long accountAmount = DcapServiceUtil.GetAccountAmount();
            AccountAmount.Text = accountAmount.ToString();
            long managerAmount = DcapServiceUtil.GetManagerAmount();
            ManagerAmount.Text = managerAmount.ToString();
            long managerL6Amount = DcapServiceUtil.GetManagerL6Amount();
            ManagerL6Amount.Text = managerL6Amount.ToString();
            UserDto[] newMemberList = DcapServiceUtil.GetNewMemberList();
            FillToNewMemberList(newMemberList);
            UserDto[] newManagerList = DcapServiceUtil.GetNewManagerList();
            FillToNewManagerList(newManagerList);
            int reportYear = DcapServiceUtil.GetReportYear();
            ReportYearLabel.Text = reportYear.ToString();
            AccountBonusDto[] acountBonusList = DcapServiceUtil.GetAcountBonusList();
            FillToAccountBonusTable(acountBonusList);
        }

        private void FillToAccountBonusTable(AccountBonusDto[] acountBonusList)
        {
        }

        private void FillToNewManagerList(UserDto[] newManagerList)
        {
            NewManagerAmount.Text = newManagerList.Length.ToString();
        }

        private void FillToNewMemberList(UserDto[] newMemberList)
        {
            NewMemberAmount.Text = newMemberList.Length.ToString();
        }
    }
}