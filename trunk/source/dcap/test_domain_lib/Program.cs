using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using domain_lib.controller;

namespace test_domain_lib
{
    public class Program
    {
        static void Main(string[] args)
        {
            var service = new Controller();
            String result = service.login("nguyenbh", "nguyenbh");
            Console.Out.WriteLine("login code: " + result);
            result = service.login("nguyenbh", "123456");
            Console.Out.WriteLine("login code: " + result);
            result = service.changePassword("nguyenbh", "nguyenbh", "123456", "123456");
            Console.Out.WriteLine("changePassword code: " + result);
            result = service.changePassword("nguyenbh", "123456", "123456", "123456");
            Console.Out.WriteLine("changePassword code: " + result);
            result = service.changePassword("nguyenbh", "123456", "nguyenbh", "nguyenbh");
            Console.Out.WriteLine("changePassword code: " + result);
            result = service.changePassword("nguyenbh", "nguyenbh", "123456", "123456");
            Console.Out.WriteLine("changePassword code: " + result);
        }
    }
}
