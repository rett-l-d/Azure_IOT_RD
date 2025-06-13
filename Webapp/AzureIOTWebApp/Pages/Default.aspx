<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AzureIOTWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <div class="jumbotron text-center">
    <h1>IoT Dashboard</h1>
    <p class="lead">
        This demo app showcases the seamless integration of a robotic system, 
        an embedded industrial controller (CODESYS), 
        and cloud-based data storage with Azure SQL — delivering a powerful 
        end-to-end industrial IoT solution.
    </p>
</div>

<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-5 col-sm-12 mb-4">
            <h2>Information at Hand</h2>
            <p>
                Simulated production data in this demo is securely stored in Azure SQL database, 
                seamlessly retrieved, and visualized through interactive DataTables 
                grids and dynamic Grafana dashboards.
            </p>        
        </div>

        <div class="col-md-5 col-sm-12 mb-4">
            <h2>State-of-the-Art Integration</h2>
            <p>
                Harnessing the power of cloud storage, MQTT, industrial communication 
                protocols, and cutting-edge technologies like C#, Python, 
                and SQL, this solution seamlessly captures, stores, 
                and delivers data for real-time diagnostics and smarter, 
                faster decision-making.
            </p>
        </div>
    </div>
</div>



</asp:Content>
