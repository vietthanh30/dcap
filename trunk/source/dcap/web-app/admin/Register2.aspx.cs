using System;
using core_lib.common;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.admin
{
    public partial class Register2 : System.Web.UI.Page
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
            int code;
            var error = int.TryParse(returnCode, out code);
            if (!error)
            {
                AccountCode.Text = "Tên đăng nhập: " + returnCode + "/" + ConstUtil.DEFAULT_PASSWORD;
                AccountCode.Visible = true;
                ResetAccountInfo();
            }
            else
            {
                switch (code)
                {
                    case -1:
                        InvalidCredentialsMessage.Text = "Chưa nhập tên đăng nhập.";
                        break;
                    case -2:
                        InvalidCredentialsMessage.Text = "Chưa nhập tên đầy đủ.";
                        break;
                    case -3:
                        InvalidCredentialsMessage.Text = "Chưa chọn quyền người dùng.";
                        break;
                    case -4:
                        InvalidCredentialsMessage.Text = "Người dùng đã tồn tại.";
                        break;
                    case -5:
                        InvalidCredentialsMessage.Text = "Đăng ký người dùng không thành công.";
                        break;
                    case -6:
                        InvalidCredentialsMessage.Text = "Đăng ký quyền người dùng không thành công.";
                        break;
                    default:
                        InvalidCredentialsMessage.Text = "Đăng ký không thành công.";
                        break;
                }
                InvalidCredentialsMessage.Visible = true;
            }
        }

        private void ResetAccountInfo()
        {
            HoTen.Value = string.Empty;
            TenDangNhap.Value = string.Empty;
            UserRole.ClearSelection();
            InvalidCredentialsMessage.Visible = false;
        }
    }
}
