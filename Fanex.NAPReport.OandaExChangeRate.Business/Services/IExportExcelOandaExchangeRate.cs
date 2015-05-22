// -----------------------------------------------------------------------
// <copyright file="IExportExcelOandaExchangeRate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.NAPReport.OandaExChangeRate.Business.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IExportExcelOandaExchangeRate
    {
        void ExportExcel(Dictionary<string, Dictionary<string, double>> allCurrenciesList, string remainingQuotes);

        string FolderPath { get; set; }
    }
}
