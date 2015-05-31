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

        private string _soCmnd = String.Empty;

        private string _diaChi = String.Empty;

        private string _soTaiKhoan = String.Empty;

        private string _chiNhanhNH = String.Empty;

        private string _soDienThoai = String.Empty;

        private long _soTien = -1;

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
        /// So Cmnd
        /// </summary>
        public virtual string SoCmnd
        {
            get { return _soCmnd; }
            set { _soCmnd = value; }
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
        /// So Tien
        /// </summary>
        public virtual long SoTien
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
