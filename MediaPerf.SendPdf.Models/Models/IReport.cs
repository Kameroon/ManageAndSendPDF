namespace MediaPerf.SendPdf.Models.Models
{
    public interface IReport
    {
        string AdressePrestataire { get; set; }
        string BfpParam1 { get; set; }
        string BfpParam2 { get; set; }
        int CountCampagne { get; set; }
        int CountPv { get; set; }
        string CurrentDate { get; set; }
        int CurrentPage { get; set; }
        string Destinataire { get; set; }
        decimal HtMontant { get; set; }
        string Prestataire { get; set; }
        int RoyaltyFeeNumber { get; set; }
        int TotalPageNumber { get; set; }
        decimal TTCMontant { get; set; }
        decimal TVA { get; set; }
    }
}