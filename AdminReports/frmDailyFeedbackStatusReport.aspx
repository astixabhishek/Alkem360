<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDailyFeedbackStatusReport.aspx.vb" Inherits="AdminReports_frmDailyFeedbackStatusReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">        
        body {
            width: 100%;
            margin: 0px;
            padding: 0px;
            background-color: #f3f3f3;
        }

        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        :after,
        :before {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        table.tbl > tbody > tr:first-child > td {
            background: #eee;
            border-bottom: 2px solid #0576BD;
            font-weight: bold;
            color: #0576BD;
        }
        table.tbl > tbody > tr:first-child > td {
            background: #eee;
            border-bottom: 2px solid #0576BD;
            font-weight: bold;
            color: #0576BD;
        }
    </style>
</head>
<body>
   <form id="Form1" method="post" runat="server">
        <div style="display: none;">
            <asp:DropDownList ID="hdnCycleId" runat="server" AutoPostBack="True">
                <asp:ListItem Value=" -Select- "> -Select- </asp:ListItem>
                <asp:ListItem Value="1">Cycle1</asp:ListItem>
                <asp:ListItem Value="2">Cycle2</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id="divMain" runat="server"></div>       
    </form>
</body>
</html>
