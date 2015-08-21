using System;
using System.IO;
using Fanex.Data.Configuration;
using Fanex.NAPReport.OandaExChangeRate.Business.Services;
using Fanex.MailDelivery.Client.Handlers;

namespace Fanex.NAPReport.OandaExChangeRate.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

                DbSettingProviderManager.Current.Start();
                

                IMailingExChangeRate exChangerate = new MailingExChangeRate();
                exChangerate.UpdateDailyExchangeRate();

                Console.WriteLine("Done at " + DateTime.Now.ToString());

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
