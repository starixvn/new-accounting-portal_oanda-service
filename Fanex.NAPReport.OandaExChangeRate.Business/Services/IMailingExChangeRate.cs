// -----------------------------------------------------------------------
// <copyright file="IMaillingExChangeRate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.NAPReport.OandaExChangeRate.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.MailDelivery.Client.Models;

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
