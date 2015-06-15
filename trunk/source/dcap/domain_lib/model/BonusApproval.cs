using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace domain_lib.model
{
    public class BonusApproval
    {
        #region Declarations

        // Member variables
        private long _id = -1;

        private long _accountId = -1;

        private string _bonusType = string.Empty;

        private double _bonusAmount = 0;

        private DateTime _createdDate = default(DateTime);

        private string _createdBy = string.Empty;

        private string _isApproved = string.Empty;

        private DateTime? _approvedDate ;

        private string _approvedBy = string.Empty;
        
        #endregion

    	#region Constructor

        public BonusApproval()
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
        /// Created by
        /// </summary>
        public virtual string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        /// <summary>
        /// Approved by
        /// </summary>
        public virtual string ApprovedBy
        {
            get { return _approvedBy; }
            set { _approvedBy = value; }
        }

        /// <summary>
        /// Is approved
        /// </summary>
        public virtual string IsApproved
        {
            get { return _isApproved; }
            set { _isApproved= value; }
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
        /// Created Date.
        /// </summary>
        public virtual DateTime? ApprovedDate
        {
            get { return _approvedDate; }
            set { _approvedDate = value; }
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