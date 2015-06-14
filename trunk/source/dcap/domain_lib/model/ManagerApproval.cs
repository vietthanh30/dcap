using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace domain_lib.model
{
    public class ManagerApproval
    {
        #region Declarations

        // Member variables
        private long _id = -1;

        private long _accountId = -1;

        private int _managerLevel = -1;

        private string _isApproved = string.Empty;

        private DateTime? _approvedDate;

        private string _approvedBy = string.Empty;

        private DateTime _createdDate = DateTime.Now;

        private long _accountNumber = -1;

        private string _userName = String.Empty;

        #endregion

    	#region Constructor

        public ManagerApproval()
        {
        }

        public ManagerApproval(long id, long accountNumber, int managerLevel, string userName)
        {
			this._id = id;
			this._accountNumber = accountNumber;
			this._managerLevel = managerLevel;
			this._userName = userName;
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
        /// Account Id
        /// </summary>
        public virtual long AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
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
        /// Is Approved.
        /// Y: approved, N: didn't approve, I: Inprogress
        /// </summary>
        public virtual string IsApproved
        {
            get { return _isApproved; }
            set { _isApproved = value; }
        }

        /// <summary>
        /// Approved Date.
        /// </summary>
        public virtual DateTime? ApprovedDate
        {
            get { return _approvedDate; }
            set { _approvedDate = value; }
        }

        /// <summary>
        /// Approved By
        /// </summary>
        public virtual string ApprovedBy
        {
            get { return _approvedBy; }
            set { _approvedBy = value; }
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
        /// AccountNumber
        /// </summary>
        public virtual long AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        /// <summary>
        /// UserName
        /// </summary>
        public virtual string UserName
        {
            get { return _userName; }
            set { _userName = value; }
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