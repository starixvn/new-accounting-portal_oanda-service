namespace Fanex.NAPReport.OandaExChangeRate.Business.Services.CurrencyConverter
{
    using System;
    using System.Configuration;

    public abstract class BaseCurrencyConvertor
    {
        public abstract string GetExchageRate(string fromCurrency, string toCurrency, string rateDate);

        public abstract double GetExchageRate_Simulate(Random random);
    }
}
