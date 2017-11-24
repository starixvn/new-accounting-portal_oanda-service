// -----------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
namespace Fanex.NAPReport.OandaExChangeRate.Business
{
    using System;
    using System.Configuration;
    using Fanex.Common;
    using Fanex.Service.Core.Utilities;

    /// <summary>
    /// Application setting.
    /// </summary>
    public class AppSettings : AppSettingBase
    {
        public static new string ServiceName
        {
            get
            {
                return GetSettingValueByKey("ServiceName");
            }
        }

        public static new int Interval
        {
            get
            {
                return Converter.ToInt(GetSettingValueByKey("Interval.Second")) * 1000;
            }
        }

        public static new string ExportDataDirectory
        {
            get
            {
                return ExportDataDirectory("Fanex.OandaExchangeRate.ExportDataDirectory");
            }
        }
      
        public static string MailSubject
        {
            get
            {
                var val = ConfigurationManager.AppSettings["OandaExchangeRate.Subject"];
                val = val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["OandaExchangeRate.Subject"]);
                val = string.Format(val, DateTime.Today.ToString("yyyy/MM/dd"));

                return val;
            }
        }

        public static string MailSubjectRemainingQuotes
        {
            get
            {
                var val = ConfigurationManager.AppSettings["OandaExchangeRate.SubjectRemainingQuotes"];
                val = val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["OandaExchangeRate.SubjectRemainingQuotes"]);
                val = string.Format(val, DateTime.Today.ToString("yyyy/MM/dd"));

                return val;
            }
        }

        public static string MailTitle
        {
            get
            {
                var val = ConfigurationManager.AppSettings["OandaExchangeRate.Title"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["OandaExchangeRate.Title"]);
            }
        }

        public static string[] BaseCurrencies
        {
            get
            {
                var strCurrencies = ConfigurationManager.AppSettings["OandaExchangeRate.BaseCurrencies"].ToString();
                var arrCurrencies = strCurrencies.Split(',');
                return arrCurrencies;
            }
        }

        public static bool IsInsertHistoryRate 
        { 
            get 
            {
                return ConfigurationManager.AppSettings["OandaExchangeRate.IsInsertHistoryRate"].Equals("1"); 
            } 
        }

        public static DateTime FromHistoryDate
        {
            get
            {
                return Convert.ToDateTime(ConfigurationManager.AppSettings["OandaExchangeRate.FromHistoryDate"]);
            }
        }

        public static DateTime ToHistoryDate
        {
            get
            {
                return Convert.ToDateTime(ConfigurationManager.AppSettings["OandaExchangeRate.ToHistoryDate"]);
            }
        }

        public static string OandaUrl
        {
            get { return ConfigurationManager.AppSettings["OandaExchangeRate.OandaUrl"].ToString(); }
        }

        public static bool IsSimulateRate
        {
            get { return GetSettingValueByKey("OandaExchangeRate.IsSimulateRate") == "1"; }
        }

        public static bool IsTest
        {
            get { return GetSettingValueByKey("OandaExchangeRate.IsTest") == "1"; }
        }

        public static DateTime TestDate
        {
            get { return Convert.ToDateTime(GetSettingValueByKey("OandaExchangeRate.TestDate")); }
        }

        public static bool IsEncryptionKey
        {
            get { return GetSettingValueByKey("OandaExchangeRate.IsEncryptionKey") == "1"; }
        }

        public static string APIKey
        {
            get 
            {
                string key = ConfigurationManager.AppSettings["OandaExchangeRate.APIKey"].ToString(); 
                if (IsEncryptionKey)
                {
                    key = Fanex.Common.Cryptography.CfsDeCrypt(key);
                }

                return key;
            }
        }

        public static string RemainingQuoteUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["OandaExchangeRate.RemainingQuoteUrl"].ToString();
                url = string.Format(url, APIKey);
                
                return url;
            }
        }

        public static string ConnectionString
        {
            get 
            {
                return @"Data Source=SQLiteDB\AutoMail-ExchangeRateOanda.s3db;Version=3;";
            }
        }

        public static int MailingListId
        {
            get
            {
                return Converter.ToInt(GetSettingValueByKey("OandaExchangeRate.MailingListId"));
            }
        }

        public static int RemainingHistory
        {
            get
            {
                return Converter.ToInt(GetSettingValueByKey("OandaExchangeRate.RemainingHistory"));
            }
        }

        public static int RemainingVariance
        {
            get
            {
                return Converter.ToInt(GetSettingValueByKey("OandaExchangeRate.RemainingVariance"));
            }
        }

        public static new bool OnProcessing
        {
            get;
            set;
        }

        public static string NapReportURL
        {
            get 
            {
                return GetSettingValueByKey("OandaExchangeRate.NapReportURL");
            }
        }
    }
}
