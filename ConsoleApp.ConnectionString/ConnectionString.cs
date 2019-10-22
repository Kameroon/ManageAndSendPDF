using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ConnectionString
{
    public class ConnectionString
    {
        public static string GetConnectionString()
        {
            //string connetionString = "Data Source=DESKTOP-6DSO6AT\\SQLEXPRESS; Database=JunkEFCodeFrist1; Trusted_Connection=True";

            //if (ConfigurationManager.ConnectionStrings["MyDbConnectionString"] == null)
            //{
            //    return connetionString;
            //}
            //else
            //{
            //    return ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
            //}

            return ConfigurationManager.ConnectionStrings["MyDbConnectionString"].ConnectionString;
        }
    }
}
