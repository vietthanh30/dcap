using System;

namespace ws_server.model
{
    public class MemberInfo
    {
        #region Declarations

        // Property variables
        private int _memberId = -1;

        // Member variables
        private string _hoTen = String.Empty;

        private DateTime? _ngaySinh;

        private String _soCmnd = String.Empty;

        private DateTime? _ngayCap;

        private String _soDienThoai = String.Empty;

        private String _diaChi = String.Empty;

        private String _gioiTinh = String.Empty;

        private String _soTaiKhoan = String.Empty;

        private String _chiNhanhNh = String.Empty;

        private String _imageUrl = String.Empty;

        private DateTime _createdDate = DateTime.Now;

        private String _createdBy = String.Empty;

        #endregion

    	#region Constructor

        public MemberInfo()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// Member ID
        /// </summary>
        public virtual int MemberID
        {
            get { return _memberId; }
            set { _memberId = value; }
        }

        /// <summary>
        /// Ho Ten.
        /// </summary>
        public virtual string HoTen
        {
            get { return _hoTen; }
            set { _hoTen = value; }
        }

        /// <summary>
        /// Ngay Sinh
        /// </summary>
        public virtual DateTime? NgaySinh
        {
            get { return _ngaySinh; }
            set { _ngaySinh = value; }
        }

        /// <summary>
        /// So Cmnd
        /// </summary>
        public virtual String SoCmnd
        {
            get { return _soCmnd; }
            set { _soCmnd = value; }
        }

        /// <summary>
        /// Ngay Cap
        /// </summary>
        public virtual DateTime? NgayCap
        {
            get { return _ngayCap; }
            set { _ngayCap = value; }
        }

        /// <summary>
        /// So Dien Thoai
        /// </summary>
        public virtual String SoDienThoai
        {
            get { return _soDienThoai; }
            set { _soDienThoai = value; }
        }

        /// <summary>
        /// Dia Chi
        /// </summary>
        public virtual String DiaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; }
        }

        /// <summary>
        /// Gioi Tinh
        /// </summary>
        public virtual String GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; }
        }

        /// <summary>
        /// So Tai Khoan
        /// </summary>
        public virtual String SoTaiKhoan
        {
            get { return _soTaiKhoan; }
            set { _soTaiKhoan = value; }
        }

        /// <summary>
        /// Chi Nhanh NH
        /// </summary>
        public virtual String ChiNhanhNH
        {
            get { return _chiNhanhNh; }
            set { _chiNhanhNh = value; }
        }

        /// <summary>
        /// Member Code
        /// </summary>
        public virtual String ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        /// <summary>
        /// Created Date
        /// </summary>
        public virtual DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        /// <summary>
        /// So Cmnd
        /// </summary>
        public virtual String CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _imageUrl;
        }

        #endregion
    }
}