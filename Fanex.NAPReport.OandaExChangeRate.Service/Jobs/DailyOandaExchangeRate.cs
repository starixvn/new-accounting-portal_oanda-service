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
    using Quartz;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DailyOandaExchangeRate : JobBase
    {
        protected override JobResultMessage Run(Schedule schedule, IJobExecutionContext jobContext)
        {
            IMailingExChangeRate service = new MailingExChangeRate();
            Mail mail = service.UpdateDailyExchangeRate();

            return new JobResultMessage()
            {
                JobStatus = JobStatus.Success,
                Mail = mail,
                Schedule = schedule,
                Result = new ResultMessage
                {
                    ErrCode = 0,
                    ErrMsg = string.Empty
                }
            };
        }
    }
}
