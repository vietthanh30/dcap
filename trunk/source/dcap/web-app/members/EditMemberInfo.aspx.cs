using System;
using core_lib.common;
using web_app.common;
using web_app.DcapServiceReference;

namespace web_app.members
{
    public partial class EditMemberInfo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ContinueDestinationPageUrl.Value = Request.QueryString["ReturnUrl"];
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto != null)
            {
                HoTen.Value = userDto.FullName;
                NgaySinh.Value = DateUtil.GetDateTimeAsDdmmyyyy(userDto.NgaySinh);
                SoCmnd.Value = userDto.SoCmnd;
                NgayCap.Value =  DateUtil.GetDateTimeAsDdmmyyyy(userDto.NgayCap);
                SoDienThoai.Value = userDto.SoDienThoai;
                DiaChi.Value = userDto.DiaChi;
                GioiTinh.SelectedValue = userDto.GioiTinh;
                SoTaiKhoan.Value = userDto.SoTaiKhoan;
                ChiNhanhNH.Value = userDto.ChiNhanhNH;
            }
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
            var fullName = HoTen.Value.Trim();
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
            else
            { 
                var userDto = (UserDto)Session["UserDto"];
                if (userDto != null)
                {
                    photoUrl = userDto.ImageUrl;
                }
            }
            var userName = User.Identity.Name;
            returnCode = DcapServiceUtil.UpdateUser(userName, fullName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl);
            if (string.Compare(returnCode, "-1", true) != 0)
            {
                UpdateUserInfo(fullName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan,
                               chiNhanhNH, photoUrl);
                AccountCode.Text = "Cập nhật thông tin thành viên thành công.";
                AccountCode.Visible = true;
            }
            else
            {
                InvalidCredentialsMessage.Text = "Cập nhật không thành công.";
                InvalidCredentialsMessage.Visible = true;
            }
        }

        private void UpdateUserInfo(string fullName, DateTime? ngaySinh, string soCmnd, DateTime? ngayCap, string soDienThoai, string diaChi, string gioiTinh, string soTaiKhoan, string chiNhanhNh, string photoUrl)
        {
            var userDto = (UserDto)Session["UserDto"];
            if (userDto != null)
            {
                userDto.FullName = fullName;
                userDto.NgaySinh = ngaySinh;
                userDto.SoCmnd = soCmnd;
                userDto.NgayCap = ngayCap;
                userDto.SoDienThoai = soDienThoai;
                userDto.DiaChi = diaChi;
                userDto.GioiTinh = gioiTinh;
                userDto.SoTaiKhoan = soTaiKhoan;
                userDto.ChiNhanhNH = chiNhanhNh;
                userDto.ImageUrl = photoUrl;
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
