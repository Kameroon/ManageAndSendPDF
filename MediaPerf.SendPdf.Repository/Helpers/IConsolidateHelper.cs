using System.Data;
using MediaPerf.SendPdf.Models.Models;

namespace MediaPerf.SendPdf.Repository.Helpers
{
    public interface IConsolidateHelper
    {
        IReport ConsolidateReport(DataTable dataTable);
    }
}