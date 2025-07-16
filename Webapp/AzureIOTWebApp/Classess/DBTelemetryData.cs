using System;
using System.Data;
using System.Data.SqlClient;
using Polly;
using System.Threading.Tasks;
using System.Runtime.Caching;

using AzureIOTWebApp.Properties;

using Newtonsoft.Json;

namespace AzureIOTWebApp
{

    public class DBTelemetryData
    {
        public string GetOrSetJsonCache(bool overridecache, out bool datacached)
        {
            string key = "MyJsonData";
            ObjectCache cache = MemoryCache.Default;

            if (!overridecache)
            {
                if (cache.Contains(key))
                {
                    datacached = true;
                    return cache[key] as string; //retrieves the cached data
                }
            }

            // Generate and cache if not already stored
            string jsonResult = GetTelemetryData().Result;

            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(119) };
            cache.Set(key, jsonResult, policy);

            datacached = false;
            return jsonResult;
        }

        public async Task<string> GetTelemetryData()
        {
            var storedProcedureName = "spGetAllData";
            string jsonResult;
            string connectionstr = Settings.Default.DBConnString;

            //Define retry policy
            //Don't forget to add new IP addressess to azure firewall when testing in different places
            var retryPolicy = Policy
            .Handle<SqlException>()
            .WaitAndRetryAsync(
                retryCount: 2,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(3),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    // log error here
                });


            try
            {
                return await retryPolicy.ExecuteAsync(async () =>
                {
                    using (SqlConnection conn = new SqlConnection(connectionstr))
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();

                        DataSet ds = new DataSet();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds, "table");
                        }

                        var table = ds.Tables["table"];
                        jsonResult =  JsonConvert.SerializeObject(table);

                        return jsonResult;
                    }
                });
            }
            catch (Exception ex)
            {

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