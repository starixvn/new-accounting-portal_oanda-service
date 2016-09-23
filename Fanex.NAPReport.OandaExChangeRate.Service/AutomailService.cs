// -----------------------------------------------------------------------
// <copyright file="AutomailService.cs" company="Nexcel Solutions Vietnam.">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.NAPReport.OandaExChangeRate.Service
{
    using System;
    using System.IO;
    using System.ServiceProcess;
    using Fanex.Data.Configuration;
    using Fanex.MailDelivery.Client.Handlers;
    using Fanex.NAPReport.OandaExChangeRate.Business;
    using System.Timers;
    using Fanex.NAPReport.OandaExChangeRate.Business.Services;
    using ONEbook.UM.Services;
    using ONEbook.UM.Services.Impl;    /// <summary>
                                       /// Auto mail service.
                                       /// </summary>
    public partial class AutoMailService : ServiceBase
    {
        /// <summary>
        /// The timer.
        /// </summary>
        private static Timer timer;

        public AutoMailService()
        {
            InitializeComponent(AppSettings.ServiceName);
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = AppSettings.Interval;
            timer.Elapsed += new ElapsedEventHandler(OnInterval);
            timer.AutoReset = true;
            timer.Enabled = true;

            AppSettings.OnProcessing = false;

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            DbSettingProviderManager.Current.Start();

            MailClientHandler.Start();
        }

        protected override void OnStop()
        {
            MailClientHandler.Stop();

            timer.Stop();
            timer = null;
            AppSettings.OnProcessing = false;

            GC.Collect();
        }

        private static void OnInterval(object source, ElapsedEventArgs e)
        {
            if (!AppSettings.OnProcessing)
            {
                AppSettings.OnProcessing = true;
                var um = IsUnderMaintenance();
                if (!um)
                {
                    IMailingExChangeRate exchangeRate = new MailingExChangeRate();

                    if (!AppSettings.IsSimulateRate)
                    {
                        exchangeRate.UpdateRemainingQuotes();
                    }
                }

                AppSettings.OnProcessing = false;
            }
        }

        private static bool IsUnderMaintenance()
        {
            IMaintenanceService maintenanceService = new MaintenanceService();
            return maintenanceService.IsUnderMaintenance();
        }
    }
}