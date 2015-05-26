using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_app.common
{
    public class DcapServiceUtil
    {
        private static DcapServiceReference.DcapServiceSoapClient dcapService = new DcapServiceReference.DcapServiceSoapClient();

        public static string login(string userName, string password)
        {
            return dcapService.login(userName, password);
        }

        public static string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            return dcapService.changePassword(userName, oldPassword, newPassword, confirmPassword);
        }

        public static string createUser(string userName, string password, string confirmPassword)
        {
            return dcapService.createUser(userName, password, confirmPassword);
        }
    }
}