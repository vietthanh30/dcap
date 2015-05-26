using System;
using System.Web.Security;
using web_app.common;

namespace web_app.admin
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        protected void RegisterUser_CreatingUser(object sender, EventArgs e)
        {
            var userName = RegisterUser.UserName;
            var password = RegisterUser.Password;
            var confirmPassword = RegisterUser.ConfirmPassword;
            var returnCode = DcapServiceUtil.createUser(userName, password, confirmPassword);
            if (Convert.ToInt32(returnCode) == 0)
            {
                RegisterUser_CreatedUser(sender, e);
            }
            else
            {
                Response.Redirect("Register.aspx");
            }
        }
    }
}
