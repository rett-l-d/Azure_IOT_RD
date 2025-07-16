using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Devices;
using System.Data;

public class IoTHubToSqlFunction
{
    private readonly ILogger _logger;
    private const int MaxRetries = 5;
    private readonly ServiceClient serviceClient;

    public IoTHubToSqlFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<IoTHubToSqlFunction>();
        var iotHubServiceConn = Environment.GetEnvironmentVariable("IoTHubServiceConnection");
        serviceClient = ServiceClient.CreateFromConnectionString(iotHubServiceConn);
    }

    [Function("IoTHubToSqlFunction")]
    public async Task Run([EventHubTrigger("lrIOT", Connection = "IoTHubConnection")] string[] messages,
    FunctionContext context)
    {
         var logger = context.GetLogger("IoTHubToSqlFunction");
        
       // var json = Encoding.UTF8.GetString(messages);
        //  var telemetry = JsonConvert.DeserializeObject<TelemetryData>(json);

        var connString = Environment.GetEnvironmentVariable("SqlConnectionString");
        string targetDeviceId = "myDevice01";
        foreach (var message in messages)
        {
            var telemetry = JsonConvert.DeserializeObject<TelemetryData>(message);
            
           // using var conn = new SqlConnection(connString);
           // await conn.OpenAsync();
            if (telemetry != null)
            {
                DateTime dt = DateTime.ParseExact(telemetry.Timestamp?? "", "yyyy-MM-dd-HH:mm:ss", CultureInfo.InvariantCulture);
                // Convert to UTC (if needed)
                DateTime dtUtc = dt.ToUniversalTime();
                // Format to ISO8601 (with 'Z' for UTC)
                string isoTimestamp = dtUtc.ToString("yyyy-MM-ddTHH:mm:ssZ");
                for (int attempt = 0; attempt < MaxRetries; attempt++)
                {
                    try
                    {
                        using var conn = new SqlConnection(connString);
                        
                        await conn.OpenAsync(); // ?? catch errors opening the DB here
                        var cmd = new SqlCommand("spInsertTelemetryData", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProdCount", telemetry.ProdCount);
                        cmd.Parameters.AddWithValue("@DeviceId", telemetry.DeviceId);
                        cmd.Parameters.AddWithValue("@Temp", telemetry.Temperature);
                        cmd.Parameters.AddWithValue("@Humid", telemetry.Humidity);
                        cmd.Parameters.AddWithValue("@Time", isoTimestamp);
                        await cmd.ExecuteNonQueryAsync();

                        logger.LogInformation($"Inserted data from {telemetry.DeviceId}");
                        logger.LogInformation($"ProdCounter {telemetry.ProdCount}");


                        var c2dMessage = new Message(Encoding.UTF8.GetBytes("Data Synced at " + DateTime.Now.ToString()));
                        // c2dMessage.Properties.Add("command", "ping");

                        await serviceClient.SendAsync(targetDeviceId, c2dMessage);
                        _logger.LogInformation($"Sent C2D message to device '{targetDeviceId}'" + " " + c2dMessage);

                        break; // Success

                    }
                    catch (SqlException ex)
                    {
                        _logger.LogInformation($"Retrying in 30s...{ex.Message}");

                        _logger.LogInformation($"SQL connection failed. Retrying in 30 seconds...");

                        var c2dMessage = new Message(Encoding.UTF8.GetBytes("Sql Excpection Failed to Sync at " + DateTime.Now.ToString()));
                        // c2dMessage.Properties.Add("command", "ping");

                        await serviceClient.SendAsync(targetDeviceId, c2dMessage);
                        _logger.LogInformation($"Sent C2D message to device '{targetDeviceId}'" + " " + c2dMessage);
                        await Task.Delay(30000);
                    }
                    catch (Exception ex)
                    {

                        _logger.LogInformation($"Retrying in 30s...{ex.Message}");

                        _logger.LogInformation($"Caught general Exception: {ex.Message}");
                        var c2dMessage = new Message(Encoding.UTF8.GetBytes("General Exception Failed to Sync at " + DateTime.Now.ToString()));

                        await serviceClient.SendAsync(targetDeviceId, c2dMessage);
                        _logger.LogInformation($"Sent C2D message to device '{targetDeviceId}'" + " " + c2dMessage);
                        await Task.Delay(30000);
                    }
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
