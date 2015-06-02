﻿using System;
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

namespace web_app.main_pages
{
    public partial class bang_ke_tra_luong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ReportMonth.Value = DateUtil.GetDateTimeAsStringWithEnProvider(DateTime.Now,
                                                                               "MM/yyyy");
                OnSearchBangKe();
            }
        }

        private void OnSearchBangKe()
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
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
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            var thangKeKhai = DateUtil.GetDateTime(ReportMonth.Value.Trim());
            var allBangKeDto = DcapServiceUtil.SearchBangKe(thangKeKhai);
            if (allBangKeDto.Length == 0)
            {
                return;
            }
            var dir = String.Format("BKTL_{0:yyyyMMdd}", DateTime.Now);
            var fileName = String.Format("BKTL_{0:yyyyMMddHHmmssfff}", DateTime.Now) + ".xls";
            var filePath = Server.MapPath("~/upload") + "\\" + dir + "\\" + fileName;
            var columnNames = new[] {"stt", "Tên nhân viên", "Số cmnd", "Địa chỉ", "Số TK", 
                "Ngân Hàng", "Số ĐT", "Tổng tiền", "Tháng", "Ký nhận"};
            var sheetName = "Tra cứu bảng lương";
            var ds = CreateDataSet(sheetName, columnNames, allBangKeDto);

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            DataGrid dataGrd = new DataGrid();
            dataGrd.DataSource = ds;
            dataGrd.DataBind();
            dataGrd.RenderControl(htmlWrite);
            string directory = filePath.Substring(0, filePath.LastIndexOf("\\"));// GetDirectory(Path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
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
            Response.ContentType = "application/vnd.ms-excel";
            //Send file string to client 
            Response.WriteFile(file.FullName);
            //Stop execute  
            Response.End();
            // Cleanup
            file.Delete();
        }

        private DataSet CreateDataSet(String sheetName, String[] columnNames, BangKeDto[] allBangKeDto)
        {
            var ds = new DataSet(sheetName);
            var dataTable = new DataTable(sheetName);
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
                dataRow[columnNames[index++]] = bangKeDto.SoCmnd;
                dataRow[columnNames[index++]] = bangKeDto.DiaChi;
                dataRow[columnNames[index++]] = bangKeDto.SoTaiKhoan;
                dataRow[columnNames[index++]] = bangKeDto.ChiNhanhNH;
                dataRow[columnNames[index++]] = bangKeDto.SoDienThoai;
                dataRow[columnNames[index++]] = bangKeDto.SoTien;
                dataRow[columnNames[index]] = bangKeDto.Thang;
                dataTable.Rows.Add(dataRow);
            }
            ds.Tables.Add(dataTable);
            return ds;
        }

        protected void BangKeTraLuong_ExportPDF(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
        }

        protected void gvBangKe_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}