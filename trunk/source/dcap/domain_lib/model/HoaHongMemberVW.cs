using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_lib.model
{
    public class HoaHongMemberVW
    {
        #region Declarations

        // Property variables
        private long _stt = -1;

        // Member variables
        private long _accountId = -1;

        private string _bonusType = String.Empty;

        private string _thang = String.Empty;

        private double _tong = -1.0;

        #endregion

    	#region Constructor

        public HoaHongMemberVW()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// Stt.
        /// </summary>
        public virtual long Stt
        {
            get { return _stt; }
            set { _stt = value; }
        }

        /// <summary>
        /// AccountId.
        /// </summary>
        public virtual long AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        /// <summary>
        /// BonusType.
        /// </summary>
        public virtual string BonusType
        {
            get { return _bonusType; }
            set { _bonusType = value; }
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
            return _accountId + "|" + _bonusType + "|" + _thang;
        }

        #endregion
    }
}
