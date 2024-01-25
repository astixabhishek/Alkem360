<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAppraseDetails.aspx.vb" Inherits="AdminReports_frmAppraseDetails" %>

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

        table.tbl,
        table.tbl-status {
            width: 90%;
            margin: 20px auto;
        }

            table.tbl > tbody > tr:first-child > td {
                background: #eee;
                border-bottom: 2px solid #0576BD;
                font-weight: bold;
                color: #0576BD;
            }

        .pg-title {
            background: #006B9F;
            color: #FFF;
            padding: 10px 6pt;
            font-weight: bold;
        }

        .clsAppRsp0 {
            background: #FFA6A6; /*#c9302c*/
        }

        .clsAppRsp1 {
            background: #FFDF71; /*#ec971f*/
        }

        .clsAppRsp2 {
            background: #6CFF6C; /*#449d44*/
        }
    </style>
    <script type="text/javascript">
        function fnClose() {
            window.close();
        }
    </script>
</head>
<body>
    <form method="post" runat="server">

        <div class="pg-title">FeedBack Status</div>

        <div id="dvAppraseDetails" runat="server"></div>

        <table class="tbl-status" cellspacing="5" cellpadding="5">
            <tr>
                <td class="clsAppRsp0" width="40" bgcolor="#FFABAB" height="1"></td>
                <td align="left" height="1">Not Started</td>
            </tr>
            <tr>
                <td class="clsAppRsp1" width="40" bgcolor="#FFFF88" height="1"></td>
                <td align="left" height="1">Started but not completed</td>
            </tr>
            <tr>
                <td class="clsAppRsp2" width="40" bgcolor="#7BFB92" height="1"></td>
                <td align="left" height="1">Completed</td>
            </tr>
        </table>
        <div class="text-center">
            <%-- <a class="button" onclick="fnClose()">Close</a>--%>
            <input id="Button1" type="button" value="Close" onclick="fnClose()" class="btn btn-primary" />
        </div>
    </form>
</body>
</html>
