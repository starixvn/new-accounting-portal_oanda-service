// -----------------------------------------------------------------------
// <copyright file="IMaillingExChangeRate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Fanex.MailDelivery.Core.Models;

namespace Fanex.NAPReport.OandaExChangeRate.Business.Services
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMailingExChangeRate
    {
        Mail UpdateDailyExchangeRate();

        void UpdateExchangeRateManually();

        void UpdateRemainingQuotes();
    }
}