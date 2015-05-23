using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ws_server
{
    /// <summary>
    /// Summary description for DcapService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DcapService : System.Web.Services.WebService
    {
        [WebMethod]
        public string login(String userName, String password)
        {
            return "";
        }

        [WebMethod]
        public string changePassword(String userName, String oldPassword, String newPassword, String confirmPassword)
        {
            return "";
        }

        [WebMethod]
        public string createUser(String userName, String password, String confirmPassword)
        {
            return "";
        }
    }
}