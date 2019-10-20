using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.Models.Models
{
    public class Report
    {
        public int CountPv { get; set; }
        public int CountCampagne { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPageNumber { get; set; }
        public string CurrentDate { get; set; }
        public int RoyaltyFeeNumber { get; set; }
        public string Prestataire { get; set; }
        public string AdressePrestataire { get; set; }
        public string Destinataire { get; set; }
        public string BfpParam1 { get; set; }
        public string BfpParam2 { get; set; }
        public decimal HtMontant { get; set; } = 533.39m;
        public decimal TVA { get; set; } = 106.67m;
        public decimal TTCMontant { get; set; } = 640.01m;
    }
}
