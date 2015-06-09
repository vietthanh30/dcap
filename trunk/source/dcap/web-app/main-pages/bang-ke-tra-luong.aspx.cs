using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using core_lib.common;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.main_pages
{
    public partial class bang_ke_tra_luong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            if (!UserUtil.IsQthtRole(userDto) && !UserUtil.IsQlktRole(userDto))
            {
                Response.Redirect("~/admin/Default.aspx");
                return;
            }
            if(!Page.IsPostBack)
            {
                ReportMonth.Value = DateUtil.GetDateTimeAsStringWithEnProvider(DateTime.Now,
                                                                               "MM/yyyy");
                OnSearchBangKe();
            }
        }

        private void OnSearchBangKe()
        {
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKe(thangKeKhai);
            gvBangKe.DataSource = allBangKeDto;
            gvBangKe.DataBind();
        }

        protected void BangKeTraLuong_SearchBangKe(object sender, EventArgs e)
        {
            OnSearchBangKe();
        }

        protected void BangKeTraLuong_ExportExcel(object sender, EventArgs e)
        {
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKe(thangKeKhai);
            if (allBangKeDto.Length == 0)
            {
                return;
            }
            var columnNames = new[] {"stt", "Tên nhân viên", "Tên đăng nhập", "Số cmnd", "Địa chỉ", "Số TK", 
                "Ngân Hàng", "Số ĐT", "Tổng tiền", "Tháng", "Ký nhận"};
            var fileName = String.Format("BKTL_{0:yyyyMMddHHmmssfff}", DateTime.Now) + ".xlsx";
            var fileDir = String.Format("BKTL_{0:yyyyMMdd}", DateTime.Now);
            var filePath = Server.MapPath("~/upload") + "\\" + fileDir + "\\" + fileName;
            string directory = filePath.Substring(0, filePath.LastIndexOf("\\"));// GetDirectory(Path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var tableName = "BANG_KE_VW";
            var dt = CreateDataTable(tableName, columnNames, allBangKeDto);
            ExcelHelper excelFacade = new ExcelHelper();
            excelFacade.Create(filePath, dt);

            FileInfo file = new FileInfo(filePath);
            Response.Clear();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Add header, give a default file name for "File Download/Store as"
            Response.AddHeader("Content-Disposition", "attachment; filename="
                + Server.UrlEncode(file.Name));
            //Add header, set file size to enable browser display download progress
            Response.AddHeader("Content-Length", file.Length.ToString());
            //Set the return string is unavailable reading for client, and must be downloaded
            Response.ContentType = "application/vnd.ms-excel";
            //Send file string to client 
            Response.WriteFile(file.FullName);
            //Stop execute  
            Response.End();
            // Cleanup
            file.Delete();
        }

        private DataTable CreateDataTable(String tableName, String[] columnNames, BangKeDto[] allBangKeDto)
        {
            var dataTable = new DataTable(tableName);
            foreach (var columnName in columnNames)
            {
                dataTable.Columns.Add(columnName);
            }
            foreach (var bangKeDto in allBangKeDto)
            {
                var dataRow = dataTable.NewRow();
                int index = 0;
                dataRow[columnNames[index++]] = bangKeDto.STT;
                dataRow[columnNames[index++]] = bangKeDto.HoTen;
                dataRow[columnNames[index++]] = bangKeDto.UserName;
                dataRow[columnNames[index++]] = bangKeDto.SoCmnd;
                dataRow[columnNames[index++]] = bangKeDto.DiaChi;
                dataRow[columnNames[index++]] = bangKeDto.SoTaiKhoan;
                dataRow[columnNames[index++]] = bangKeDto.ChiNhanhNH;
                dataRow[columnNames[index++]] = bangKeDto.SoDienThoai;
                dataRow[columnNames[index++]] = bangKeDto.SoTien*1000000;
                dataRow[columnNames[index]] = bangKeDto.Thang;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        protected void BangKeTraLuong_ExportDOC(object sender, EventArgs e)
        {
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKe(thangKeKhai);
            if (allBangKeDto.Length == 0)
            {
                return;
            }
            var columnNames = new[] {"stt", "Tên nhân viên", "Tên đăng nhập", "Số cmnd", "Địa chỉ", "Số TK", 
                "Ngân Hàng", "Số ĐT", "Tổng tiền", "Tháng", "Ký nhận"};
            var tableName = "BANG_KE_VW";
            var dt = CreateDataTable(tableName, columnNames, allBangKeDto);
            var fileName = String.Format("BKTL_{0:yyyyMMddHHmmssfff}", DateTime.Now) + ".doc";
            var fileDir = String.Format("BKTL_{0:yyyyMMdd}", DateTime.Now);
            var filePath = Server.MapPath("~/upload") + "\\" + fileDir + "\\" + fileName;
            string directory = filePath.Substring(0, filePath.LastIndexOf("\\"));// GetDirectory(Path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            DataGrid dataGrd = new DataGrid();
            dataGrd.DataSource = dt;
            dataGrd.DataBind();
            dataGrd.RenderControl(htmlWrite);
            StreamWriter vw = new StreamWriter(filePath, true, Encoding.UTF8);
            stringWriter.ToString().Normalize();
            vw.Write(stringWriter.ToString());
            vw.Flush();
            vw.Close();

            FileInfo file = new FileInfo(filePath);
            Response.Clear();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Add header, give a default file name for "File Download/Store as"
            Response.AddHeader("Content-Disposition", "attachment; filename="
                + Server.UrlEncode(file.Name));
            //Add header, set file size to enable browser display download progress
            Response.AddHeader("Content-Length", file.Length.ToString());
            //Set the return string is unavailable reading for client, and must be downloaded
            Response.ContentType = "application/vnd.ms-word";
            //Send file string to client 
            Response.WriteFile(file.FullName);
            //Stop execute  
            Response.End();
            // Cleanup
            file.Delete();
        }
        
        private int _stt = 1;
        
        public string GetStt()
        {
            return Convert.ToString(_stt++);
        }

        protected void gvBangKe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBangKe.PageIndex = e.NewPageIndex;
            int pageIndex = e.NewPageIndex;
            int rowCount = gvBangKe.PageSize;
            _stt = pageIndex * rowCount + 1;
            OnSearchBangKe();
        }
    }
}