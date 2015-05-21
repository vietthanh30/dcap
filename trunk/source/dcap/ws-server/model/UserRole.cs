using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_server.model
{
    public class UserRole
    {
        #region Declarations

        // Property variables
        private int role_ID = -1;
        private int user_ID = -1;

        // Member variables
        private bool is_Active = true;

        #endregion

    	#region Constructor

        public UserRole()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// Role ID
        /// </summary>
        public virtual int RoleID
        {
            get { return role_ID; }
            set { role_ID = value; }
        }

        /// <summary>
        /// User ID
        /// </summary>
        public virtual int UserID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }

        /// <summary>
        /// Is Active.
        /// </summary>
        public virtual bool IsActive
        {
            get { return is_Active; }
            set { is_Active = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return role_ID + "|" + user_ID;
        }

        #endregion
    }
}