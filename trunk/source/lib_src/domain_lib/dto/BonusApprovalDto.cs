using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_lib.dto
{
    public class BonusApprovalDto
    {
        #region Declarations

        // Property variables
        private long _id = -1;

        // Member variables
        private string _bonusType = string.Empty;

        private double _bonusAmount = -1.0;

        private long _accountNumber = -1;

        private string _isApproved = string.Empty;

        private string _createdBy = string.Empty;

        private string _approvedBy = string.Empty;
        
        private string _userName = string.Empty;
        
        #endregion

    	#region Constructor

        public BonusApprovalDto()
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
        /// Bonus Type.
        /// </summary>
        public virtual string BonusType
        {
            get { return _bonusType; }
            set { _bonusType = value; }
        }

        /// <summary>
        /// BonusAmount
        /// </summary>
        public virtual double BonusAmount
        {
            get { return _bonusAmount; }
            set { _bonusAmount = value; }
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
        /// UserName
        /// </summary>
        public virtual string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// Is approved
        /// </summary>
        public virtual string IsApproved
        {
            get { return _isApproved; }
            set { _isApproved= value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _userName + "|" + _accountNumber + "|" + _bonusAmount;
        }

        #endregion
    }
}
