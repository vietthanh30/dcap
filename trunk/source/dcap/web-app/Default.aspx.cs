using System;
using System.Web.Security;
using core_lib.common;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (!User.Identity.IsAuthenticated || userDto == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Session["UserDto"] = null;
                    FormsAuthentication.SignOut();
                }
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
            AccountBonusTable.Text = "<table class=\"table no-margin\">";
            AccountBonusTable.Text += "<thead>";
            AccountBonusTable.Text += "<tr>";
            AccountBonusTable.Text += "<th>Tháng</th>";
            AccountBonusTable.Text += "<th>Tổng số</th>";
            AccountBonusTable.Text += "<th>Trạng Thái</th>";
            AccountBonusTable.Text += "</tr>";
            AccountBonusTable.Text += "</thead>";
            AccountBonusTable.Text += "<tbody>";
            foreach (var accountBonusDto in acountBonusList)
            {
                AccountBonusTable.Text += "<tr>";
                var dThang = DateUtil.GetDateTime(accountBonusDto.Thang);
                string thang;
                if (dThang != null)
                {
                    thang = "Tháng " + ((DateTime)dThang).Month;
                }
                else
                {
                    thang = string.Empty;
                }
                AccountBonusTable.Text += "<td><a href=\"#\">" + thang + "</a></td>";
                AccountBonusTable.Text += "<td>" + accountBonusDto.Tong + "</td>";
                var isPaid = accountBonusDto.IsPaid;
                if (isPaid == 1)
                {
                    AccountBonusTable.Text += "<td><span class=\"label label-success\">Đã trả thưởng</span></td>";
                }
                else
                {
                    AccountBonusTable.Text += "<td><span class=\"label label-warning\">Chưa trả thưởng</span></td>";
                }
                AccountBonusTable.Text += "</tr>";
            }
            AccountBonusTable.Text += "</tbody>";
            AccountBonusTable.Text += "</table>";
        }

        private void FillToNewManagerList(UserDto[] newManagerList)
        {
            NewManagerAmount.Text = newManagerList.Length + " Quản lý";
            NewManagerList.Text = "<ul class=\"users-list clearfix\">";
            foreach (var userDto in newManagerList)
            {
                NewManagerList.Text += "<li>";
                var imageUrl = userDto.ImageUrl;
                if (string.IsNullOrEmpty(imageUrl))
                {
                    imageUrl = "~/dist/img/avatar5.png";
                }
                imageUrl = imageUrl.Replace("~", "");
                NewManagerList.Text += "<img src=\"" + imageUrl + "\" runat=\"server\" alt=\"User Image\"/>";
                NewManagerList.Text += "<a class=\"users-list-name\" href=\"#\" title=\"" + userDto.FullName + "\">" + userDto.FullName + "</a>";
                var createdDate = DateUtil.GetDateTimeAsDdmmyyyy(userDto.CreatedDate);
                NewManagerList.Text += "<span class=\"users-list-date\">" + createdDate + "</span>";
                NewManagerList.Text += "</li>";
            }
            NewManagerList.Text += "</ul>";
        }

        private void FillToNewMemberList(UserDto[] newMemberList)
        {
            NewMemberAmount.Text = newMemberList.Length + " Thành viên";
            NewMemberList.Text = "<ul class=\"users-list clearfix\">";
            foreach (var userDto in newMemberList)
            {
                NewMemberList.Text += "<li>";
                var imageUrl = userDto.ImageUrl;
                if (string.IsNullOrEmpty(imageUrl))
                {
                    imageUrl = "~/dist/img/avatar5.png";
                }
                imageUrl = imageUrl.Replace("~", "");
                NewMemberList.Text += "<img src=\"" + imageUrl + "\" runat=\"server\" alt=\"User Image\"/>";
                NewMemberList.Text += "<a class=\"users-list-name\" href=\"#\" title=\"" + userDto.FullName + "\">" + userDto.FullName + "</a>";
                var createdDate = DateUtil.GetDateTimeAsDdmmyyyy(userDto.CreatedDate);
                NewMemberList.Text += "<span class=\"users-list-date\">" + createdDate + "</span>";
                NewMemberList.Text += "</li>";
            }
            NewMemberList.Text += "</ul>";
        }
    }
}
