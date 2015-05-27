using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_server.model
{
    public class AccountLog
    {
        #region Declarations

        // Member variables
        private long account_ID = -1;

        private string dml = string.Empty;

        private DateTime created_Date = default(DateTime);

        #endregion

    	#region Constructor

        public AccountLog()
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
        /// Created Date.
        /// </summary>
        public virtual DateTime CreatedDate
        {
            get { return created_Date; }
            set { created_Date = value; }
        }

        /// <summary>
        /// Dml.
        /// </summary>
        public virtual string Dml
        {
            get { return dml; }
            set { dml = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return dml + ":" + account_ID;
        }

        #endregion
    }
}