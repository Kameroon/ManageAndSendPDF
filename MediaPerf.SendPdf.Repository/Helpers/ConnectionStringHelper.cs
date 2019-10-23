//using ConsoleApp.ConnectionString;
using System.Configuration;

namespace MediaPerf.SendPdf.Repository.Helpers
{
    public class ConnectionStringHelper : IConnectionStringHelper
    {
        public string GetConnectionString()
        {
            string connetionString = "Data Source=DESKTOP-6DSO6AT\\SQLEXPRESS; Database=JunkEFCodeFrist1; Trusted_Connection=True";

            if (ConfigurationManager.ConnectionStrings["MyDbConnectionString"] == null)
            {
                return connetionString;
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
            }

            //return ConnectionString.GetConnectionString();
        }
    }
}
