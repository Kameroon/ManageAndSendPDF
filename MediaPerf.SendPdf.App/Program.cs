using MediaPerf.SendPdf.Models.Models;
using MediaPerf.SendPdf.Repository.Helpers;
using MediaPerf.SendPdf.Repository.Repository;
using MediaPerf.SendPdf.Service.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MediaPerf.SendPdf.App
{
    class Program
    {
        /*
         * 
            static ITestInjectedClass _testInjectedClass;
            private static IUserRepository _userRepository;
            static void Main(string[] args)
            { 
                _testInjectedClass= new TestInjectedClass(_userRepository);
                _testInjectedClass.UserRepoRun();

                Console.ReadLine();

            } 
         
         * 
         * */


        private static IReport _report;
        private static IPDFRepository _pdfRepository;
        private static IPDFManager _pdfManager;
        private static IConsolidateHelper _consolidateHelper;

        static void Main(string[] args)
        {
            // -- Call initialize --
            Initialize();

            //PDFManager.GetMultipleDataSet();

            string pdfPath = @"C:\Users\Sweet Family\Desktop\PdfFilesPath";

            _pdfManager = new PDFManager(_report,
                                _pdfRepository,
                                _consolidateHelper);

            _pdfManager.CreatePDF(pdfPath);

            Console.WriteLine("The End.");
        }


        /// <summary>
        /// 
        /// </summary>
        private static void Initialize()
        {
            var container = new UnityContainer();
            container.RegisterType<IAuthor, Author>();
            container.RegisterType<IReport, Report>();
            container.RegisterType<IHeaderPage, HeaderPage>();
            container.RegisterType<IFooterPage, FooterPage>();
            container.RegisterType<IRoyaltyFee, RoyaltyFee>();
            container.RegisterType<IHeaderPage, HeaderPage>();
            container.RegisterType<IPDFRepository, PDFRepository>();
            container.RegisterType<IConsolidateHelper, ConsolidateHelper>();
            container.RegisterType<IConnectionStringHelper, ConnectionStringHelper>();

            // --   --
            _report = container.Resolve<Report>();
            _pdfRepository = container.Resolve<PDFRepository>();
            _consolidateHelper = container.Resolve<ConsolidateHelper>();
        }

    }
}
