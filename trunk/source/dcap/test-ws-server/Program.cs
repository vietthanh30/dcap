using System;
using System.Collections.Generic;
using core_lib.common;
using test_ws_server.DcapServiceReference;

namespace test_ws_server
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DcapServiceReference.DcapServiceSoapClient();
            UserDto userDto = service.login("nguyenbh", "nguyenbh");
            Console.Out.WriteLine("login message: " + userDto.Message);
            userDto = service.login("nguyenbh", "123456");
            Console.Out.WriteLine("login Name: " + userDto.UserName);
            string result = service.changePassword("nguyenbh", "nguyenbh", "123456", "123456");
            Console.Out.WriteLine("changePassword code: " + result);
            result = service.changePassword("nguyenbh", "123456", "123456", "123456");
            Console.Out.WriteLine("changePassword code: " + result);
            result = service.changePassword("nguyenbh", "123456", "nguyenbh", "nguyenbh");
            Console.Out.WriteLine("changePassword code: " + result);
            result = service.changePassword("nguyenbh", "nguyenbh", "123456", "123456");
            Console.Out.WriteLine("changePassword code: " + result);
            result = service.CreateUser("NGUYENBH", "", "Bui Hai Nguyen", DateTime.Now.AddYears(-25), "031356789", DateTime.Now,
                "0982096374", "Ha Noi", "M", "987826742727", "Ha Noi", "~/dist/img/user2-160x160.jpg", "NGUYENBH");
            Console.Out.WriteLine("createUser code: " + result);
            var returnCode = service.CalculateAccountLog();
            Console.Out.WriteLine("CalculateAccountLog code: " + returnCode);
        }
    }
}
