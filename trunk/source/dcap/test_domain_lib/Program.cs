using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using core_lib.common;
using domain_lib.controller;
using domain_lib.dto;

namespace test_domain_lib
{
    public class Program
    {
        static void Main(string[] args)
        {
            var service = new Controller();
//            UserDto userDto = service.login("nguyenbh", "nguyenbh");
//            Console.Out.WriteLine("login message: " + userDto.Message);
//            userDto = service.login("nguyenbh", "123456");
//            Console.Out.WriteLine("login Name: " + userDto.UserName);
//            string result = service.changePassword("nguyenbh", "nguyenbh", "123456", "123456");
//            Console.Out.WriteLine("changePassword code: " + result);
//            result = service.changePassword("nguyenbh", "123456", "123456", "123456");
//            Console.Out.WriteLine("changePassword code: " + result);
//            result = service.changePassword("nguyenbh", "123456", "nguyenbh", "nguyenbh");
//            Console.Out.WriteLine("changePassword code: " + result);
//            result = service.changePassword("nguyenbh", "nguyenbh", "123456", "123456");
//            Console.Out.WriteLine("changePassword code: " + result);
//            result = service.CreateUserAdmin("NGUYENHAIMINH03", "Nguyen Hai Minh", "QTHT", "NGUYENBH");
//            Console.Out.WriteLine("CreateUserAdmin code: " + result);
//            var test = "nguyenvana";
//            test = test + Char.ToString((char) ('a' + 1));
//            Console.Out.WriteLine("  " + test);
//            var strThang = DateUtil.GetDateTimeAsStringWithEnProvider(DateTime.Now, ConstUtil.MONTH_FORMAT);
//            Console.Out.WriteLine("  " + strThang);
            var result = service.SearchBangKe(DateTime.Now.AddDays(-15));
            Console.Out.WriteLine("SearchBangKe code: " + result);
        }
    }
}
