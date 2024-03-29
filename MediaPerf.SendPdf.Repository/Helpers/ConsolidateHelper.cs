﻿using MediaPerf.SendPdf.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.Repository.Helpers
{
    public class ConsolidateHelper : IConsolidateHelper
    {
        private readonly IReport _report;

        public ConsolidateHelper(IReport report)
        {
            _report = report;
        }
       
        public IReport ConsolidateReport(DataTable dataTable)
        {
            var dataSet = dataTable.Rows;

            _report.CurrentDate = DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            _report.RoyaltyFeeNumber = 86327;
            _report.BfpParam1 = "When an object of this type is passed to a PdfDocument (class), every element added to this document will be written to the file specified.";
            _report.BfpParam2 = "Create a new PdfPage class using the addNewPage() method of the PdfDocument class.";

            _report.Destinataire = "MEDIAPERFORMANCES";
            _report.Prestataire = "CARREFOUR MARKET (94258)";
            _report.AdressePrestataire = "24 QUAI GALLIENI 92150 SURESNES FRANCE";
            _report.CountCampagne = 33;
            _report.TotalPageNumber = 3;
            _report.CurrentPage = 1;
            _report.CountPv = 2;

            foreach (DataRow dataRow in dataSet)
            {

                //_report.AdressePrestataire = dataRow["BookTitle"].ToString();
            }

            var results = from myRow in dataTable.AsEnumerable()
                          where myRow.Field<int>("Age") == 1
                          select myRow;

            string[] columnNames = (from dc in dataTable.Columns.Cast<DataColumn>()
                                    select dc.ColumnName).ToArray();

            return _report;
        }
    }
}
