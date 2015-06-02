using System;
using System.Collections.Generic;
using System.Threading;
using core_lib.common;
using test_ws_server.DcapServiceReference;

namespace test_ws_server
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DcapServiceReference.DcapServiceSoapClient();
            while (true)
            {
                service.CalculateAccountLog();
                service.CalculateBonusOfAccountTree();
                service.CalculateBonusOfManagerTree();
                Thread.Sleep(30000);
                Console.Out.Write(".");
            }
        }
    }
}
