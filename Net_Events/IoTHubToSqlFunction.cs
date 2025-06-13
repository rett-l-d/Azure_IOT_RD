using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class IoTHubToSqlFunction
{
    private readonly ILogger _logger;

    public IoTHubToSqlFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<IoTHubToSqlFunction>();
    }

    [Function("IoTHubToSqlFunction")]
    public async Task Run([EventHubTrigger("lrIOT", Connection = "IoTHubConnection")] string[] messages,
    FunctionContext context)
    {
         var logger = context.GetLogger("IoTHubToSqlFunction");
       // var json = Encoding.UTF8.GetString(messages);
      //  var telemetry = JsonConvert.DeserializeObject<TelemetryData>(json);

        var connString = Environment.GetEnvironmentVariable("SqlConnectionString");

       foreach (var message in messages)
        {
            var telemetry = JsonConvert.DeserializeObject<TelemetryData>(message);

            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();
            if (telemetry != null)
            {
                DateTime dt = DateTime.ParseExact(telemetry.Timestamp?? "", "yyyy-MM-dd-HH:mm:ss", CultureInfo.InvariantCulture);
                // Convert to UTC (if needed)
                DateTime dtUtc = dt.ToUniversalTime();
                // Format to ISO8601 (with 'Z' for UTC)
                string isoTimestamp = dtUtc.ToString("yyyy-MM-ddTHH:mm:ssZ");
                try
                {
                    var cmd = new SqlCommand("INSERT INTO TelemetryData (ProdCount, DeviceId, Temperature, Humidity, Timestamp) VALUES (@ProdCount, @DeviceId, @Temp, @Humid, @Time)", conn);
                    cmd.Parameters.AddWithValue("@ProdCount", telemetry.ProdCount);
                    cmd.Parameters.AddWithValue("@DeviceId", telemetry.DeviceId);
                    cmd.Parameters.AddWithValue("@Temp", telemetry.Temperature);
                    cmd.Parameters.AddWithValue("@Humid", telemetry.Humidity);
                    cmd.Parameters.AddWithValue("@Time", isoTimestamp);
                    await cmd.ExecuteNonQueryAsync();
                    logger.LogInformation($"Inserted data from {telemetry.DeviceId}");
                    logger.LogInformation($"Inserted data from {telemetry.ProdCount}");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Exception Message: " + ex.Message);
                    Console.WriteLine("SQL Error Code: " + ex.Number);
                    Console.WriteLine("SQL State: " + ex.State);
                    Console.WriteLine("Stack Trace: " + ex.StackTrace);
                }           
            }
        }
    }

    public class TelemetryData
    {
        public int MessageId { get; set; }
        public int ProdCount { get; set; }
        public string? DeviceId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public string? Timestamp { get; set; }
    }
}
