using System;
using System.Web.Security;
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
            var ngaySinh = NgaySinh.Value.Trim();
            var soCmnd = SoCmnd.Value.Trim();
            var diaChi = DiaChi.Value.Trim();
            var soTaiKhoan = SoTaiKhoan.Value.Trim();
            var chiNhanhNH = ChiNhanhNH.Value.Trim();
            var photoUrl = Server.MapPath("upload") + "\\" + soCmnd + String.Format("_{0:yyyyMMddHHmmss}", DateTime.Now) + ".jpg";
            SavePhotoToUploadFolder(photoUrl);
            var returnCode = DcapServiceUtil.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, diaChi, soTaiKhoan, chiNhanhNH, photoUrl);
            if (Convert.ToInt32(returnCode) == 0)
            {
                RegisterUser_CreatedUser(sender, e);
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
            var ngaySinh = NgaySinh.Value.Trim();
            var soCmnd = SoCmnd.Value.Trim();
            var diaChi = DiaChi.Value.Trim();
            var soTaiKhoan = SoTaiKhoan.Value.Trim();
            var chiNhanhNH = ChiNhanhNH.Value.Trim();
            var photoUrl = Server.MapPath("upload") + "\\" + soCmnd + String.Format("_{0:yyyyMMddHHmmss}", DateTime.Now) + ".jpg";
            SavePhotoToUploadFolder(photoUrl);
            var returnCode = DcapServiceUtil.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, diaChi, soTaiKhoan, chiNhanhNH, photoUrl);
            if (Convert.ToInt32(returnCode) == 0)
            {
                RegisterUser_CreatedUser(sender, e);
            }
            else
            {
                InvalidCredentialsMessage.Text = "Có lỗi khi tra cứu.";
                InvalidCredentialsMessage.Visible = true;
            }
        }

        private void SavePhotoToUploadFolder(string saveLocation)
        {
            if ((filePhotoUpload.PostedFile != null) && (filePhotoUpload.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(filePhotoUpload.PostedFile.FileName);
                try
                {
                    filePhotoUpload.PostedFile.SaveAs(saveLocation);
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }
    }
}
