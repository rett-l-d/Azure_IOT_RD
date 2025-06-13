<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Grafana.aspx.cs" Inherits="AzureIOTWebApp.Grafana" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Grafana Dashboard</h2>

    <button onclick="openGrafanaModal()">Open Dashboard Modal</button>

    <script type="text/javascript">
        function openGrafanaModal() {
            var url = 'https://lrettore.grafana.net/public-dashboards/a724adaf85f74d25b643c17afd2f41b7';
            var width = screen.availWidth * 0.9;
            var height = screen.availHeight * 0.9;
            var left = (screen.availWidth - width) / 2;
            var top = (screen.availHeight - height) / 2;
            window.open(url, 'GrafanaPopup',
                `width=${width},height=${height},left=${left},top=${top},resizable=yes,scrollbars=yes`);
        }
    </script>
</asp:Content>
