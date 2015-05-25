using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_server.model
{
    public class ManagerL2Log
    {
        #region Declarations

        // Property variables
        private int member_ID = -1;

        // Member variables
        private int group_No = -1;

        #endregion

    	#region Constructor

        public ManagerL2Log()
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
        /// Group No.
        /// </summary>
        public virtual int GroupNo
        {
            get { return group_No; }
            set { group_No = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return member_ID + ":" + group_No;
        }

        #endregion
    }
}