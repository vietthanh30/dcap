using System;

namespace domain_lib.model
{
    public class Account
    {
        #region Declarations

        // Property variables
        private long _accountId = -1;

        // Member variables
        private long _parentId = -1;

        private long _parentDirectId = -1;

        private long _accountNumber = -1;

        private int _childIndex = -1;

        private string _isActive = string.Empty;

        private long _memberId = -1;

        private long _userId = -1;

        private DateTime _createdDate = default(DateTime);

        private string _createdBy = string.Empty;

        private string _prefixAccountNumber = string.Empty;

        #endregion

    	#region Constructor

        public Account()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// Account ID
        /// </summary>
        public virtual long AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        /// <summary>
        /// Parent ID
        /// </summary>
        public virtual long ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// Parent Direct ID
        /// </summary>
        public virtual long ParentDirectId
        {
            get { return _parentDirectId; }
            set { _parentDirectId = value; }
        }

        /// <summary>
        /// Account Number
        /// </summary>
        public virtual long AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        /// <summary>
        /// Child Index
        /// </summary>
        public virtual int ChildIndex
        {
            get { return _childIndex; }
            set { _childIndex = value; }
        }

        /// <summary>
        /// Is Active
        /// </summary>
        public virtual string IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        /// <summary>
        /// Member ID
        /// </summary>
        public virtual long MemberId
        {
            get { return _memberId; }
            set { _memberId = value; }
        }

        /// <summary>
        /// User ID
        /// </summary>
        public virtual long UserId
        {
            get { return _userId; }
            set { _userId = value; }
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
        /// Created By.
        /// </summary>
        public virtual string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        /// <summary>
        /// PrefixAccountNumber.
        /// </summary>
        public virtual string PrefixAccountNumber
        {
            get { return _prefixAccountNumber; }
            set { _prefixAccountNumber = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _memberId + ":" + _accountId;
        }

        #endregion
    }
}