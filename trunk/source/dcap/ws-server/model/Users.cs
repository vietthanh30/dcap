﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// User entity
/// </summary>
namespace ws_server.model
{
    public class Users
    {
        #region Declarations

        // Property variables
        private int user_ID = -1;

        // Member variables
        private string user_Name = String.Empty;

        private string password = String.Empty;

        #endregion

    	#region Constructor

        public Users()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// User ID
        /// </summary>
        public virtual int UserID
        {
            get { return user_ID; }
            set { user_ID = value; }
        }

        /// <summary>
        /// User name.
        /// </summary>
        public virtual string UserName
        {
            get { return user_Name; }
            set { user_Name = value; }
        }

        /// <summary>
        /// Password
        /// </summary>
        public virtual string Password
        {
            get { return password; }
            set { password = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return user_Name;
        }

        #endregion
    }
}