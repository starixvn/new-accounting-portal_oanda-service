namespace Fanex.NAPReport.OandaExChangeRate.Business.Services.CurrencyConverter
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Xml;

    /*
     * Send a request to FXCSV:

       - Connect to http://www.oanda.com/cgi-bin/fxcsv/fxcsv and create your request with
        client_id=*********
        expr=list of currencies (or one currency). This is home currency
        exch=list of currencies (or leave empty to get all currencies). This is the quote currency.
        amount=amount for the exchange. Optional. Default value=1
        date=MM/DD/YYYY (starting date for the conversion). Optional. Default value=today
        nprices=number of conversions you wish to receive. Optional. Default value=1

        http://www.oanda.com/cgi-bin/fxcsv/fxcsv?client_id=[customerid]&expr=[list_home_currencies] &exch=[list_of_quote_currencies]
     */

    public class OandaConvertor : BaseCurrencyConvertor
    {
        private List<string> specialCurrencies = new List<string>
        {
            "IDR", "VND"
        };

        public override string GetExchageRate(string fromCurrency, string toCurrency, string rateDate)
        {
            string ask = "0", bid = "0";
            string date = Convert.ToDateTime(rateDate).ToString("yyyy-MM-dd");
            try
            {
                if (fromCurrency.Equals(toCurrency))
                {
                    return "1";
                }

                if (fromCurrency.ToLower() == "c" || toCurrency.ToLower() == "c"
                    || fromCurrency.ToLower() == "h" || toCurrency.ToLower() == "h"
                    || fromCurrency.ToLower() == "uus" || toCurrency.ToLower() == "uus")
                {
                    return ask;
                }

                var url = AppSettings.OandaUrl;
                url = string.Format(url, fromCurrency.Trim(), toCurrency.Trim(), date, AppSettings.APIKey);
                if (specialCurrencies.Contains(fromCurrency))
                {
                    url = url + "&decimal_places=8";
                }

                var request = WebRequest.Create(url);
                var response = request.GetResponse();

                XmlDocument xml = new XmlDocument();
                xml.Load(response.GetResponseStream());
                XmlNodeList xnlist = xml.SelectNodes("response/quotes/quote");

                foreach (XmlNode node in xnlist)
                {
                    var askTag = node.SelectNodes("ask");
                    if (askTag.Count > 0)
                    {
                        ask = askTag[0].InnerText;
                    }

                    var bidTag = node.SelectNodes("bid");
                    if (bidTag.Count > 0)
                    {
                        bid = bidTag[0].InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "fromCurrency: " + fromCurrency + ", toCurrency: " + toCurrency + ", date: " + date + ", Exception: " + ex.Message;
                throw new Exception(msg);
            }

            return ask;
        }

        public override double GetExchageRate_Simulate(Random random)
        {
            double ask = random.NextDouble();

            return ask;
        }
    }
}