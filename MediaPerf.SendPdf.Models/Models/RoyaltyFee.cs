using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.Models.Models
{
    public class RoyaltyFee : IRoyaltyFee
    {

        public long IdPv { get; set; }
        public string Enseigne { get; set; }
        public string Commune { get; set; }
        public string Produit { get; set; }
        public string Dp { get; set; }
        public long IdCmp { get; set; }
        public long IdBFP { get; set; }
        public long Fk_S_TypeProduit { get; set; }
        public long MontantRdvcHT { get; set; }
        public string Campagne { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
    }
}
