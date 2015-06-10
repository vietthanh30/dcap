using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_lib.dto
{
    public class AccountBonusDto
    {
        #region Declarations

        // Property variables
        private long _id = -1;

        // Member variables
        private double _tong = -1.0;

        private string _thang = String.Empty;

        private long _isPaid = -1;

        #endregion

    	#region Constructor

        public AccountBonusDto()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// Id
        /// </summary>
        public virtual long Id
        {
            get { return _id; }
            set { _id = value; }
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

        /// <summary>
        /// Status
        /// </summary>
        public virtual long IsPaid
        {
            get { return _isPaid; }
            set { _isPaid = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _thang + "|" + _isPaid + "|" + _tong;
        }

        #endregion
    }
}
