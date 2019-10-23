using Dapper;
using MediaPerf.SendPdf.Models.Models;
using MediaPerf.SendPdf.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPerf.SendPdf.Repository.Repository
{
    public class PDFRepository : IPDFRepository
    {

        private IConnectionStringHelper _connectionString;

        public PDFRepository(IConnectionStringHelper connectionString)
        {
            _connectionString = connectionString;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<IAuthor> GetAuthors()
        {
            var date = Convert.ToDateTime(DateTime.Now.Date.ToShortDateString());
            var authorList = new List<IAuthor>();

            authorList.Add(new Author("Mahesh Chand",
                35,
                "A Prorammer's Guide to ADO.NET",
                true,
                date));
            authorList.Add(new Author("Neel Beniwal",
                18,
                "Graphics Development with C#",
                false,
                date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
                35,
                "Graphics Programming with GDI+",
                true,
                date));
            authorList.Add(new Author("Raj Kumar",
                30,
                "Building Creative Systems",
                false,
                date));
            authorList.Add(new Author("Neel Beniwal",
               18,
               "Graphics Development with C#",
               false,
               date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
                35,
                "Graphics Programming with GDI+",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
               35,
               "A Prorammer's Guide to ADO.NET",
               true,
               date));
            authorList.Add(new Author("Neel Beniwal",
                18,
                "Graphics Development with C#",
                false,
                date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
                35,
                "Graphics Programming with GDI+",
                true,
                date));
            authorList.Add(new Author("Raj Kumar",
                30,
                "Building Creative Systems",
                false,
                date));
            authorList.Add(new Author("Neel Beniwal",
               18,
               "Graphics Development with C#",
               false,
               date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
                35,
                "Graphics Programming with GDI+",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
               35,
               "A Prorammer's Guide to ADO.NET",
               true,
               date));
            authorList.Add(new Author("Neel Beniwal",
                18,
                "Graphics Development with C#",
                false,
                date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
               35,
               "Graphics Programming with GDI+",
               true,
               date));
            authorList.Add(new Author("Mahesh Chand",
               35,
               "A Prorammer's Guide to ADO.NET",
               true,
               date));
            authorList.Add(new Author("Neel Beniwal",
                18,
                "Graphics Development with C#",
                false,
                date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
                35,
                "Graphics Programming with GDI+",
                true,
                date));
            authorList.Add(new Author("Raj Kumar",
                30,
                "Building Creative Systems",
                false,
                date));
            authorList.Add(new Author("Neel Beniwal",
               18,
               "Graphics Development with C#",
               false,
               date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
                35,
                "A Prorammer's Guide to ADO.NET",
                true,
                date));
            authorList.Add(new Author("Neel Beniwal",
                18,
                "Graphics Development with C#",
                false,
                date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
               35,
               "Graphics Programming with GDI+",
               true,
               date));
            authorList.Add(new Author("Mahesh Chand",
               35,
               "A Prorammer's Guide to ADO.NET",
               true,
               date));
            authorList.Add(new Author("Neel Beniwal",
                18,
                "Graphics Development with C#",
                false,
                date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));
            authorList.Add(new Author("Mahesh Chand",
                35,
                "Graphics Programming with GDI+",
                true,
                date));
            authorList.Add(new Author("Raj Kumar",
                30,
                "Building Creative Systems",
                false,
                date));
            authorList.Add(new Author("Neel Beniwal",
               18,
               "Graphics Development with C#",
               false,
               date));
            authorList.Add(new Author("Praveen Kumar",
                28,
                "Mastering WCF",
                true,
                date));


            return authorList;
        }

        //public static void GetSingleDataObject()
        //{
        //    //var params = new DynamicParameters();
        //    //params.Add("a", 11);
        //    //params.Add("r", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            
        //    var param = new DynamicParameters();
        //    param.Add("@personTypeId", 2);
        //    param.Add("@CompagnyId", 3);

        //    //var connection = new SqlConnection(ConnectionStringHelper.GetConnectionString());
                      
        //    //var grid = connection.QueryMultiple("GetSingleDataObject", param,
        //    //                                     commandType: CommandType.StoredProcedure);

        //    //connection.Close();

        //    //params.Get<int>("r").IsEqualTo(11);
        //}

        //public static void GetMultiplesDatas()
        //{
        //    string storedProcedure = "GetAllDataObjects";

        //    //using (var conn = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
        //    //{
        //    //    using (var data = conn.QueryMultiple(storedProcedure, null))
        //    //    {
        //    //        ////var totalRecords = data.Read<int>().Single();
        //    //        //var records = data.Read<dynamic>();
        //    //        //var record = data.Read<dynamic>();
        //    //        ////var remaining = totalRecords - records.Count();
        //    //    }
        //    //}
        //}

        //public static void GetData()
        //{
        //    string query = @"SELECT TOP 1000 [PersonId]
        //          ,[FirstName]
        //          ,[Adress_Street]
        //          ,[State]
        //          ,[DateAdded]
        //          ,[PersonTypeId]
        //          ,[DateModified]
        //      FROM[JunkEFCodeFrist1].[dbo].[People]";

        //    query += @"SELECT TOP 1000 [CompagnyId]
        //          ,[CompagnyName]
        //          ,[Adress_Street]
        //          ,[State]
        //            FROM[JunkEFCodeFrist1].[dbo].[Compagnies]";
                      
        //    //using (var conn = new SqlConnection(ConnectionStringHelper.GetConnectionString()))
        //    //{
        //    //    using (var data = conn.QueryMultiple(query, null))
        //    //    {
        //    //        //var totalRecords = data.Read<int>().Single();
        //    //        var records = data.Read<dynamic>();
        //    //        var record = data.Read<dynamic>();
        //    //        //var remaining = totalRecords - records.Count();
        //    //    }
        //    //}
        //}
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

   
    /*
     * 
    *public string Insertstudent(StudentStore objss)  
    {  
        var para = new DynamicParameters();  
  
        para.Add("@STUDENTName", objss.STUDENTName); // Normal Parameters  
        para.Add("@ROLLNO", objss.ROLLNO);  
        para.Add("@COURSE", objss.COURSE);  
        para.Add("@DeparmentID", "1");  
  
        para.Add("@Myout", dbType: DbType.Int32, direction: ParameterDirection.Output);   
        // Getting Out Parameter  
  
        para.Add("@Ret", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);   
        // Getting Return value  
  
        Con.Open(); // opening connection  
  
        Con.Execute("Usp_getallstudents", para, commandType: CommandType.StoredProcedure);   
        //Executing Command   
        // mapping this StoredProcedure with Database one.  
  
        int Valueout = para.Get<int>("@Myout"); //Getting Out Value  
  
        int Valuereturn = para.Get<int>("@Ret"); //Getting Out Return  
  
        Con.Close(); // Closing connection  
  
        return "Inserted";  
    }  
     * 
     * */
}
