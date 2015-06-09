using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using core_lib.common;
using web_app.DcapServiceReference;

namespace web_app.common
{
    public class UserUtil
    {
        public static string GetRoleCode(UserDto userDto)
        {
            if (userDto == null)
            {
                return ConstUtil.QLTV;
            }
            var allRoles = userDto.AllRoles;
            if (allRoles.Length == 0)
            {
                return ConstUtil.QLTV;
            }
            return allRoles[0].RoleCode;
        }

        public static bool IsQthtRole(UserDto userDto)
        {
            return string.Compare(GetRoleCode(userDto), ConstUtil.QTHT, true) == 0;
        }

        public static bool IsQlktRole(UserDto userDto)
        {
            return string.Compare(GetRoleCode(userDto), ConstUtil.QLKT, true) == 0;
        }

        public static bool IsQltvRole(UserDto userDto)
        {
            return string.Compare(GetRoleCode(userDto), ConstUtil.QLTV, true) == 0;
        }
    }
}