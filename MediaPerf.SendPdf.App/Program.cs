using MediaPerf.SendPdf.Service.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string pdfPath = @"C:\Users\Sweet Family\Desktop\PdfFilesPath";

            PDFManager.CreatePDF(pdfPath);

            Console.WriteLine("The End.");
        }
    }
}
