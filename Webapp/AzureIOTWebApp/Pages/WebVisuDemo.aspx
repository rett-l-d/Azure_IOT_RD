<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebVisuDemo.aspx.cs" Inherits="AzureIOTWebApp.Pages.WebVisuDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
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
    <form id="form1" runat="server">
       <%-- <div>
             <iframe src="https://6c6111867b71.ngrok.app/homedemo.htm"
        allowfullscreen="true"></iframe>
        </div>--%>
        <iframe src="https://lrettore.ddns.net:8080/homedemo.htm"
        allowfullscreen="true"></iframe>
    </form>
</body>
</html>
