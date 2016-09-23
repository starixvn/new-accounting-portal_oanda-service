// -----------------------------------------------------------------------
// <copyright file="IRemainingQuote.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.OandaExChangeRate.Business.DataAccess
{
    using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IRemainingQuoteDAO
    {
        string GetRemainningQuote();

        DataTable SelectAll();

        int SelectLastest();

        void Insert(int qoutes);

        void Delete();
    }
}