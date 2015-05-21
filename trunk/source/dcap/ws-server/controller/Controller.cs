using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ws_server.persistence;

namespace ws_server.controller
{
    public class Controller
    {
        #region Declarations

        // Member variables
        PersistenceManager m_PersistenceManager = new PersistenceManager();
        
        // Property variables

        #endregion

        #region Constructor

        public Controller()
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        /// <summary>
        /// Clears all records from the database.
        /// </summary>
        /// <remarks>We use this method to reset the database at the beginning or each run.</remarks>
        public void ClearDatabase()
        {
            m_PersistenceManager.ClearDatabase();
        }

        /// <summary>
        /// Converts an ICollection of dictionary keys to a string array.
        /// </summary>
        /// <param name="keys">The ICollection of keys to convert.</param>
        /// <returns>A string array of keys.</returns>
        public string[] ConvertKeys(ICollection<string> keys)
        {
            int i = 0;
            string[] keyArray = new string[keys.Count];
            foreach (string key in keys)
            {
                keyArray[i] = key;
                i++;
            }
            return keyArray;
        }

        #endregion
    }
}