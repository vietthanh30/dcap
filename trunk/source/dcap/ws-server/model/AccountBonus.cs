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
        private long account_ID = -1;

        private string bonus_Type = string.Empty;

        private DateTime created_Date = default(DateTime);

        private long bonus_Amount = -1;

        private string month = string.Empty;

        private long is_Paid = -1;
        
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
        public virtual long AccountID
        {
            get { return account_ID; }
            set { account_ID = value; }
        }
        
        /// <summary>
        /// Bonus Type.
        /// </summary>
        public virtual string BonusType
        {
            get { return bonus_Type; }
            set { bonus_Type = value; }
        }

        /// <summary>
        /// Bonus Amount
        /// </summary>
        public virtual long BonusAmount
        {
            get { return bonus_Amount; }
            set { bonus_Amount = value; }
        }

        /// <summary>
        /// Month.
        /// </summary>
        public virtual string Month
        {
            get { return month; }
            set { month = value; }
        }

        /// <summary>
        /// Is Paid
        /// </summary>
        public virtual long IsPaid
        {
            get { return is_Paid; }
            set { is_Paid= value; }
        }

        /// <summary>
        /// Created Date.
        /// </summary>
        public virtual DateTime CreatedDate
        {
            get { return created_Date; }
            set { created_Date = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return bonus_Type + ":" + account_ID;
        }

        #endregion
    }
}