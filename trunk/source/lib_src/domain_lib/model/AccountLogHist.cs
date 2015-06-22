using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace domain_lib.model
{
    public class AccountLogHist
    {
        #region Declarations

        // Member variables
        private long _id = -1;

        private long _accountId = -1;

        private string _dml = string.Empty;

        private string _error = string.Empty;

        private DateTime _createdDate = default(DateTime);

        #endregion

    	#region Constructor

        public AccountLogHist()
        {
        }

    	#endregion

        #region Properties
        
        /// <summary>
        /// ID
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
        /// Error
        /// </summary>
        public virtual string Error
        {
            get { return _error; }
            set { _error = value; }
        }

        /// <summary>
        /// Created Date.
        /// </summary>
        public virtual DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        /// <summary>
        /// Dml.
        /// </summary>
        public virtual string Dml
        {
            get { return _dml; }
            set { _dml = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _id.ToString();
        }

        #endregion
    }
}