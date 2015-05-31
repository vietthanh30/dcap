using System;
using System.Web.Security;
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
            }
        }
    }
}
