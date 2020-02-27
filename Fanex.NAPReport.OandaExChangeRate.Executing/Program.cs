using Fanex.Data.Configuration;
using Fanex.MailDelivery.Core.Models;
using Fanex.NAPReport.OandaExChangeRate.Business.Services;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Fanex.NAPReport.OandaExChangeRate.Executing
{
    internal static class Program
    {
        private static void Main()
        {
            var result = new JobMessage();
            try
            {
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                DbSettingProviderManager.Current.Start();

                IMailingExChangeRate exchangerate = new MailingExChangeRate();
                var mail = exchangerate.UpdateDailyExchangeRate();
                result = new JobMessage
                {
                    Mail = mail,
                    JobStatus = JobStatus.Success
                };
            }
            catch (Exception ex)
            {
                result.JobStatus = JobStatus.DataIsNotReady;
                result.TaskLog = new TaskLog
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                };
            }
            finally
            {
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                Console.WriteLine(json);
                Environment.Exit(0);
            }
        }
    }
}