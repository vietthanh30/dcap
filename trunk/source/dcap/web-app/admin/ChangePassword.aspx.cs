using System;
using System.Web.Security;
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
            if (newPassword.Length < Membership.MinRequiredPasswordLength)
            {
                InvalidCredentialsMessage.Text = "Mật khẩu mới quá ngắn. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            var returnCode = DcapServiceUtil.changePassword(userName, currentPassword, newPassword, confirmNewPassword);
            if (Convert.ToInt32(returnCode) == 0)
            {
                InvalidCredentialsMessage.Visible = false;
                ChangeUserPassword_ChangedPassword(sender, e);
            }
            else
            {
                var code = Convert.ToInt32(returnCode);
                switch (code)
                {
                    case -1:
                        InvalidCredentialsMessage.Text = "Chưa nhập tên đăng nhập.";
                        break;
                    case -2:
                        InvalidCredentialsMessage.Text = "Chưa nhập mật khẩu cũ.";
                        break;
                    case -3:
                        InvalidCredentialsMessage.Text = "Người dùng không tồn tại.";
                        break;
                    case -4:
                        InvalidCredentialsMessage.Text = "Mật khẩu cũ không đúng.";
                        break;
                    case -5:
                        InvalidCredentialsMessage.Text = "Mật khẩu mới trùng mật khẩu cũ.";
                        break;
                    case -6:
                        InvalidCredentialsMessage.Text = "Chưa nhập mật khẩu xác nhận.";
                        break;
                    case -7:
                        InvalidCredentialsMessage.Text = "Mật khẩu mới không trùng mật khẩu xác nhận.";
                        break;
                    default:
                        InvalidCredentialsMessage.Text = "Đổi mật khẩu không thành công.";
                        break;
                }
                InvalidCredentialsMessage.Visible = true;
            }
        }
    }
}
