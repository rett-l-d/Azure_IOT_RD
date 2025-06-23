<%@ Page Title="RoboDK" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoboDK.aspx.cs" Inherits="AzureIOTWebApp.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
       .vnc-container {
            margin: 0;
            padding: 0;
            max-width: 100%; /* allow full width */
            width: 100vw; /* span the entire viewport width */
        }

        #vncFrame {
            width: 100%;
            height: 85vh; /* leave some room for navbar & footer */
            border: none;
        }

        h2 {
            margin-bottom: 10px;
        }
    </style>

    <div text-align: left;">
        <h2>Robot Simulation</h2>
        <p>Password: 123456</p>
    </div>

    <div class="vnc-container" text-align: left;">
        <%--VNC Server Needs to be TightVNC, Real VNC encryption does not work --%>

        <iframe
            id="vncFrame"
           <%-- src="../noVNC/vnc.html?host=1335-38-85-180-189.ngrok-free.app&encrypt=1&autoconnect=true&path="
            encript should be 1 if IIS server https if not it should 0--%>
          <%--    src="../noVNC/vnc.html?host=192.168.6.73&port=5902&encrypt=0&autoconnect=true&path="--%>
            src="../noVNC/vnc.html?host=732075b69e5c.ngrok.app&encrypt=1&resize=scale&autoconnect=true&path="
            frameborder="0">
        </iframe>
    </div>

</asp:Content>
