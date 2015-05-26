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
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        protected void CreateUser(object sender, EventArgs e)
        {
            var userName = RegisterUser.UserName;
            var password = RegisterUser.Password;
            var confirmPassword = RegisterUser.ConfirmPassword;
            var returnCode = DcapServiceUtil.createUser(userName, password, confirmPassword);
            if (string.Compare(returnCode, "Create User success", true) == 0)
            {
                RegisterUser_CreatedUser(sender, e);
            }
            else
            {
                RegisterUser.InstructionText = returnCode;
                Response.Redirect("Register.aspx");
            }
        }
    }
}
