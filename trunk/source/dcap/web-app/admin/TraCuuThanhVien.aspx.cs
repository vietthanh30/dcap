using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using core_lib.common;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.admin
{
    public partial class TraCuuThanhVien : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TraCuuThanhVien_Search(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            var soCmnd = SoCmndSearch.Value.Trim();
            var idThanhVien = IdThanhVienSearch.Value.Trim();
            var hoTen = HoTenSearch.Value.Trim();
            if (string.IsNullOrEmpty(soCmnd) && string.IsNullOrEmpty(idThanhVien) && string.IsNullOrEmpty(hoTen))
            {
                InvalidCredentialsMessage.Text = "Phải nhập tối thiểu 1 thông tin tìm kiếm.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            var userDtos = DcapServiceUtil.SearchUserInfo(soCmnd, idThanhVien, hoTen);
            if (userDtos.Length > 0)
            {
                LoadUserInfo(userDtos);
            }
            else
            {
                InvalidCredentialsMessage.Text = "Không tìm thấy thành viên thỏa mãn";
                InvalidCredentialsMessage.Visible = true;
            }
        }
        
        private void LoadUserInfo(UserDto[] userDtos)
        {
            gvMemberInfo.DataSource = userDtos;
            gvMemberInfo.DataBind();
        }

        protected void TraCuuThanhVien_Save(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
        }
    }
}