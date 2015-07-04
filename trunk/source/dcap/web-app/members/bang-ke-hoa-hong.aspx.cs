using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using core_lib.common;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.members
{
    public partial class bang_ke_hoa_hong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            if (!UserUtil.IsQltvRole(userDto))
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
            if(!Page.IsPostBack)
            {
                ReportMonth.Value = DateUtil.GetDateTimeAsStringWithEnProvider(DateTime.Now,
                                                                               "MM/yyyy");
                OnSearchBangKe();
            }
            if (!Page.ClientScript.IsStartupScriptRegistered("invokeMeMaster"))
            {
                Page.ClientScript.RegisterStartupScript
                    (this.GetType(), "invokeMeMaster", "invokeMeMaster();", true);
            }
        }

        private void OnSearchBangKe()
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            var accountNumber = userDto.AccountNumber;
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKeHoaHong(accountNumber, thangKeKhai);
            gvBangKe.DataSource = allBangKeDto;
            gvBangKe.DataBind();
        }

        protected void BangKeHoaHong_SearchBangKe(object sender, EventArgs e)
        {
            OnSearchBangKe();
        }

        protected void BangKeTraLuong_ExportExcel(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            var accountNumber = userDto.AccountNumber;
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKeHoaHong(accountNumber, thangKeKhai);
            if (allBangKeDto.Length == 0)
            {
                return;
            }
            var fileName = String.Format("BKHH_{0:yyyyMMddHHmmssfff}", DateTime.Now) + ".xlsx";
            var fileDir = String.Format("BKHH_{0:yyyyMMdd}", DateTime.Now);
            var filePath = Server.MapPath("~/upload") + "\\" + fileDir + "\\" + fileName;
            string directory = filePath.Substring(0, filePath.LastIndexOf("\\"));// GetDirectory(Path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var dt = OnCreateDataTable(allBangKeDto);
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

        private DataTable OnCreateDataTable(HoaHongMemberDto[] allBangKeDto)
        {
            var columnNames = new[] {"stt", "Id thành viên", "Trực tiếp", "Cân cặp", "Hệ thống", "Quản lý", 
                "Thưởng thêm", "Tháng", "Tổng"};
            var columnTypes = new[] {typeof(int), typeof(string), typeof(double), typeof(double), typeof(double), typeof(double), 
                typeof(double), typeof(string), typeof(double)};
            var tableName = "HOA_HONG_MEMBER_VW";
            return CreateDataTable(tableName, columnNames, columnTypes, allBangKeDto);
        }

        private DataTable CreateDataTable(String tableName, String[] columnNames, Type[] columnTypes, HoaHongMemberDto[] allBangKeDto)
        {
            var dataTable = ExcelHelper.CreateEmptyDataTable(tableName, columnNames, columnTypes);
            foreach (var bangKeDto in allBangKeDto)
            {
                var dataRow = dataTable.NewRow();
                int index = 0;
                dataRow[columnNames[index++]] = bangKeDto.STT;
                dataRow[columnNames[index++]] = bangKeDto.AccountId;
                dataRow[columnNames[index++]] = bangKeDto.TrucTiep * 1000000;
                dataRow[columnNames[index++]] = bangKeDto.CanCap * 1000000;
                dataRow[columnNames[index++]] = bangKeDto.HeThong * 1000000;
                dataRow[columnNames[index++]] = bangKeDto.QuanLy * 1000000;
                dataRow[columnNames[index++]] = bangKeDto.ThuongThem * 1000000;
                dataRow[columnNames[index++]] = bangKeDto.Thang;
                dataRow[columnNames[index]] = bangKeDto.Tong * 1000000;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        protected void BangKeTraLuong_ExportDOC(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            var accountNumber = userDto.AccountNumber;
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKeHoaHong(accountNumber, thangKeKhai);
            if (allBangKeDto.Length == 0)
            {
                return;
            }
            var dt = OnCreateDataTable(allBangKeDto);
            var fileName = String.Format("BKHH_{0:yyyyMMddHHmmssfff}", DateTime.Now) + ".doc";
            var fileDir = String.Format("BKHH_{0:yyyyMMdd}", DateTime.Now);
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