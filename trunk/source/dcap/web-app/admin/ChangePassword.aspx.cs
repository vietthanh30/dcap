using System;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.admin
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            ChangeUserPassword.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void ChangeUserPassword_ChangedPassword(object sender, EventArgs e)
        {
            string continueUrl = ChangeUserPassword.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        protected void ChangeUserPassword_ChangingPassword(object sender, EventArgs e)
        {
            var userName = User.Identity.Name;
            var currentPassword = ChangeUserPassword.CurrentPassword;
            var newPassword = ChangeUserPassword.NewPassword;
            var confirmNewPassword = ChangeUserPassword.ConfirmNewPassword;
            var returnCode = DcapServiceUtil.changePassword(userName, currentPassword, newPassword, confirmNewPassword);
            if (Convert.ToInt32(returnCode) == 0)
            {
                ChangeUserPassword_ChangedPassword(sender, e);
            }
            else
            {
                Response.Redirect("ChangePassword.aspx");
            }
        }
    }
}
