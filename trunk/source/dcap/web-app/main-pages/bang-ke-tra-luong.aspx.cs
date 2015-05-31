using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using core_lib.common;
using web_app.common;

namespace web_app.main_pages
{
    public partial class bang_ke_tra_luong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BangKeTraLuong_SearchBangKe(object sender, EventArgs e)
        {
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKe(thangKeKhai);
            gvBangKe.DataSource = allBangKeDto;
            gvBangKe.DataBind();
        }

        protected void BangKeTraLuong_ExportExcel(object sender, EventArgs e)
        {

        }

        protected void BangKeTraLuong_ExportPDF(object sender, EventArgs e)
        {

        }

        protected void gvBangKe_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}