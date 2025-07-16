<%@ Page Title="WebVisu" Language="C#" MasterPageFile="~/Site.Mobile.Master" AutoEventWireup="true" CodeBehind="WebVisuPLCMobile.aspx.cs" Inherits="AzureIOTWebApp.WebVisuPLC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
         /* Remove container restrictions */
       .container.body-content {
            margin: 0;
            padding: 0;
            max-width: 100%;
            width: 100%;
        }

        /* Also hide the footer */
        footer, hr {
            display: none;
        }

        html, body, form {
            margin: 0;
            padding: 0;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }

        iframe {
            position: absolute;
            top: 50px; /* height of your navbar */
            left: 0;
            right: 0;
            bottom: 0;
            width: 100%;
            height: calc(100% - 50px);
            border: none;
        }
    </style>

   <%-- <iframe src="https://6c6111867b71.ngrok.app/homemobile.htm"
        allowfullscreen="true"></iframe>--%>
     <iframe src="http://lrettore.myddns.me:8080/homemobile.htm"
        allowfullscreen="true"></iframe>
</asp:Content>
