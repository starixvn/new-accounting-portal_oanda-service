// -----------------------------------------------------------------------
// <copyright file="MailingExChangeRate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.NAPReport.OandaExChangeRate.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Threading;
    using Fanex.Common;
    using Fanex.MailDelivery.Client.Handlers;
    using Fanex.MailDelivery.Client.Models;
    using Fanex.OandaExChangeRate.Business.DataAccess;
    using Fanex.OandaExChangeRate.Business.Model;
    using RazorEngine;
    using Fanex.Service.Core.Extensions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MailingExChangeRate : IMailingExChangeRate
    {
        IRemainingQuoteDAO _remainingQuoteDAO;
        public MailingExChangeRate()
        {
            _remainingQuoteDAO = new RemainingQuoteDAO();
        }

        public Mail UpdateDailyExchangeRate()
        {
            Mail mail = new Mail();

            var exchageRateService = new ExRateService();
            var baseCurrencies = AppSettings.BaseCurrencies;

            exchageRateService.UpdateRate(baseCurrencies);
            exchageRateService.UpdateIbcRate();

            mail.Subject = AppSettings.MailSubject;
            mail.Title = AppSettings.MailTitle;
            mail.Body = exchageRateService.BodyContent;

            mail.Attachments = new List<MailAttachment>();
            mail.Attachments.Add(new MailAttachment(@exchageRateService.FolderPath, MailDelivery.Client.Enumerations.FileExtension.MS_EXCEL));
            
            return mail;
        }

        public void UpdateExchangeRateManually()
        {
            var rateDate = new DateTime();
            try
            {
                var exchageRateService = new ExRateService();
                var baseCurrencies = AppSettings.BaseCurrencies;

                if (AppSettings.IsTest)
                {
                    exchageRateService.UpdateRate(baseCurrencies, AppSettings.TestDate);
                }
                else if (AppSettings.IsInsertHistoryRate)
                {
                    rateDate = AppSettings.ToHistoryDate;
                    var fromDate = AppSettings.FromHistoryDate;

                    while (!rateDate.ToShortDateString().Equals(fromDate.ToShortDateString()))
                    {
                        exchageRateService.UpdateRate(baseCurrencies, rateDate);
                        rateDate = rateDate.AddDays(-1);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    exchageRateService.UpdateRate(baseCurrencies);
                    exchageRateService.UpdateIbcRate();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateRemainingQuotes()
        {
            try
            {
                Mail mail = new Mail();

                string quotes = _remainingQuoteDAO.GetRemainningQuote();

                int currentQuotes = Converter.ToInt(quotes.Replace(",", string.Empty)); 
                int quotesFromDB = _remainingQuoteDAO.SelectLastest();
                int variance = Math.Abs(quotesFromDB - currentQuotes);
                if (variance >= AppSettings.RemainingVariance)
                {
                    _remainingQuoteDAO.Insert(currentQuotes); 
                    _remainingQuoteDAO.Delete();

                    mail.Body = GenerateBody(_remainingQuoteDAO.SelectAll());
                    mail.Subject = AppSettings.MailSubjectRemainingQuotes;
                    mail.Title = AppSettings.MailTitle;
                    mail.MailingListId = AppSettings.MailingListId;

                    MailClientHandler.SendMail(mail);
                }
            }
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
            }
        }

        private string GenerateBody(DataTable data)
        {
            List<RemainingQuote> quotes = new List<RemainingQuote>();
            foreach (DataRow row in data.Rows)
            {
                quotes.Add(new RemainingQuote() 
                {
                    CreatedDate = Converter.ToDateTime(row["CreatedDate"]),
                    Quotes = string.Format("{0:#,##0}", Converter.ToInt(row["Remaining_quotes"]))
                });
            }

            string appLocation = AppDomain.CurrentDomain.BaseDirectory;
            var template = string.Empty;
            template = File.ReadAllText(appLocation + @"/Templates/EmailTemplate.cshtml");

            var content = Razor.Parse(template, quotes);

            return content;
        }
    }
}
