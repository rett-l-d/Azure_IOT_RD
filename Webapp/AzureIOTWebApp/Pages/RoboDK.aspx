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
        <p></p>
        <p>A robotic simulation is shown here to demonstrate and mimic a real-world
            production environment. As a pick-and-place robotic system cycles
            through a set of boxes, the production count is uploaded and stored
            in a cloud database after each completed cycle.
        </p>
    </div>

    <div class="vnc-container" text-align: left;">
        <%--VNC Server Needs to be TightVNC, Real VNC encryption does not work --%>

        <iframe
            id="vncFrame"
           <%-- src="../noVNC/vnc.html?host=1335-38-85-180-189.ngrok-free.app&encrypt=1&autoconnect=true&path="
            encript should be 1 if IIS server https if not it should 0--%>
          <%--    src="../noVNC/vnc.html?host=192.168.6.73&port=5902&encrypt=0&autoconnect=true&path="--%>
            <%--src="../noVNC/vnc.html?host=7ce05cc8bf6d.ngrok.app&encrypt=1&resize=scale&autoconnect=true&path="
            frameborder="0">--%>
           <%--  src="../noVNC/vnc.html?host=lrettore.ddns.net&port=6080&encrypt=0&resize=scale&autoconnect=true&path="
            frameborder="0">--%>
              src="../noVNC/vnc.html?host=lrettore.myddns.me&port=6080&encrypt=0&resize=scale&autoconnect=true&path="
            frameborder="0">
        </iframe>
    </div>

</asp:Content>
