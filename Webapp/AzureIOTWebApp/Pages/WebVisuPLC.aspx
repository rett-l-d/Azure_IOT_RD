<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebVisuPLC.aspx.cs" Inherits="AzureIOTWebApp.WebVisuPLC" %>
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

    <iframe src="http://192.168.6.73:8080/webvisu.htm" 
        allowfullscreen="true"></iframe>
</asp:Content>
