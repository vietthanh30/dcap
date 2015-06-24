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
            if (!UserUtil.IsQthtRole(userDto) && !UserUtil.IsQlktRole(userDto))
            {
                Response.Redirect("~/Default.aspx");
                return;
            }
        }

        protected void TraCuuThanhVien_Search(object sender, EventArgs e)
        {
            OnSearchThanhVien();
        }

        private void OnSearchThanhVien()
        {
            UserDto[] userDtos;
            if (!GetAllUserInfo(out userDtos))
            {
                return;
            }
            if (userDtos.Length > 0)
            {
                LoadUserInfo(userDtos);
                InvalidCredentialsMessage.Visible = false;
            }
            else
            {
                InvalidCredentialsMessage.Text = "Không tìm thấy thành viên thỏa mãn";
                InvalidCredentialsMessage.Visible = true;
                ResetGvMemberInfo();
            }
        }

        private bool GetAllUserInfo(out UserDto[] userDtos)
        {
            var soCmnd = SoCmndSearch.Value.Trim();
            var idThanhVien = IdThanhVienSearch.Value.Trim();
            var hoTen = HoTenSearch.Value.Trim();
            if (!string.IsNullOrEmpty(idThanhVien) && !DcapServiceUtil.IsValidAccountNumber(idThanhVien))
            {
                InvalidCredentialsMessage.Text = "Id thành viên không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                userDtos = new UserDto[0];
                ResetGvMemberInfo();
                return false;
            }
            if (string.IsNullOrEmpty(soCmnd) && string.IsNullOrEmpty(idThanhVien) && string.IsNullOrEmpty(hoTen))
            {
                InvalidCredentialsMessage.Text = "Phải nhập tối thiểu 1 thông tin tìm kiếm.";
                InvalidCredentialsMessage.Visible = true;
                userDtos = new UserDto[0];
                ResetGvMemberInfo();
                return false;
            }
            userDtos = DcapServiceUtil.SearchUserInfo(soCmnd, idThanhVien, hoTen);
            return true;
        }

        private void ResetGvMemberInfo()
        {
            gvMemberInfo.DataSource = new UserDto[0];
            gvMemberInfo.DataBind();
        }

        private void LoadUserInfo(UserDto[] userDtos)
        {
            gvMemberInfo.DataSource = userDtos;
            gvMemberInfo.DataBind();
        }

        protected void TraCuuThanhVien_ExportToWord(object sender, EventArgs e)
        {
            InvalidCredentialsMessage.Visible = false;
            UserDto[] userDtos;
            if (!GetAllUserInfo(out userDtos))
            {
                return;
            }
            if (userDtos.Length == 0)
            {
                return;
            }
            DataTable dt = OnCreateDataTable(userDtos);
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

        private DataTable OnCreateDataTable(UserDto[] userDtos)
        {
            var columnNames = new[] { "Họ tên", "Số cmnd", "Id thành viên", "Tên đăng nhập" };
            var columnTypes = new[] { typeof(string), typeof(string), typeof(string), typeof(string) };
            var tableName = "MEMBER_INFO";
            return CreateDataTable(tableName, columnNames, columnTypes, userDtos);
        }

        private DataTable CreateDataTable(String tableName, String[] columnNames, Type[] columnTypes, UserDto[] userDtos)
        {
            var dataTable = ExcelHelper.CreateEmptyDataTable(tableName, columnNames, columnTypes);
            foreach (var userDto in userDtos)
            {
                var dataRow = dataTable.NewRow();
                int index = 0;
                dataRow[columnNames[index++]] = userDto.FullName;
                dataRow[columnNames[index++]] = userDto.SoCmnd;
                dataRow[columnNames[index++]] = userDto.AccountNumber;
                dataRow[columnNames[index++]] = userDto.UserName;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        protected void TraCuuThanhVien_ExportToExcel(object sender, EventArgs e)
        {
            InvalidCredentialsMessage.Visible = false;
            UserDto[] userDtos;
            if (!GetAllUserInfo(out userDtos))
            {
                return;
            }
            if (userDtos.Length == 0)
            {
                return;
            }
            var dt = OnCreateDataTable(userDtos);
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

        protected void imgBtnEditUser_Click(object sender, ImageClickEventArgs e)
        {
            var row = (GridViewRow)(sender as Control).Parent.Parent;
            var accountNumber = row.Cells[3].Text;
            var userDtos = DcapServiceUtil.SearchUserInfo(string.Empty, accountNumber, string.Empty);
            if (userDtos.Length == 0)
            {
                return;
            }
            LoadEditUserInfo(userDtos[0]);
            EditPlaceHolder.Visible = true;
            EditMemberPopup.ShowPopupWindow();
        }

        private void LoadEditUserInfo(UserDto userDto)
        {
            HoTen.Value = userDto.FullName;
            var sParentId = userDto.ParentId;
            ParentId.Value = GetMemberDescById(sParentId);
            var sParentDirectId = userDto.ParentDirectId;
            DirectParentId.Value = GetMemberDescById(sParentDirectId);
            NgaySinh.Value = DateUtil.GetDateTimeAsDdmmyyyy(userDto.NgaySinh);
            SoCmnd.Value = userDto.SoCmnd;
            NgayCap.Value = DateUtil.GetDateTimeAsDdmmyyyy(userDto.NgayCap);
            SoDienThoai.Value = userDto.SoDienThoai;
            DiaChi.Value = userDto.DiaChi;
            GioiTinh.SelectedValue = userDto.GioiTinh;
            SoTaiKhoan.Value = userDto.SoTaiKhoan;
            ChiNhanhNH.Value = userDto.ChiNhanhNH;
        }

        private string GetMemberDescById(string accountNumber)
        {
            MemberNodeDto dto = DcapServiceUtil.GetNodeDto(accountNumber);
            if (dto == null)
            {
                return string.Empty;
            }
            var description = dto.Description;
            if (String.IsNullOrEmpty(description))
            {
                return description;
            }
            var arr1 = description.Split(new[] { '|' });
            return arr1[1];
        }

        protected void OnClosePopupWindow(object sender, EventArgs e)
        {
            DeleteMemberPopup.HidePopupWindow();
            EditMemberPopup.HidePopupWindow();
            EditPlaceHolder.Visible = false;
        }

        protected void imgBtnDeleteUser_Click(object sender, ImageClickEventArgs e)
        {
            var row = (GridViewRow)(sender as Control).Parent.Parent;
            var fullName = row.Cells[1].Text;
            var accountNumber = row.Cells[3].Text;
            DeleteMemberLabel.Text = "Bạn muốn xóa thành viên " + fullName + " [" + accountNumber + "]?";
            DeleteMemberPopup.ShowPopupWindow();
        }

        protected void TraCuuThanhVien_EditUser(object sender, EventArgs e)
        {
            var fullName = HoTen.Value.Trim();
            var sNgaySinh = NgaySinh.Value.Trim();
            var ngaySinh = DateUtil.GetDateTime(sNgaySinh);
            var soCmnd = SoCmnd.Value.Trim();
            var sNgayCap = NgayCap.Value.Trim();
            var ngayCap = DateUtil.GetDateTime(sNgayCap);
            var soDienThoai = SoDienThoai.Value.Trim();
            var diaChi = DiaChi.Value.Trim();
            var gioiTinh = GioiTinh.SelectedValue.Trim();
            var soTaiKhoan = SoTaiKhoan.Value.Trim();
            var chiNhanhNH = ChiNhanhNH.Value.Trim();
            if (!String.IsNullOrEmpty(sNgaySinh) && ngaySinh == null)
            {
                InvalidCredentialsMessage.Text = "Ngày sinh không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                OnClosePopupWindow(sender, e);
                return;
            }
            if (!String.IsNullOrEmpty(sNgayCap) && ngayCap == null)
            {
                InvalidCredentialsMessage.Text = "Ngày cấp không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                OnClosePopupWindow(sender, e);
                return;
            }
            if (!string.IsNullOrEmpty(fullName) && fullName.Length > 100)
            {
                InvalidCredentialsMessage.Text = "Họ tên quá dài (Nhiều hơn 100 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                OnClosePopupWindow(sender, e);
                return;
            }
            if (!string.IsNullOrEmpty(soCmnd) && soCmnd.Length > 15)
            {
                InvalidCredentialsMessage.Text = "Số CMND quá dài (Nhiều hơn 15 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                OnClosePopupWindow(sender, e);
                return;
            }
            if (!string.IsNullOrEmpty(soDienThoai) && soDienThoai.Length > 15)
            {
                InvalidCredentialsMessage.Text = "Số điện thoại quá dài (Nhiều hơn 15 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                OnClosePopupWindow(sender, e);
                return;
            }
            if (!string.IsNullOrEmpty(diaChi) && diaChi.Length > 500)
            {
                InvalidCredentialsMessage.Text = "Địa chỉ quá dài (Nhiều hơn 500 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                OnClosePopupWindow(sender, e);
                return;
            }
            if (!string.IsNullOrEmpty(soTaiKhoan) && soTaiKhoan.Length > 50)
            {
                InvalidCredentialsMessage.Text = "Số tài khoản quá dài (Nhiều hơn 50 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                OnClosePopupWindow(sender, e);
                return;
            }
            if (!string.IsNullOrEmpty(chiNhanhNH) && chiNhanhNH.Length > 100)
            {
                InvalidCredentialsMessage.Text = "Tên ngân hàng quá dài (Nhiều hơn 100 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                OnClosePopupWindow(sender, e);
                return;
            }
            var photoName = soCmnd + String.Format("_{0:yyyyMMddHHmmss}", DateTime.Now) + ".jpg";
            var photoPath = Server.MapPath("~/upload") + "\\" + photoName;
            var returnCode = SavePhotoToUploadFolder(photoPath);
            var photoUrl = string.Empty;
            if (string.Compare(returnCode, "-1") != 0)
            {
                photoUrl = "~/upload/" + photoName;
            }
            else
            {
                var userDto = (UserDto)Session["UserDto"];
                if (userDto != null)
                {
                    photoUrl = userDto.ImageUrl;
                }
            }
            var userName = User.Identity.Name;
            returnCode = DcapServiceUtil.UpdateUser(userName, fullName, sNgaySinh, soCmnd, sNgayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl);
            int code;
            var status = int.TryParse(returnCode, out code);
            if (status && code == 0)
            {
                AccountCode.Text = "Cập nhật thông tin thành viên thành công.";
                AccountCode.Visible = true;
                InvalidCredentialsMessage.Visible = false;
            }
            else
            {
                switch (code)
                {
                    case -1:
                        InvalidCredentialsMessage.Text = "Chưa nhập họ tên.";
                        break;
                    case -2:
                        InvalidCredentialsMessage.Text = "Chưa nhập số CMND.";
                        break;
                    case -3:
                        InvalidCredentialsMessage.Text = "Thành viên không tồn tại.";
                        break;
                    default:
                        InvalidCredentialsMessage.Text = "Cập nhật không thành công.";
                        break;
                }
                InvalidCredentialsMessage.Visible = true;
            }
            OnClosePopupWindow(sender, e);
        }

        private string SavePhotoToUploadFolder(string saveLocation)
        {
            if ((filePhotoUpload.PostedFile != null) && (filePhotoUpload.PostedFile.ContentLength > 0))
            {
                try
                {
                    filePhotoUpload.PostedFile.SaveAs(saveLocation);
                    return "0";
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
            return "-1";
        }

        protected void TraCuuThanhVien_DeleteUser(object sender, EventArgs e)
        {
            OnClosePopupWindow(sender, e);
        }
    }
}