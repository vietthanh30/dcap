using System;
using core_lib.common;
using web_app.common;

namespace web_app.admin
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
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
            var ngaySinh = DateUtil.GetDateTime(NgaySinh.Value.Trim());
            var soCmnd = SoCmnd.Value.Trim();
            var ngayCap = DateUtil.GetDateTime(NgayCap.Value.Trim());
            var soDienThoai = SoDienThoai.Value.Trim();
            var diaChi = DiaChi.Value.Trim();
            var gioiTinh = GioiTinh.SelectedValue.Trim();
            var soTaiKhoan = SoTaiKhoan.Value.Trim();
            var chiNhanhNH = ChiNhanhNH.Value.Trim();
            if (ngaySinh == null)
            {
                InvalidCredentialsMessage.Text = "Ngày sinh không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (ngayCap == null)
            {
                InvalidCredentialsMessage.Text = "Ngày cấp không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
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
            var createdBy = User.Identity.Name;
            returnCode = DcapServiceUtil.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
            if (string.Compare(returnCode, "-1", true) != 0)
            {
                var codes = returnCode.Split(new[] {'|'});
                var accountNumber = string.Format("{0:0000000}", Convert.ToInt64(codes[0]));
                var tenDangNhap = codes[1];
                AccountCode.Text = "Id thành viên: " + accountNumber + "; Tên đăng nhập: " + tenDangNhap + "/" + ConstUtil.DEFAULT_PASSWORD;
                AccountCode.Visible = true;
            }
            else
            {
                InvalidCredentialsMessage.Text = "Đăng ký không thành công.";
                InvalidCredentialsMessage.Visible = true;
            }
        }

        protected void RegisterUser_SearchUser(object sender, EventArgs e)
        {
            var parentId = ParentId.Value.Trim();
            var directParentId = DirectParentId.Value.Trim();
            var userName = HoTen.Value.Trim();
            var ngaySinh = DateUtil.GetDateTime(NgaySinh.Value.Trim());
            var soCmnd = SoCmnd.Value.Trim();
            var ngayCap = DateUtil.GetDateTime(NgayCap.Value.Trim());
            var soDienThoai = SoDienThoai.Value.Trim();
            var diaChi = DiaChi.Value.Trim();
            var gioiTinh = GioiTinh.SelectedValue.Trim();
            var soTaiKhoan = SoTaiKhoan.Value.Trim();
            var chiNhanhNH = ChiNhanhNH.Value.Trim();
            if (ngaySinh == null)
            {
                InvalidCredentialsMessage.Text = "Ngày sinh không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            if (ngayCap == null)
            {
                InvalidCredentialsMessage.Text = "Ngày cấp không đúng định dạng. Vui lòng nhập lại.";
                InvalidCredentialsMessage.Visible = true;
                return;
            }
            var returnCode = DcapServiceUtil.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH);
            if (string.Compare(returnCode, "-1", true) != 0)
            {
                var message = "";
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
