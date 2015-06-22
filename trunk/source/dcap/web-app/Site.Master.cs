using System;
using core_lib.common;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            bool logged = userDto != null && Request.IsAuthenticated;
            if (logged)
            {
                UpdateUserInfo(userDto);
            }
            else
            {
                LeftContentAdmin.Visible = false;
                LeftContentKT.Visible = false;
                LeftContentTV.Visible = false;
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
            if (String.IsNullOrEmpty(imageUrl))
            {
                imageUrl = GioiTinhUtil.GetDefaultPhotoUrlBy(userDto.GioiTinh);
            }
            var headLoginName = userDto.FullName;
            string headLoginNameAdmin;
            var roleCode = UserUtil.GetRoleCode(userDto);
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
            UpdateLeftPanel(roleCode);
			
        }

        private void UpdateLeftPanel(string roleCode)
        {
            if (string.Compare(ConstUtil.QTHT, roleCode, true) == 0)
            {
                LeftContentAdmin.Visible = true;
                LeftContentKT.Visible = false;
                LeftContentTV.Visible = false;
            }
            if (string.Compare(ConstUtil.QLKT, roleCode, true) == 0)
            {
                LeftContentAdmin.Visible = false;
                LeftContentKT.Visible = true;
                LeftContentTV.Visible = false;
            }
            if (string.Compare(ConstUtil.QLTV, roleCode, true) == 0)
            {
                LeftContentAdmin.Visible = false;
                LeftContentKT.Visible = false;
                LeftContentTV.Visible = true;
            }
        }
    }
}
