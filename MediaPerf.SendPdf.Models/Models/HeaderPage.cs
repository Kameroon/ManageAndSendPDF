using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.Models.Models
{
    public class HeaderPage
    {
        public string Destinataire { get; set; }
        public string BfpParam3 { get; set; }
        public string BfpParam4 { get; set; }
        public string Prestataire { get; set; }
        public string AdressePrestataire { get; set; }
    }
}
