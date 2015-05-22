// -----------------------------------------------------------------------
// <copyright file="ExportExcelOandaExchangeRate.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.NAPReport.OandaExChangeRate.Business.Services
{
    using System;
    using System.Collections;
    using System.Data;
    using System.IO;
    using Fanex.Data;
    using Fanex.Service.Core.Common;
    using Fanex.Service.Core.Extensions;
    using Fanex.Service.Core.Utilities;
    using NPOI.XSSF.UserModel;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ExportExcelOandaExchangeRate : ExportExcelOandaExchangeRateBase, IExportExcelOandaExchangeRate
    {
        public void ExportExcel(Dictionary<string, Dictionary<string, double>> allCurrenciesList, string remainingQuotes)
        {
            var baseCurrencies = AppSettings.BaseCurrencies;

            ExportHeader(baseCurrencies);

            ExportBody(baseCurrencies, allCurrenciesList);

            ExportFooter(remainingQuotes);

            SaveExcelFile();
        }

        private void ExportHeader(string[] baseCurrencies)
        {
            var row = worksheet.GetRow(_cellInfo.CurrentRow) as XSSFRow ??
                      worksheet.CreateRow(_cellInfo.CurrentRow) as XSSFRow;
            _cellInfo.CellValue = "Currency";
            NPOIHighVersion.RenderCell(ref worksheet, ref row, ref _cellInfo, _headerTextStyle);

            for (var i = 0; i < baseCurrencies.Length; i++)
            {
                NPOIUtility.IncreaseColumnIndex(ref _cellInfo);
                var columnName = baseCurrencies[i];
                _cellInfo.CellValue = columnName;
                NPOIHighVersion.RenderCell(ref worksheet, ref row, ref _cellInfo, _headerTextStyle);
            }

            NPOIUtility.IncreaseRowIndex(ref _cellInfo);
            NPOIUtility.ResetColumnIndex(ref _cellInfo);
        }

        private void ExportBody(string[] baseCurrencies, Dictionary<string, Dictionary<string, double>> allCurrenciesList)
        {
            foreach (var code in allCurrenciesList)
            {
                var row = worksheet.GetRow(_cellInfo.CurrentRow) as XSSFRow ?? worksheet.CreateRow(_cellInfo.CurrentRow) as XSSFRow;
                _cellInfo.CellValue = code.Key;
                NPOIHighVersion.RenderCell(ref worksheet, ref row, ref _cellInfo, _leftTextStyle);

                foreach (var rate in code.Value)
                {
                    NPOIUtility.IncreaseColumnIndex(ref _cellInfo);
                    _cellInfo.CellValue = rate.Value;
                    NPOIHighVersion.RenderCell(ref worksheet, ref row, ref _cellInfo, _decimalNumberStyle);
                }

                NPOIUtility.IncreaseRowIndex(ref _cellInfo);
                NPOIUtility.ResetColumnIndex(ref _cellInfo);
            }            
        }

        private void ExportFooter(string remainingQuotes)
        {
            NPOIUtility.IncreaseRowIndex(ref _cellInfo);

            var row = worksheet.GetRow(_cellInfo.CurrentRow) as XSSFRow ?? worksheet.CreateRow(_cellInfo.CurrentRow) as XSSFRow;
            _cellInfo.CellValue = "Remaining quotes: " + remainingQuotes;
            NPOIHighVersion.RenderCell(ref worksheet, ref row, ref _cellInfo, _leftTextStyle);
        }

        private void SaveExcelFile()
        {
            var reportName = "OandaExchangeRate_" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm") + ".xlsx";
            FolderPath = AppSettings.ExportDataDirectory + @"\" + reportName;
            SaveFile.SaveExcelFile(AppSettings.ExportDataDirectory, reportName, _workbook);

            GC.Collect();
        }


        public string FolderPath
        {
            get;
            set;
        }
    }
}
