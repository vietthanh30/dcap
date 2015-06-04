using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using win_sv_app;
using win_sv_app.service;

namespace wsv_dcap
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new DcapWinService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
