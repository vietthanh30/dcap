using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
        }

        protected void TraCuuThanhVien_Search(object sender, EventArgs e)
        {
            OnSearchThanhVien();
        }

        private void OnSearchThanhVien()
        {
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

        protected void TraCuuThanhVien_ExportToWord(object sender, EventArgs e)
        {
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
            if (userDtos.Length == 0)
            {
                return;
            }
            var columnNames = new[] {"Họ tên", "Số cmnd", "Id thành viên", "Tên đăng nhập"};
            var tableName = "MEMBERr_INFO";
            var dt = CreateDataTable(tableName, columnNames, userDtos);
            var fileName = String.Format("TCTV_{0:yyyyMMddHHmmssfff}", DateTime.Now) + ".doc";
            var fileDir = String.Format("TCTV_{0:yyyyMMdd}", DateTime.Now);
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

        private DataTable CreateDataTable(String tableName, String[] columnNames, UserDto[] userDtos)
        {
            var dataTable = new DataTable(tableName);
            foreach (var columnName in columnNames)
            {
                dataTable.Columns.Add(columnName);
            }
            foreach (var userDto in userDtos)
            {
                var dataRow = dataTable.NewRow();
                int index = 0;
                dataRow[columnNames[index++]] = userDto.FullName;
                dataRow[columnNames[index++]] = userDto.SoCmnd;
                dataRow[columnNames[index++]] = string.Format("{0:0000000}", userDto.AccountNumber);
                dataRow[columnNames[index++]] = userDto.UserName;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        protected void TraCuuThanhVien_ExportToExcel(object sender, EventArgs e)
        {
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
            if (userDtos.Length == 0)
            {
                return;
            }
            var columnNames = new[] { "Họ tên", "Số cmnd", "Id thành viên", "Tên đăng nhập" };
            var tableName = "MEMBERr_INFO";
            var dt = CreateDataTable(tableName, columnNames, userDtos);
            var fileName = String.Format("TCTV_{0:yyyyMMddHHmmssfff}", DateTime.Now) + ".xlsx";
            var fileDir = String.Format("TCTV_{0:yyyyMMdd}", DateTime.Now);
            var filePath = Server.MapPath("~/upload") + "\\" + fileDir + "\\" + fileName;
            string directory = filePath.Substring(0, filePath.LastIndexOf("\\"));// GetDirectory(Path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

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

        int _stt = 1;

        public string GetStt()
        {
            return Convert.ToString(_stt++);
        }

        protected void gvMemberInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMemberInfo.PageIndex = e.NewPageIndex;
            int pageIndex = e.NewPageIndex;
            int rowCount = gvMemberInfo.PageSize;
            _stt = pageIndex * rowCount + 1;
            OnSearchThanhVien();
        }

    }
}