namespace MediaPerf.SendPdf.Models.Models
{
    public interface IHeaderPage
    {
        string AdressePrestataire { get; set; }
        string BfpParam3 { get; set; }
        string BfpParam4 { get; set; }
        string Destinataire { get; set; }
        string Prestataire { get; set; }
    }
}