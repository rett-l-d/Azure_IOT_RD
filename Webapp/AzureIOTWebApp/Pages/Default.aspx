<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AzureIOTWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <div class="jumbotron text-center" style="padding: 20px;">
    <h2>IoT Dashboard</h2>
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

     <!-- Image Section -->
    <div class="row justify-content-center mb-4" style="margin-left:100px">
        <div class="col-md-8 text-center">
            <img src="<%= ResolveUrl("~/Images/Main_Img.jpg") %>" alt="IoT System Technologies" class="img-fluid rounded shadow">
        </div>
    </div>

</div>



</asp:Content>
