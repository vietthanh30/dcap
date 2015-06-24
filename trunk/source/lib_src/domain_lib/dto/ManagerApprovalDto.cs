using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_lib.dto
{
    public class ManagerApprovalDto
    {
        #region Declarations

        // Property variables
		private long _id = -1;
		
        // Member variables
        private string _accountNumber = string.Empty;

        private int _managerLevel = -1;

        private string _userName = String.Empty;

        private string _approvedBy = string.Empty;

        #endregion

    	#region Constructor

        public ManagerApprovalDto()
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
        /// AccountNumber
        /// </summary>
        public virtual string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        /// <summary>
        /// Manager Level
        /// </summary>
        public virtual int ManagerLevel
        {
            get { return _managerLevel; }
            set { _managerLevel = value; }
        }

        /// <summary>
        /// UserName
        /// </summary>
        public virtual string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// Approved By
        /// </summary>
        public virtual string ApprovedBy
        {
            get { return _approvedBy; }
            set { _approvedBy = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _managerLevel + "|" + _accountNumber;
        }

        #endregion
    }
}
