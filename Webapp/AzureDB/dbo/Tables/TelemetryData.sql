CREATE TABLE [dbo].[TelemetryData] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [DeviceID]    NVARCHAR (255) NULL,
    [MessageID]   FLOAT (53)     NULL,
    [ProdCount]   FLOAT (53)     NULL,
    [Temperature] FLOAT (53)     NULL,
    [Humidity]    FLOAT (53)     NULL,
    [Timestamp]   DATETIME       NULL
);

