using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.Models.Models
{
    public class FooterPage
    {
        public long IdBFP { get; set; }
        public long TxTva { get; set; }
        public long TotalHT { get; set; }
        public long TotalTVA { get; set; }
        public long TotalTTC { get; set; }
        public string BfpParam4 { get; set; }
        public string BfpParam5 { get; set; }
        public string BfpParam6 { get; set; }
        public string BfpParam7 { get; set; }
        public string BfpParam8 { get; set; }
    }
}
