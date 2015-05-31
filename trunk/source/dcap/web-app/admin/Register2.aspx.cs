using System;
using core_lib.common;
using web_app.common;

namespace web_app.admin
{
    public partial class Register2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ContinueDestinationPageUrl.Value = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            string continueUrl = ContinueDestinationPageUrl.Value;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        protected void RegisterUser_CreatingUser(object sender, EventArgs e)
        {
            var fullName = HoTen.Value.Trim();
            var userName = TenDangNhap.Value.Trim();
            var roleCode = UserRole.SelectedValue.Trim();
            var createdBy = User.Identity.Name;
            var returnCode = DcapServiceUtil.CreateUserAdmin(userName, fullName, roleCode, createdBy);
            if (string.Compare(returnCode, "-1", true) != 0)
            {
                AccountCode.Text = "Tên đăng nhập: " + returnCode + "/" + ConstUtil.DEFAULT_PASSWORD;
                AccountCode.Visible = true;
            }
            else
            {
                InvalidCredentialsMessage.Text = "Đăng ký không thành công.";
                InvalidCredentialsMessage.Visible = true;
            }
        }
    }
}
