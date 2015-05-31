using System;
using web_app.DcapServiceReference;

namespace web_app
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            bool logged = userDto != null;
            if (logged)
            {
                UpdateUserInfo(userDto);
            }
            HeadLoginImg1.Visible = logged;
            HeadLoginImg2.Visible = logged;
            HeadLoginImg3.Visible = logged;
            HeadLoginName1.Visible = logged;
            HeadLoginName2.Visible = logged;
            HeadLoginName3.Visible = logged;
            HeadLoginStatus.Visible = !logged;

        }

        private void UpdateUserInfo(UserDto userDto)
        {
            var imageUrl = userDto.ImageUrl;
            var headLoginName = userDto.FullName;
            string headLoginNameAdmin;
            var roleCode = GetRoleCode(userDto);
            if (String.IsNullOrEmpty(roleCode))
            {
                headLoginNameAdmin = userDto.FullName;
            }
            else
            {
                headLoginNameAdmin = userDto.FullName + " - " + roleCode;
            }
            HeadLoginImg1.Src = imageUrl;
            HeadLoginImg2.Src = imageUrl;
            HeadLoginImg3.Src = imageUrl;
            HeadLoginName1.Text = headLoginName;
            HeadLoginName2.Text = headLoginNameAdmin;
            HeadLoginName3.Text = headLoginName;
        }

        private string GetRoleCode(UserDto userDto)
        {
            var allRoles = userDto.AllRoles;
            if (allRoles.Length == 0)
            {
                return string.Empty;
            }
            return allRoles[0].RoleCode;
        }
    }
}
