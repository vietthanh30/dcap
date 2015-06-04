using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace domain_lib.dto
{
    public class MemberNodeDto
    {
        #region Declarations

        // Property variables
        private long _accountId = -1;

        // Member variables
        private long _parentId = -1;

        private string _description = String.Empty;

        #endregion

    	#region Constructor

        public MemberNodeDto()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// AccountId
        /// </summary>
        public virtual long AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        /// <summary>
        /// ParentId.
        /// </summary>
        public virtual long ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// Description
        /// </summary>
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _description;
        }

        #endregion
    }
}
