using System;
using System.IO;
using core_lib.common;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.admin
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto == null)
            {
                Response.Redirect("~/admin/Login.aspx");
                return;
            }
            ContinueDestinationPageUrl.Value = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            string continueUrl = ContinueDestinationPageUrl.Value;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        protected void RegisterUser_CreatingUser(object sender, EventArgs e)
        {
            var parentId = ParentId.Value.Trim();
            var directParentId = DirectParentId.Value.Trim();
            var userName = HoTen.Value.Trim();
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
            if (!string.IsNullOrEmpty(sNgaySinh) && ngaySinh == null)
            {
                InvalidCredentialsMessage.Text = "Ngày sinh không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(sNgayCap) && ngayCap == null)
            {
                InvalidCredentialsMessage.Text = "Ngày cấp không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(userName) && userName.Length > 100)
            {
                InvalidCredentialsMessage.Text = "Họ tên quá dài (Nhiều hơn 100 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(soCmnd) && soCmnd.Length > 15)
            {
                InvalidCredentialsMessage.Text = "Số CMND quá dài (Nhiều hơn 15 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(soDienThoai) && soDienThoai.Length > 15)
            {
                InvalidCredentialsMessage.Text = "Số điện thoại quá dài (Nhiều hơn 15 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(diaChi) && diaChi.Length > 500)
            {
                InvalidCredentialsMessage.Text = "Địa chỉ quá dài (Nhiều hơn 500 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(soTaiKhoan) && soTaiKhoan.Length > 50)
            {
                InvalidCredentialsMessage.Text = "Số tài khoản quá dài (Nhiều hơn 50 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(chiNhanhNH) && chiNhanhNH.Length > 100)
            {
                InvalidCredentialsMessage.Text = "Tên ngân hàng quá dài (Nhiều hơn 100 ký tự). Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            var photoName = soCmnd + String.Format("_{0:yyyyMMddHHmmss}", DateTime.Now) + ".jpg";
            var photoDir = String.Format("PHOTO_{0:yyyyMMdd}", DateTime.Now);
            var photoPath = Server.MapPath("~/upload") + "\\" + photoDir + "\\" + photoName;
            var returnCode = SavePhotoToUploadFolder(photoPath);
            var photoUrl = string.Empty;
            if (string.Compare(returnCode, "-1") != 0)
            {
                photoUrl = "~/upload/" + photoDir + "/" + photoName;
            }
            var createdBy = User.Identity.Name;
            returnCode = DcapServiceUtil.CreateUser(parentId, directParentId, userName, sNgaySinh, soCmnd, sNgayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
            int code;
            var error = int.TryParse(returnCode, out code);
            if (!error)
            {
                var codes = returnCode.Split(new[] {'|'});
                var accountNumber = string.Format("{0:0000000}", Convert.ToInt64(codes[0]));
                var tenDangNhap = codes[1];
                AccountCode.Text = "Id thành viên: " + accountNumber + "; Tên đăng nhập: " + tenDangNhap + "/" + ConstUtil.DEFAULT_PASSWORD;
                AccountCode.Visible = true;
                ResetAccountInfo();
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
                        InvalidCredentialsMessage.Text = "Người giới thiệu không tồn tại.";
                        break;
                    case -4:
                        InvalidCredentialsMessage.Text = "Tuyến trên không tồn tại.";
                        break;
                    case -5:
                        InvalidCredentialsMessage.Text = "Người giới thiệu đã giới thiệu đủ 3 thành viên.";
                        break;
                    case -6:
                        InvalidCredentialsMessage.Text = "Đăng ký thành viên không thành công.";
                        break;
                    case -7:
                        InvalidCredentialsMessage.Text = "Đăng ký người dùng không thành công.";
                        break;
                    case -8:
                        InvalidCredentialsMessage.Text = "Đăng ký quyền người dùng không thành công.";
                        break;
                    default:
                        InvalidCredentialsMessage.Text = "Đăng ký không thành công.";
                        break;
                }
                InvalidCredentialsMessage.Visible = true;
            }
        }

        private void ResetAccountInfo()
        {
            ParentId.Value = string.Empty;
            DirectParentId.Value = string.Empty;
            HoTen.Value = string.Empty;
            NgaySinh.Value = string.Empty;
            SoCmnd.Value = string.Empty;
            NgayCap.Value = string.Empty;
            SoDienThoai.Value = string.Empty;
            DiaChi.Value = string.Empty;
            GioiTinh.ClearSelection();
            SoTaiKhoan.Value = string.Empty;
            ChiNhanhNH.Value = string.Empty;
            InvalidCredentialsMessage.Visible = false;
        }

        protected void RegisterUser_SearchUser(object sender, EventArgs e)
        {
            var parentId = ParentId.Value.Trim();
            var directParentId = DirectParentId.Value.Trim();
            var userName = HoTen.Value.Trim();
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
            if (!string.IsNullOrEmpty(sNgaySinh) && ngaySinh == null)
            {
                InvalidCredentialsMessage.Text = "Ngày sinh không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (!string.IsNullOrEmpty(sNgayCap) && ngayCap == null)
            {
                InvalidCredentialsMessage.Text = "Ngày cấp không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            var returnCode = DcapServiceUtil.SearchUser(parentId, directParentId, userName, sNgaySinh, soCmnd, sNgayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH);
            if (string.Compare(returnCode, "-1", true) != 0)
            {
                var message = "";
                if (string.IsNullOrEmpty(returnCode))
                {
                    message = "Không có thành viên nào thỏa mãn";
                }
                else
                {
                    var records = returnCode.Split(new[] { ';' });
                    int count = 0;
                    foreach (var record in records)
                    {
                        var codes = record.Split(new[] { '|' });
                        var accountNumber = string.Format("{0:0000000}", Convert.ToInt64(codes[0]));
                        var tenDangNhap = codes[1];
                        if (count == 0)
                        {
                            message = "(Id thành viên, Tên đăng nhập): (" + accountNumber + ", " + tenDangNhap + ")";
                        }
                        else
                        {
                            message += "; (" + accountNumber + ", " + tenDangNhap + ")";
                        }
                        count++;
                    }
                }
                AccountCode.Text = message;
                AccountCode.Visible = true;
            }
            else
            {
                InvalidCredentialsMessage.Text = "Có lỗi khi tra cứu.";
                InvalidCredentialsMessage.Visible = true;
            }
        }

        private string SavePhotoToUploadFolder(string saveLocation)
        {
            if ((filePhotoUpload.PostedFile != null) && (filePhotoUpload.PostedFile.ContentLength > 0))
            {
                try
                {
                    string directory = saveLocation.Substring(0, saveLocation.LastIndexOf("\\"));// GetDirectory(Path);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
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
    }
}
