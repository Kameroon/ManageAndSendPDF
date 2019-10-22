using Dapper;
using MediaPerf.SendPdf.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.Repository.Repository
{
    public class PDFRepository
    {

        public static void GetData()
        {
            string query = @"SELECT TOP 1000 [PersonId]
                  ,[FirstName]
                  ,[Adress_Street]
                  ,[State]
                  ,[DateAdded]
                  ,[PersonTypeId]
                  ,[DateModified]
              FROM[JunkEFCodeFrist1].[dbo].[People]";

            query += @"SELECT TOP 1000 [CompagnyId]
                  ,[CompagnyName]
                  ,[Adress_Street]
                  ,[State]
                    FROM[JunkEFCodeFrist1].[dbo].[Compagnies]";

            var con = ConnectionStringHelper.GetConnectionString();

            using (var conn = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
            {
                using (var data = conn.QueryMultiple(query, null))
                {
                    //var totalRecords = data.Read<int>().Single();
                    var records = data.Read<dynamic>();
                    var record = data.Read<dynamic>();
                    //var remaining = totalRecords - records.Count();
                }
            }
        }
    }

    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string Adress_Street { get; set; }
        public DateTime DateAdded { get; set; }
        public int PersonTypeId { get; set; }
        public DateTime DateModified { get; set; }
    }
}
