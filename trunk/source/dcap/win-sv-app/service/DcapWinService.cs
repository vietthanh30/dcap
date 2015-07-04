using System;
using System.ServiceProcess;
using System.Timers;
using core_lib.common;
using domain_lib.controller;
using win_sv_app.common;

namespace win_sv_app.service
{
    public partial class DcapWinService : ServiceBase
    {
        private Timer timer1 = null;
        private Controller service;

        public DcapWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            long intervalTime;
            if (!long.TryParse(ParameterUtil.GetParameter("IntervalTime"), out intervalTime))
            {
                intervalTime = 300000; //every 5 minutes
            }
			double bonusTypeTtAmount;
			if (!double.TryParse(ParameterUtil.GetParameter("BonusTypeTtAmount"), out bonusTypeTtAmount))
            {
                bonusTypeTtAmount = 0.7;
            }
			ConstUtil.BONUS_TYPE_TT.Amount = bonusTypeTtAmount;
            timer1 = new Timer();
            this.timer1.Interval = intervalTime;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Tick);
            this.service = new Controller();
            timer1.Enabled = true;
            timer1.Start();
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
