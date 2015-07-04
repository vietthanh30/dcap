using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_lib.dto
{
    public class HoaHongMemberDto
    {
        #region Declarations

        // Property variables
        private long _stt = -1;

        // Member variables
        private string _accountId = string.Empty;

        private double _trucTiep = -1.0;

        private double _canCap = -1.0;

        private double _heThong = -1.0;

        private double _quanLy = -1.0;

        private double _thuongThem = -1.0;

        private double _tong = -1.0;

        private string _thang = String.Empty;

        #endregion

    	#region Constructor

        public HoaHongMemberDto()
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
        /// AccountId.
        /// </summary>
        public virtual string AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        /// <summary>
        /// TrucTiep
        /// </summary>
        public virtual double TrucTiep
        {
            get { return _trucTiep; }
            set { _trucTiep = value; }
        }

        /// <summary>
        /// CanCap
        /// </summary>
        public virtual double CanCap
        {
            get { return _canCap; }
            set { _canCap = value; }
        }

        /// <summary>
        /// HeThong
        /// </summary>
        public virtual double HeThong
        {
            get { return _heThong; }
            set { _heThong = value; }
        }

        /// <summary>
        /// QuanLy
        /// </summary>
        public virtual double QuanLy
        {
            get { return _quanLy; }
            set { _quanLy = value; }
        }

        /// <summary>
        /// ThuongThem
        /// </summary>
        public virtual double ThuongThem
        {
            get { return _thuongThem; }
            set { _thuongThem = value; }
        }

        /// <summary>
        /// Tong
        /// </summary>
        public virtual double Tong
        {
            get { return _tong; }
            set { _tong = value; }
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
            return _accountId + "|" + _thang;
        }

        #endregion
    }
}
