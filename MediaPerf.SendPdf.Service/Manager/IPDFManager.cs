namespace MediaPerf.SendPdf.Service.Manager
{
    public interface IPDFManager
    {
        bool CreatePDF(string repositoryPath);
    }
}