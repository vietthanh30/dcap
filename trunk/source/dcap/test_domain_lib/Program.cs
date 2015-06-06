using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using core_lib.common;
using domain_lib.controller;
using domain_lib.dto;
using domain_lib.model;

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
//            var result = service.SearchBangKe(DateTime.Now.AddDays(-15));
//            Console.Out.WriteLine("SearchBangKe code: " + result);
//            var text = String.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);
//            Console.Out.WriteLine("  " + text);
//            var allMemberInfos = service.RetrieveAll<MemberInfo>();
//            foreach (var memberInfo in allMemberInfos)
//            {
//                memberInfo.HoTenKd = VnStringHelper.toEnglish(memberInfo.HoTen);
//                service.Save(memberInfo);
//            }
//            var returnCode = service.CreateUser("", "", "Trần Thị Hương", null, "011405182", null, "", "", "", "", "",
//                                                "", "NGUYENBH");
//            Console.Out.WriteLine("CreateUser code: " + returnCode);
//            var result = service.SearchMemberNodeDto("");
//            Console.Out.WriteLine("SearchBangKe code: " + result);

            service.CalculateAccountLog();
            service.CalculateBonusOfAccountTree();
            service.CalculateBonusOfManagerTree();
            Thread.Sleep(30000);
            Console.Out.Write(".");
        }
    }
}
