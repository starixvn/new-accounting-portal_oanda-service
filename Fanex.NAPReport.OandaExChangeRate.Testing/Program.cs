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
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            DbSettingProviderManager.Current.Start();
            MailClientHandler.Start();

            IMailingExChangeRate exChangerate = new MailingExChangeRate();
            //exChangerate.UpdateDailyExchangeRate();
            exChangerate.UpdateRemainingQuotes();

            Console.WriteLine("Done at " + DateTime.Now.ToString());

            MailClientHandler.Stop();

            Console.ReadLine();
        }
    }
}
