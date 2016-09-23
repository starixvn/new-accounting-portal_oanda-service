using System;
using System.IO;
using Fanex.Data.Configuration;
using Fanex.NAPReport.OandaExChangeRate.Business.Services;

namespace Fanex.NAPReport.OandaExChangeRate.Testing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

                DbSettingProviderManager.Current.Start();

                IMailingExChangeRate exchangerate = new MailingExChangeRate();
                exchangerate.UpdateDailyExchangeRate();

                Console.WriteLine("Done at " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}