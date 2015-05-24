using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_server.model
{
    public class ManagerTreeL1
    {
        #region Declarations

        // Property variables
        private int member_ID = -1;

        // Member variables
        private int member_Index = -1;

        private int parent_ID = -1;

        private String children_ID = String.Empty;

        private int status = -1;

        #endregion

    	#region Constructor

        public ManagerTreeL1()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// Member ID
        /// </summary>
        public virtual int MemberID
        {
            get { return member_ID; }
            set { member_ID = value; }
        }

        /// <summary>
        /// Member Index.
        /// </summary>
        public virtual int MemberIndex
        {
            get { return member_Index; }
            set { member_Index = value; }
        }

        /// <summary>
        /// Parent ID
        /// </summary>
        public virtual int ParentID
        {
            get { return parent_ID; }
            set { parent_ID = value; }
        }

        /// <summary>
        /// Children ID
        /// </summary>
        public virtual String ChildrenID
        {
            get { return children_ID; }
            set { children_ID = value; }
        }

        /// <summary>
        /// Status
        /// </summary>
        public virtual int Status
        {
            get { return status; }
            set { status = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return member_ID + ":" + member_Index;
        }

        #endregion
    }
}