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
        private long _id = -1;

        private long _accountId = -1;

        private long _calcAccountId = -1;

        private int _accountLevel;

        private long _levelIndex = -1;

        private string _isCalculated = string.Empty;

        private DateTime _calculatedDate = default(DateTime);

        private DateTime _createdDate = default(DateTime);
        
        #endregion

    	#region Constructor

        public AccountPreCalc()
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
        /// Account ID
        /// </summary>
        public virtual long AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        /// <summary>
        /// Calc Account ID
        /// </summary>
        public virtual long CalcAccountId
        {
            get { return _calcAccountId; }
            set { _calcAccountId= value; }
        }

        /// <summary>
        /// Account Level
        /// </summary>
        public virtual int AccountLevel
        {
            get { return _accountLevel; }
            set { _accountLevel = value; }
        }

        /// <summary>
        /// Level Index
        /// </summary>
        public virtual long LevelIndex
        {
            get { return _levelIndex; }
            set { _levelIndex = value; }
        }

        /// <summary>
        /// Is Calculaed
        /// </summary>
        public virtual string IsCalculated
        {
            get { return _isCalculated; }
            set { _isCalculated= value; }
        }
        
        /// <summary>
        /// Calculated Date.
        /// </summary>
        public virtual DateTime CalculatedDate
        {
            get { return _calculatedDate; }
            set { _calculatedDate = value; }
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
            return _calcAccountId + ":" + _accountId;
        }

        #endregion
    }
}