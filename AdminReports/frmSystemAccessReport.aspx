<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSystemAccessReport.aspx.vb" Inherits="AdminReports_frmSystemAccessReport_NEW" %>

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
    </style>
</head>
<body>
    <form id="form1" method="post" runat="server">
        <div id="tbldiv" runat="server">
        </div>
    </form>
</body>
</html>
