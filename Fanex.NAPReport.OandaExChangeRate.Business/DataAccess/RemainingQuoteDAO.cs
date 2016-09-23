// -----------------------------------------------------------------------
// <copyright file="RemainingQuote.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.OandaExChangeRate.Business.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Net;
    using System.Xml;
    using Fanex.AccountingReports.SqLiteDatabase;
    using Fanex.Common;
    using Fanex.NAPReport.OandaExChangeRate.Business;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RemainingQuoteDAO : IRemainingQuoteDAO
    {
        private SqLite _db = null;

        public RemainingQuoteDAO()
        {
            _db = new SqLite(AppSettings.ConnectionString);
        }

        public string GetRemainningQuote()
        {
            if(AppSettings.IsSimulateRate)
            {
                return "0";
            }

            string url = AppSettings.RemainingQuoteUrl;

            var request = WebRequest.Create(url);
            var response = request.GetResponse();

            XmlDocument xml = new XmlDocument();
            xml.Load(response.GetResponseStream());
            XmlNodeList xnlist = xml.SelectNodes("response");

            int quotes = 0;
            foreach (XmlNode node in xnlist)
            {
                var quotesTag = node.SelectNodes("remaining_quotes");
                if (quotesTag.Count > 0)
                {
                    quotes = Convert.ToInt32(quotesTag[0].InnerText);
                }
            }

            return string.Format("{0:#,##0}", quotes);
        }

        public void Insert(int qoutes)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Remaining_quotes", qoutes.ToString());
            data.Add("CreatedDate", DateTime.Now.ToString());
            Converter.ToInt(_db.Insert("Log", data));
        }

        public void Delete()
        {
            string sql = "delete from log where id < (select max(id) - " + AppSettings.RemainingHistory + " from log)";
            _db.ExecuteNonQuery(sql);
        }

        public DataTable SelectAll()
        {
            string sql = "select * from Log";
            DataTable data = _db.GetDataTable(sql);

            return data;
        }

        public int SelectLastest()
        {
            string sql = "select * from Log where Id  in (select max(Id) from Log)";
            DataTable data = _db.GetDataTable(sql);
            int qoutes = 0;
            if (data != null && data.Rows.Count > 0)
            {
                qoutes = Converter.ToInt(data.Rows[0]["Remaining_quotes"]);
            }

            return qoutes;
        }
    }
}