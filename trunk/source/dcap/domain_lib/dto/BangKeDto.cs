using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_lib.dto
{
    public class BangKeDto
    {
        #region Declarations

        // Property variables
        private long _stt = -1;

        // Member variables
        private string _hoTen = String.Empty;

        private string user_Name = String.Empty;

        private string _gioiTinh = String.Empty;

        private string _soCmnd = String.Empty;

        private string _ngayCap = String.Empty;

        private string _diaChi = String.Empty;

        private string _soTaiKhoan = String.Empty;

        private string _chiNhanhNH = String.Empty;

        private string _soDienThoai = String.Empty;

        private string _ngayDangKy = String.Empty;

        private string _nguoiBaoTro = String.Empty;

        private double _soTien = -1.0;

        private string _thang = String.Empty;

        #endregion

    	#region Constructor

        public BangKeDto()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// STT
        /// </summary>
        public virtual long STT
        {
            get { return _stt; }
            set { _stt = value; }
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
        /// User name.
        /// </summary>
        public virtual string UserName
        {
            get { return user_Name; }
            set { user_Name = value; }
        }

        /// <summary>
        /// Gioi Tinh.
        /// </summary>
        public virtual string GioiTinh
        {
            get { return _gioiTinh; }
            set { _gioiTinh = value; }
        }

        /// <summary>
        /// So Cmnd
        /// </summary>
        public virtual string SoCmnd
        {
            get { return _soCmnd; }
            set { _soCmnd = value; }
        }

        /// <summary>
        /// Ngay Cap
        /// </summary>
        public virtual string NgayCap
        {
            get { return _ngayCap; }
            set { _ngayCap = value; }
        }

        /// <summary>
        /// Dia Chi
        /// </summary>
        public virtual string DiaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; }
        }

        /// <summary>
        /// So Tai Khoan
        /// </summary>
        public virtual string SoTaiKhoan
        {
            get { return _soTaiKhoan; }
            set { _soTaiKhoan = value; }
        }

        /// <summary>
        /// Chi Nhanh NH
        /// </summary>
        public virtual string ChiNhanhNH
        {
            get { return _chiNhanhNH; }
            set { _chiNhanhNH = value; }
        }

        /// <summary>
        /// So Dien Thoai
        /// </summary>
        public virtual string SoDienThoai
        {
            get { return _soDienThoai; }
            set { _soDienThoai = value; }
        }

        /// <summary>
        /// Ngay Dang Ky
        /// </summary>
        public virtual string NgayDangKy
        {
            get { return _ngayDangKy; }
            set { _ngayDangKy = value; }
        }

        /// <summary>
        /// Nguoi Bao Tro
        /// </summary>
        public virtual string NguoiBaoTro
        {
            get { return _nguoiBaoTro; }
            set { _nguoiBaoTro = value; }
        }

        /// <summary>
        /// So Tien
        /// </summary>
        public virtual double SoTien
        {
            get { return _soTien; }
            set { _soTien = value; }
        }

        /// <summary>
        /// Thang
        /// </summary>
        public virtual string Thang
        {
            get { return _thang; }
            set { _thang = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _hoTen;
        }

        #endregion
    }
}
