<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AzureIOTWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background-image: url('<%= ResolveUrl("~/Images/Iluminada_logo_tr2.jpg") %>');
            background-size: cover;
            background-repeat: no-repeat;
            background-attachment: fixed;
            background-position: center -25px;
            background-size: 100%;
            left: 0;
            background-color: rgba(105, 105, 105, 0.1);
        }


    </style>

   <div class="jumbotron text-center" style="padding: 20px; background-color: rgba(255, 255, 255, 0.7);">
    <h2>Iluminada IoT Portal</h2>
    <p class="lead">
        Iluminada is a world class webapp that seamless integrates manufacturing systems,
        edge devices,
        and cloud-based data storage with Microsoft Azure — delivering a powerful
        end-to-end industrial IoT solution.
    </p>
</div>

<div class="container">
   <div class="row justify-content-center no-gutters">
        <div class="col-md-5 col-sm-12 mb-4" style="background-color: rgba(255, 255, 255, 0.7); padding: 15px; border-radius: 8px; margin-left: 30px;">
            <h2>Information at Hand</h2>
            <p>
                Simulated production data, of a robotic system in this demo, is securely stored in Azure SQL database,
                seamlessly retrieved, and visualized through interactive dataTables
                grids and dynamic dashboards.
            </p>
        </div>

        <div class="col-md-5 col-sm-12 mb-4" style="background-color: rgba(255, 255, 255, 0.7); padding: 15px; border-radius: 8px; margin-left: 25px; width: 50%;">
            <h2>State-of-the-Art Integration</h2>
            <p>
                Harnessing the power of cloud storage, MQTT, industrial communication
                protocols, embedded controllers, and cutting-edge technologies such as
                interactive webfrontends, C#, Python,
                and SQL, this solution seamlessly captures, stores,
                and delivers data for real-time diagnostics and smarter,
                faster decision-making.
            </p>
        </div>
    </div>

     <!-- Image Section -->
    <%--<div class="row justify-content-center mb-4" style="margin-left:100px">
        <div class="col-md-8 text-center">
            <img src="<%= ResolveUrl("~/Images/Iluminada_logo.jpeg") %>" alt="Iluminada IoT System Technologies" class="img-fluid rounded shadow">
        </div>
    </div>--%>

</div>



</asp:Content>
