namespace Fanex.NAPReport.OandaExChangeRate.Business.DataAccess
{
    using System;
    using System.Collections.Generic;
    using Fanex.NAPReport.OandaExChangeRate.Business.Model;

    public interface IExchangeRateDAO
    {
        List<string> GetCurrencyCodeList();

        Result UpdateRate(string fromCurrency, DateTime runDate, Dictionary<string, double> toCurrency);

        Result UpdateIbcRate();
    }
}
