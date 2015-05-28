using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ws_server.model
{
    public class ManagerLevel
    {
        #region Declarations

        // Member variables
        protected long account_ID = -1;

        protected long parent_ID = -1;

        protected long child_Index = -1;

        protected string all_Child = string.Empty;

        protected long level = -1;

        protected long level_Index = -1;

        protected string is_Active = string.Empty;

        protected DateTime created_Date = default(DateTime);

        protected string created_By = string.Empty;

        
        #endregion

    	#region Constructor

        public ManagerLevel()
        {
        }

    	#endregion

        #region Properties

        /// <summary>
        /// Account ID
        /// </summary>
        public virtual long AccountId
        {
            get { return account_ID; }
            set { account_ID = value; }
        }

        /// <summary>
        /// Parent ID
        /// </summary>
        public virtual long ParentId
        {
            get { return parent_ID; }
            set { parent_ID= value; }
        }


        /// <summary>
        /// Parent ID
        /// </summary>
        public virtual long ChildIndex
        {
            get { return child_Index; }
            set { child_Index = value; }
        }

        /// <summary>
        /// All Child
        /// </summary>
        public virtual string AllChild
        {
            get { return all_Child; }
            set { all_Child = value; }
        }

        /// <summary>
        /// Level
        /// </summary>
        public virtual long Level
        {
            get { return level; }
            set { level = value; }
        }

        /// <summary>
        /// Level Index
        /// </summary>
        public virtual long LevelIndex
        {
            get { return level_Index; }
            set { level_Index = value; }
        }

        /// <summary>
        /// Is Active
        /// </summary>
        public virtual string IsActive
        {
            get { return is_Active; }
            set { is_Active= value; }
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
        /// Created By
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
            return parent_ID + ":" + account_ID;
        }

        #endregion
    }
}