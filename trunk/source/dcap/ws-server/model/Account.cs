using System;

namespace ws_server.model
{
    public class Account
    {
        #region Declarations

        // Property variables
        private long account_ID = -1;

        // Member variables
        private long parent_ID = -1;

        private long parent_Direct_ID = -1;

        private long account_Number = -1;

        private int child_Index = -1;

        private string is_Active = string.Empty;

        private long member_ID = -1;

        private long user_ID = -1;

        private DateTime created_Date = default(DateTime);

        private string created_By = string.Empty;

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
        public virtual long AccountID
        {
            get { return account_ID; }
            set { account_ID = value; }
        }

        /// <summary>
        /// Parent ID
        /// </summary>
        public virtual long ParentID
        {
            get { return parent_ID; }
            set { parent_ID = value; }
        }

        /// <summary>
        /// Parent Direct ID
        /// </summary>
        public virtual long ParentDirectID
        {
            get { return parent_Direct_ID; }
            set { parent_Direct_ID = value; }
        }

        /// <summary>
        /// Account Number
        /// </summary>
        public virtual long AccountNumber
        {
            get { return account_Number; }
            set { account_Number = value; }
        }

        /// <summary>
        /// Child Index
        /// </summary>
        public virtual int ChildIndex
        {
            get { return child_Index; }
            set { child_Index = value; }
        }

        /// <summary>
        /// Is Active
        /// </summary>
        public virtual string IsActive
        {
            get { return is_Active; }
            set { is_Active = value; }
        }

        /// <summary>
        /// Member ID
        /// </summary>
        public virtual long MemberID
        {
            get { return member_ID; }
            set { member_ID = value; }
        }

        /// <summary>
        /// User ID
        /// </summary>
        public virtual long UserID
        {
            get { return user_ID; }
            set { user_ID = value; }
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
        /// Created By.
        /// </summary>
        public virtual string CreatedBy
        {
            get { return created_By; }
            set { created_By = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return member_ID + ":" + account_ID;
        }

        #endregion
    }
}