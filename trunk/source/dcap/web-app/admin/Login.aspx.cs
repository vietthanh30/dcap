using System;
using System.Web;
using web_app.common;

namespace web_app.admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void CheckUser(object sender, EventArgs e)
        {
            var userName = LoginUser.UserName;
            var password = LoginUser.Password;
            var returnCode = DcapServiceUtil.login(userName, password);
        }
    }
}
