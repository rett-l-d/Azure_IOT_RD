<%@ Page Title="DataTable" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataTable.aspx.cs" Inherits="AzureIOTWebApp.DataTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../DataTables/datatables.css" rel="stylesheet" />
    <link href="../DataTables/buttons.dataTables.css" rel="stylesheet" />
    <link href="../DataTables/select.dataTables.css" rel="stylesheet" />
    <link href="../DataTables/dataTables.dateTime.min.css" rel="stylesheet" />
    <link href="../DataTables/fixedHeader.dataTables.min.css" rel="stylesheet">
    <link href="../DataTables/stateRestore.dataTables.min.css" rel="stylesheet">

    <style type="text/css" class="init">
        table {
            table-layout: fixed;
        }
      /*  .container {
            margin-left: 10px;
        }*/
    </style>

    <script src="../DataTables/jquery-3.7.1.min.js"></script>
    <script src="../DataTables/datatables.js"></script>
    <script src="../DataTables/dataTables.buttons.js"></script>
    <script src="../DataTables/buttons.dataTables.js"></script>
    <script src="../DataTables/dataTables.select.js"></script>
    <script src="../DataTables/select.dataTables.js"></script>
    <script src="../DataTables/select.dataTables.js"></script>
    <script src="../DataTables/dataTables.dateTime.min.js"></script>
    <script src="../DataTables/moment.min.js"></script>
    <script src="../DataTables/datetime-moment.js"></script>
    <script src="../DataTables/dataTables.fixedHeader.min.js"></script>
    <script src="../DataTables/jszip.min.js"></script>
    <script src="../DataTables/pdfmake.min.js"></script>
    <script src="../DataTables/vfs_fonts.js"></script>
    <script src="../DataTables/buttons.html5.min.js"></script>
    <script src="../DataTables/buttons.print.min.js"></script>

    <script type="text/javascript">

        var datacachedStr = '<%:datacachedStr%>';
        var localtimestr = '<%:localtimestr%>';

        $(document).ready(function () {
            var table = new DataTable('#mainGrid', {
                "lengthMenu": [10, 25, 50, 100, { label: 'All', value: -1 }],
                "pageLength": 10,
                "fixedHeader": {
                    footer: true
                },
                initComplete: function () {

                    var minDate, maxDate;


                    //Custom filtering function which will search data
                    //in column 2 between two values
                    DataTable.ext.search.push(function (settings, data, dataIndex) {
                        var min = minDate.val();
                        var max = maxDate.val();
                        var date = new Date(data[2]); //Date column date search

                        if (
                            (min === null && max === null) ||
                            (min === null && date <= max) ||
                            (min <= date && max === null) ||
                            (min <= date && date <= max)
                        ) {
                            return true;
                        }
                        return false;
                    });

                    //Create date inputs
                    minDate = new DateTime('#minDate', {
                        format: 'MMMM Do YYYY'
                    });
                    maxDate = new DateTime('#maxDate', {
                        format: 'MMMM Do YYYY'
                    });


                    //Refilter the table
                    $('#minDate, #maxDate').on('change', function () {
                        if ($("#minDate").val() == "") {

                            minDate.val(null);
                        }
                        if ($("#maxDate").val() == "") {

                            maxDate.val(null);

                        }
                        //Search and draw
                        table.search()
                        table.draw();
                    });

                    // Convert the string to a Date object
                   // var date = new Date(localtimestr);

                    var utc = new Date(localtimestr + ' UTC'); // or use 'Z'
                    // Convert to local time string
                    var localized = utc.toLocaleString();

                    document.getElementById('<%=CachedLabel.ClientID %>')
                        .textContent = datacachedStr + localized;
                },

                layout: {
                    topStart: {
                        buttons: ['copy', 'csv', 'excel', 'pdf', 'print', 'pageLength']
                    }
                },


                //"ajax": {
                //    url: 'DataTable.aspx/BindTelemetryData',
                //    dataSrc: function (response) {
                //        var tr = response;
                //        console.log(typeof tr);
                //        return tr;
                //    },
                //    type: "POST",
                //    contentType: 'application/json; charset=utf-8'
                //},


                "ajax": {
                    url: 'DataTable.aspx/BindTelemetryData',
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    dataSrc: function (response) {
                        if (response === null || response === undefined) {
                            alert("The server is waking-up, after 20 seconds the data will appear, or refresh the page.");
                            return []; // Return empty array to prevent DataTables crash
                        }
                        else {
                            var tr = response;
                            console.log(typeof tr);
                            return tr;
                        }

                    }
                },

                //Set column definition initialisation properties
                "columnDefs": [
                    {
                        "targets": 1,
                        "createdCell": function (td, cellData, rowData, row, col) {
                            if (cellData <= "200") {
                                $(td).css('background-color', 'rgba(255, 0, 0, 0.15)') //red
                            }
                            if ((cellData > "200") && (cellData <= "500")){
                                $(td).css('background-color', 'rgba(255, 255, 0, 0.15)') //yellow
                            }
                            if (cellData > "500") {
                                $(td).css('background-color', 'rgba(0, 255, 0, 0.15)') //green
                            }
                            $(td).css('text-align', 'center')
                        }
                    }],

                "columns": [
                    { "data": 'DeviceID', 'width': '150px'},
                    { "data": 'ProdCount', 'width': '80px' },
                    {
                        data: 'Timestamp', 'width': '150px',
                        render: function (data, type, row) {
                            // Force parse as UTC by appending Z
                            const date = new Date(data + 'Z');
                            return date.toLocaleString();
                        }
                    }
                ],
            });

            setInterval(function () {
                table.ajax.reload(null, false); // user paging is not reset on reload
            }, 30000);
        });
    </script>

    <div class="container" style="margin-top: 20px;">
        <h2>Data Collection Details</h2>
        <p>Easily explore and interact with data using smart table features — search,
            sort, and filter instantly. Enjoy smooth navigation with automatic
            paging and a responsive layout that works on any device.
            Export data to Excel, PDF, or print with a single click.
            Any information logged by the automation system can be conveniently viewed here.
        </p>
        <asp:Label ID="CachedLabel" runat="server" Text=""></asp:Label>
        <asp:Button ID="ButtonRefresh" runat="server" Text="Refresh Data" Style="margin-left:100px;" Width="106px" OnClick="ButtonRefresh_Click"/>
      <table border="0" cellspacing="10" cellpadding="5">
        <tbody>
              <tr class="blank_row">
            <td bgcolor="#FFFFFF" colspan="3">&nbsp;</td>
        </tr>
            <tr>
            <td><label for="minDate" class="col-form-label">Minimum date:&nbsp</label>
            </td>
            <td><input type="text" id="minDate" name="minDate"></td>
        </tr>
            <tr class="blank_row">
            <td bgcolor="#FFFFFF" colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td><label for="maxDate" class="col-form-label">Maximum date:&nbsp</label>
            </td>
            <td><input type="text" id="maxDate" name="maxDate"></td>
            <td><div style="width: 80px"></div>
            </td>
        </tr>
    </tbody></table>

         <%--Grid declaration--%>
        <table id="mainGrid" class="display" style="width:40%">
            <thead>
                <tr>
                    <th>Device ID</th>
                    <th>Prod. Count</th>
                    <th>TimeStamp</th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <th>Device ID</th>
                    <th>Prod. Count</th>
                    <th>TimeStamp</th>
                </tr>
            </tfoot>
        </table>
    </div>
    </asp:Content>

