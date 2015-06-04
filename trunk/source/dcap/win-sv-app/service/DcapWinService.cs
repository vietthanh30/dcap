using System;
using System.ServiceProcess;
using System.Timers;
using win_sv_app.common;
using win_sv_app.DcapServiceReference;

namespace win_sv_app.service
{
    public partial class DcapWinService : ServiceBase
    {
        private Timer timer1 = null;
        private DcapServiceSoapClient service;

        public DcapWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer();
            this.timer1.Interval = 30000; //every 30 secs
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            this.service = new DcapServiceSoapClient();
            timer1.Enabled = true;
            Library.WriteErrorLog("DCAP window service started");
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            try
            {
                service.CalculateAccountLog();
                service.CalculateBonusOfAccountTree();
                service.CalculateBonusOfManagerTree();
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex.Message);
            }
        }

        protected override void OnStop()
        {
            timer1.Enabled = false;
            Library.WriteErrorLog("DCAP window service stopped");
        }
    }
}
