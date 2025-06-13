using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using AzureIOTWebApp.Properties;

using Newtonsoft.Json;

namespace AzureIOTWebApp
{
    public class DBTelemetryData
    {
        public string GetTelemetryData()
        {
            var storedProcedureName = "spGetAllData";
            string jsonResult;
            string connectionstr = Settings.Default.DBConnString;

            // establish connection to DB, define command to execute stored procedure
            using (SqlConnection conn = new SqlConnection(connectionstr))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
            {
                try
                {
                    // set type of command to stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    Console.WriteLine("successful connection");

                    DataSet ds = new DataSet();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds, "table");
                        // Access the DataTable
                        var table = ds.Tables["table"];


                        jsonResult = JsonConvert.SerializeObject(table);

                    }
                    cmd.Dispose();
                    //return serialized Json
                    return jsonResult;
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                    return null;
                }
            }
        }
    }
}