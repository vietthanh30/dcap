using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace core_lib.common
{
    public class BonusType
    {
        #region Declarations

        // Member variables

        private string _type = string.Empty;

        private double _amount = -1;

        #endregion

    	#region Constructor

        public BonusType()
        {
        }

    	#endregion

        #region Properties
        
        /// <summary>
        /// Type
        /// </summary>
        public virtual string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Amount
        /// </summary>
        public virtual double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        #endregion

        #region Method Overrides

        public override string ToString()
        {
            return _type;
        }

        #endregion
    }
}
