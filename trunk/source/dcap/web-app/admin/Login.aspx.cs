using System;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Security;
using web_app.common;

namespace web_app.admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ValidateUser(object sender, EventArgs e)
        {
            string captchaImageText = Convert.ToString(Session["CaptchaImageText"]);
            string confirmCaptcha = this.CaptchaImage.Text;
            if (string.Compare(captchaImageText, confirmCaptcha, true) != 0)
            {
                InvalidCredentialsMessage.Text = "Captcha is invalid. Please try again.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            var userName = this.UserName.Text;
            var password = this.Password.Text;
            var returnCode = DcapServiceUtil.login(userName, password);
            if (Convert.ToInt32(returnCode) == 0)
            {
                FormsAuthentication.RedirectFromLoginPage(userName.ToUpper(), this.RememberMe.Checked);
            }
            else
            {
                InvalidCredentialsMessage.Text = "Your username or password is invalid. Please try again.";
                InvalidCredentialsMessage.Visible = true;
            }
        }
    }
}
