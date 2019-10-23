using System;

namespace MediaPerf.SendPdf.Models.Models
{
    public interface IRoyaltyFee
    {
        string Campagne { get; set; }
        string Commune { get; set; }
        DateTime DateDebut { get; set; }
        DateTime DateFin { get; set; }
        string Dp { get; set; }
        string Enseigne { get; set; }
        long Fk_S_TypeProduit { get; set; }
        long IdBFP { get; set; }
        long IdCmp { get; set; }
        long IdPv { get; set; }
        long MontantRdvcHT { get; set; }
        string Produit { get; set; }
    }
}