namespace MediaPerf.SendPdf.Models.Models
{
    public interface IFooterPage
    {
        string BfpParam4 { get; set; }
        string BfpParam5 { get; set; }
        string BfpParam6 { get; set; }
        string BfpParam7 { get; set; }
        string BfpParam8 { get; set; }
        long IdBFP { get; set; }
        long TotalHT { get; set; }
        long TotalTTC { get; set; }
        long TotalTVA { get; set; }
        long TxTva { get; set; }
    }
}