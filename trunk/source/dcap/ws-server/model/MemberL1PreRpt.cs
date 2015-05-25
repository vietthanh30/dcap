using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_server.model
{
    public class MemberL1PreRpt
    {
        #region Declarations

        // Property variables
        private int parent_ID = -1;

        // Member variables
        private int score = -1;

        private int full_Status = -1;

        #endregion

    	#region Constructor

        public MemberL1PreRpt()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// Parent ID
        /// </summary>
        public virtual int ParentID
        {
            get { return parent_ID; }
            set { parent_ID = value; }
        }

        /// <summary>
        /// Score.
        /// </summary>
        public virtual int Score
        {
            get { return score; }
            set { score = value; }
        }

        /// <summary>
        /// Full Status.
        /// </summary>
        public virtual int FullStatus
        {
            get { return full_Status; }
            set { full_Status = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return parent_ID + ":" + score + ":" + full_Status;
        }

        #endregion
    }
}