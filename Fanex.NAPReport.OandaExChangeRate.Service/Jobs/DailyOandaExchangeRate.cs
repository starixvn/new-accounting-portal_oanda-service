// -----------------------------------------------------------------------
// <copyright file="OandaExchangeRate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.NAPReport.OandaExChangeRate.Service.Jobs
{
    using Fanex.MailDelivery.Client.Enumerations;
    using Fanex.MailDelivery.Client.Jobs;
    using Fanex.MailDelivery.Client.Messages;
    using Fanex.MailDelivery.Client.Models;
    using Fanex.NAPReport.OandaExChangeRate.Business.Services;
    using ONEbook.UM.Services;
    using ONEbook.UM.Services.Impl;
    using Quartz;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DailyOandaExchangeRate : JobBase
    {
        protected override JobResultMessage Run(Schedule schedule, IJobExecutionContext jobContext)
        {
            var um = IsUnderMaintenance();
            if (!um)
            {
                IMailingExChangeRate service = new MailingExChangeRate();
                Mail mail = service.UpdateDailyExchangeRate();

                return new JobResultMessage()
                {
                    JobStatus = JobStatus.Success,
                    Mail = mail,
                    Schedule = schedule
                };
            }
            else
            {
                return new JobResultMessage()
                {
                    JobStatus = JobStatus.DataIsNotReady,
                    Mail = null,
                    Schedule = schedule
                };
            }
        }

        private bool IsUnderMaintenance()
        {
            IMaintenanceService maintenanceService = new MaintenanceService();
            return maintenanceService.IsUnderMaintenance();
        }
    }
}