using System;

namespace domain_lib.model
{
    public class MemberInfo
    {
        #region Declarations

        // Property variables
        private int _memberId = -1;

        // Member variables
        private string _hoTen = String.Empty;

        private string _hoTenKd = String.Empty;

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

        private long _accountId = -1;

        private long _parentId = -1;

        private long _parentDirectId = -1;

        private long _accountNumber = -1;

        private string _userName = string.Empty;

        #endregion

    	#region Constructor

        public MemberInfo()
        {
        }

        public MemberInfo(long accountNumber, string hoTen, DateTime ngaySinh, string soCmnd, DateTime ngayCap, string soDienThoai, string diaChi,
                    string gioiTinh, string soTaiKhoan, string chiNhanhNH, string imageUrl, DateTime createdDate, string createdBy)
        {
            this._accountNumber = accountNumber;
            this._hoTen = hoTen;
            this._ngaySinh = ngaySinh;
            this._soCmnd = soCmnd;
            this._ngayCap = ngayCap;
            this._soDienThoai = soDienThoai;
            this._diaChi = diaChi;
            this._gioiTinh = gioiTinh;
            this._soTaiKhoan = soTaiKhoan;
            this._chiNhanhNh = chiNhanhNH;
            this._imageUrl = imageUrl;
            this._createdDate = createdDate;
            this._createdBy = createdBy;
        }

        public MemberInfo(long accountNumber, string hoTen, string userName, DateTime ngaySinh, string soCmnd, DateTime ngayCap, string soDienThoai, string diaChi,
                    string gioiTinh, string soTaiKhoan, string chiNhanhNH, string imageUrl, DateTime createdDate, string createdBy)
        {
            this._accountNumber = accountNumber;
            this._hoTen = hoTen;
            this._userName = userName;
            this._ngaySinh = ngaySinh;
            this._soCmnd = soCmnd;
            this._ngayCap = ngayCap;
            this._soDienThoai = soDienThoai;
            this._diaChi = diaChi;
            this._gioiTinh = gioiTinh;
            this._soTaiKhoan = soTaiKhoan;
            this._chiNhanhNh = chiNhanhNH;
            this._imageUrl = imageUrl;
            this._createdDate = createdDate;
            this._createdBy = createdBy;
        }

        public MemberInfo(long accountNumber, string hoTen, long parentId, long parentDirectId, DateTime ngaySinh, string soCmnd, DateTime ngayCap, string soDienThoai, string diaChi,
                    string gioiTinh, string soTaiKhoan, string chiNhanhNH, string imageUrl, DateTime createdDate, string createdBy)
        {
            this._accountNumber = accountNumber;
            this._hoTen = hoTen;
            this._parentId = parentId;
            this._parentDirectId = parentDirectId;
            this._ngaySinh = ngaySinh;
            this._soCmnd = soCmnd;
            this._ngayCap = ngayCap;
            this._soDienThoai = soDienThoai;
            this._diaChi = diaChi;
            this._gioiTinh = gioiTinh;
            this._soTaiKhoan = soTaiKhoan;
            this._chiNhanhNh = chiNhanhNH;
            this._imageUrl = imageUrl;
            this._createdDate = createdDate;
            this._createdBy = createdBy;
        }

        public MemberInfo(long accountNumber, string hoTen, string userName, long parentId, long parentDirectId, DateTime ngaySinh, string soCmnd, DateTime ngayCap, string soDienThoai, string diaChi,
                    string gioiTinh, string soTaiKhoan, string chiNhanhNH, string imageUrl, DateTime createdDate, string createdBy)
        {
            this._accountNumber = accountNumber;
            this._hoTen = hoTen;
            this._userName = userName;
            this._parentId = parentId;
            this._parentDirectId = parentDirectId;
            this._ngaySinh = ngaySinh;
            this._soCmnd = soCmnd;
            this._ngayCap = ngayCap;
            this._soDienThoai = soDienThoai;
            this._diaChi = diaChi;
            this._gioiTinh = gioiTinh;
            this._soTaiKhoan = soTaiKhoan;
            this._chiNhanhNh = chiNhanhNH;
            this._imageUrl = imageUrl;
            this._createdDate = createdDate;
            this._createdBy = createdBy;
        }

        public MemberInfo(long accountId, long parentId, long accountNumber, string hoTen, string userName)
        {
            this._accountId = accountId;
            this._parentId = parentId;
            this._accountNumber = accountNumber;
            this._hoTen = hoTen;
            this._userName = userName;
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
        /// Account Number
        /// </summary>
        public virtual long AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        /// <summary>
        /// Account Id
        /// </summary>
        public virtual long AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        /// <summary>
        /// ParentId
        /// </summary>
        public virtual long ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// Parent Direct ID
        /// </summary>
        public virtual long ParentDirectId
        {
            get { return _parentDirectId; }
            set { _parentDirectId = value; }
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
        /// Ho Ten Kd.
        /// </summary>
        public virtual string HoTenKd
        {
            get { return _hoTenKd; }
            set { _hoTenKd = value; }
        }

        /// <summary>
        /// UserName.
        /// </summary>
        public virtual string UserName
        {
            get { return _userName; }
            set { _userName = value; }
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