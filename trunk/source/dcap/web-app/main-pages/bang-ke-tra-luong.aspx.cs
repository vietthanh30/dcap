using System;
using System.Collections;
using System.Collections.Generic;
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
                Response.Redirect("~/Default.aspx");
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
            InvalidCredentialsMessage.Visible = false;
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKe(thangKeKhai);
            gvBangKe.DataSource = allBangKeDto;
            gvBangKe.DataBind();
            Hashtable mapPaidUserName = CreateMapPaidUserName(allBangKeDto);
            UpdateKyNhanStatus(mapPaidUserName);
        }

        private Hashtable CreateMapPaidUserName(BangKeDto[] allBangKeDto)
        {
            Hashtable map = new Hashtable();
            foreach (BangKeDto dto in allBangKeDto)
            {
                map.Add(dto.UserName, dto.IsPaid);
            }
            return map;
        }

        private void UpdateKyNhanStatus(Hashtable mapPaidUserName)
        {
            for (int i = 0; i < gvBangKe.Rows.Count; i++)
            {
                GridViewRow row = gvBangKe.Rows[i];
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    var userName = row.Cells[3].Text;
                    var isPaid = mapPaidUserName[userName];
                    chkRow.Checked = isPaid != null & int.Parse(isPaid.ToString()) == 1;
                }
            }
        }

        protected void BangKeTraLuong_SearchBangKe(object sender, EventArgs e)
        {
            OnSearchBangKe();
        }

        protected void BangKeTraLuong_ExportExcel(object sender, EventArgs e)
        {
            InvalidCredentialsMessage.Visible = false;
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKe(thangKeKhai);
            if (allBangKeDto.Length == 0)
            {
                return;
            }
            var columnNames = new[] {"stt", "Tên nhân viên", "Tên đăng nhập", "Số cmnd", "Địa chỉ", "Số TK", 
                "Ngân Hàng", "Số ĐT", "Hệ thống", "Quản lý", "Thưởng thêm", "Tổng tiền", "Thuế 10%", "Thực nhận", "Tháng", "Ký nhận"};
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
                dataRow[columnNames[index++]] = bangKeDto.HeThong * 1000000;
                dataRow[columnNames[index++]] = bangKeDto.QuanLy * 1000000;
                dataRow[columnNames[index++]] = bangKeDto.ThuongThem * 1000000;
                dataRow[columnNames[index++]] = bangKeDto.SoTien * 1000000;
                dataRow[columnNames[index++]] = bangKeDto.SoTien * 100000;
                dataRow[columnNames[index++]] = bangKeDto.SoTien * 900000;
                dataRow[columnNames[index]] = bangKeDto.Thang;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        protected void BangKeTraLuong_ExportDOC(object sender, EventArgs e)
        {
            InvalidCredentialsMessage.Visible = false;
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKe(thangKeKhai);
            if (allBangKeDto.Length == 0)
            {
                return;
            }
            var columnNames = new[] {"stt", "Tên nhân viên", "Tên đăng nhập", "Số cmnd", "Địa chỉ", "Số TK", 
                "Ngân Hàng", "Số ĐT", "Hệ thống", "Quản lý", "Thưởng thêm", "Tổng tiền", "Thuế 10%", "Thực nhận", "Tháng", "Ký nhận"};
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

        protected void BangKeTraLuong_PreUpdatePaid(object sender, EventArgs e)
        {
            List<BangKeDto> bangKeDtos = GetAllBangKeDtos();
            if (bangKeDtos.Count == 0)
            {
                return;
            }
            UpdatePaidLabel.Text = "Cập nhật trả lương cho các thành viên được chọn?";
            UpdatePaidPopup.ShowPopupWindow();
        }

        protected void BangKeTraLuong_CancelUpdatePaid(object sender, EventArgs e)
        {
            OnSearchBangKe();
            UpdatePaidPopup.HidePopupWindow();
        }

        protected void OnClosePopupWindow(object sender, EventArgs e)
        {
            UpdatePaidPopup.HidePopupWindow();
        }

        protected void BangKeTraLuong_UpdatePaid(object sender, EventArgs e)
        {
            InvalidCredentialsMessage.Visible = false;
            List<BangKeDto> bangKeDtos = GetAllBangKeDtos();
            var returnCode = DcapServiceUtil.UpdatePaid(bangKeDtos.ToArray());
            if (string.Compare(returnCode, "0", true) == 0)
            {
                OnSearchBangKe();
                InvalidCredentialsMessage.Text = "Cập nhật trả lương thành công!";
            }
            else
            {
                InvalidCredentialsMessage.Text = "Cập nhật trả lương không thành công!";
            }
            InvalidCredentialsMessage.Visible = true;
            UpdatePaidPopup.HidePopupWindow();
        }

        private List<BangKeDto> GetAllBangKeDtos()
        {
            var bangKeDtos = new List<BangKeDto>();
            foreach (GridViewRow row in gvBangKe.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    var bangKeDto = new BangKeDto();
                    bangKeDto.UserName = row.Cells[3].Text;
                    bangKeDto.Thang = row.Cells[10].Text;
                    bangKeDto.IsPaid = chkRow.Checked ? 1 : -1;
                    bangKeDtos.Add(bangKeDto);
                }
            }
            return bangKeDtos;
        }
    }
}