using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_server.model
{
    public class AccountBonus
    {
        #region Declarations

        // Member variables
        private long _id = -1;

        private long _accountId = -1;

        private string _bonusType = string.Empty;

        private DateTime _createdDate = default(DateTime);

        private double _bonusAmount = 0;

        private string _month = string.Empty;

        private long _isPaid = -1;
        
        #endregion

    	#region Constructor

        public AccountBonus()
        {
        }

    	#endregion

        #region Properties
        /// <summary>
        /// Account ID
        /// </summary>
        public virtual long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Account ID
        /// </summary>
        public virtual long AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
        
        /// <summary>
        /// Bonus Type.
        /// </summary>
        public virtual string BonusType
        {
            get { return _bonusType; }
            set { _bonusType = value; }
        }

        /// <summary>
        /// Bonus Amount
        /// </summary>
        public virtual double BonusAmount
        {
            get { return _bonusAmount; }
            set { _bonusAmount = value; }
        }

        /// <summary>
        /// Month.
        /// </summary>
        public virtual string Month
        {
            get { return _month; }
            set { _month = value; }
        }

        /// <summary>
        /// Is Paid
        /// </summary>
        public virtual long IsPaid
        {
            get { return _isPaid; }
            set { _isPaid= value; }
        }

        /// <summary>
        /// Created Date.
        /// </summary>
        public virtual DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _bonusType + ":" + _accountId;
        }

        #endregion
    }
}