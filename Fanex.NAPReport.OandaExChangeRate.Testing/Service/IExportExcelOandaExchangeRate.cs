// -----------------------------------------------------------------------
// <copyright file="IExportExcelOandaExchangeRate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.OandaExChangeRate.Testing.Service
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
        void ExportExcel(List<Dictionary<string, double>> allExchangeRate);
    }
}
