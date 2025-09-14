* MES Web App that integrates manufacturing systems and processess with a cloud based database
* It includes a Grafana dashboard module, a live robotic simulation view, DataTables and a Webased UI that interfaces at the machine level
* This Demo is for my portfolio to demonstrate my C#, SQL, JS, jQuery and Azure deployment skills
* There are four projects in this application:
 * An embeded application in an industrial controller, a RaspberryPi in this demo. This is the IoT Hub client
 * A .Net Core 8+ application running on Azure listening for events triggered by the IoT hub. It logs the events with the corresponding variable values in a SQL Server DB
 * A front-end webapp that displays the information in the database using the DataTables library, the embedded controller UI and a Grafana Dashboard.
 * A Python script running in the background communication through TCP sockets with the industrial controller and as the back-end for the robotic simulation.
* Link to see a live demo:
 [Iluminada](https://iluminada.azurewebsites.net/).
