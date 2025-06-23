using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using AzureIOTWebApp.Properties;

using Newtonsoft.Json;

namespace AzureIOTWebApp
{
    public class DBTelemetryData
    {
        public async Task<string> GetTelemetryData()
        {
            var storedProcedureName = "spGetAllData";
            string jsonResult;
            string connectionstr = Settings.Default.DBConnString;

            // establish connection to DB, define command to execute stored procedure
            using (SqlConnection conn = new SqlConnection(connectionstr))
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
            {
                for (int attempt = 0; attempt < 5; attempt++)
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
                    catch (SqlException ex)
                    {

                        Console.WriteLine(ex);
                        await Task.Delay(TimeSpan.FromSeconds(30));
                       // Console.WriteLine($"Attempt {attempt + 1} failed: {ex.Message}");

                        if (attempt < 4)
                        {
                            Console.WriteLine("Waiting 30 seconds before retry...");
                            await Task.Delay(TimeSpan.FromSeconds(3));
                        }
                        else
                        {
                            return null;
                            throw; // rethrow if final attempt fails
                        }
                    }

                }
                return null;
            }
        }

        public string GetTelemetryDataNonblocking()
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
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex);
                        return null;
                        
                    }
            }
        }
    }
}