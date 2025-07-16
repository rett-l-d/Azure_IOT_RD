CREATE PROCEDURE spInsertTelemetryData
    @ProdCount INT,
    @DeviceId NVARCHAR(100),
    @Temp FLOAT,
    @Humid FLOAT,
    @Time DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.TelemetryData (ProdCount, DeviceId, Temperature, Humidity, Timestamp)
    VALUES (@ProdCount, @DeviceId, @Temp, @Humid, @Time);
END