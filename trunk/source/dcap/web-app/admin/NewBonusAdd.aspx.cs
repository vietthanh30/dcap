using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.admin
{
    public partial class NewBonusAdd : System.Web.UI.Page
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
        }
    }
}