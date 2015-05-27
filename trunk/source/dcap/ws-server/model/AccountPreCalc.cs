using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_server.model
{
    public class AccountPreCalc
    {
        #region Declarations

        // Member variables
        private long account_ID = -1;

        private long calc_Account_ID = -1;

        private int account_Level = 0;

        private long level_Index = -1;

        private string is_calculated = string.Empty;

        private DateTime calculated_Date = default(DateTime);

        private DateTime created_Date = default(DateTime);

        
        #endregion

    	#region Constructor

        public AccountPreCalc()
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
        /// Calc Account ID
        /// </summary>
        public virtual long CalcAccountID
        {
            get { return calc_Account_ID; }
            set { calc_Account_ID= value; }
        }

        /// <summary>
        /// Account Level
        /// </summary>
        public virtual int AccountLevel
        {
            get { return account_Level; }
            set { account_Level = value; }
        }

        /// <summary>
        /// Level Index
        /// </summary>
        public virtual long LevelIndex
        {
            get { return level_Index; }
            set { level_Index = value; }
        }

        /// <summary>
        /// Is Calculaed
        /// </summary>
        public virtual string IsCalculated
        {
            get { return is_calculated; }
            set { is_calculated= value; }
        }
        
        /// <summary>
        /// Calculated Date.
        /// </summary>
        public virtual DateTime CalculatedDate
        {
            get { return calculated_Date; }
            set { calculated_Date = value; }
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
            return calc_Account_ID + ":" + account_ID;
        }

        #endregion
    }
}