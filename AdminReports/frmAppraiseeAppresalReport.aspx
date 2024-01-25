<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAppraiseeAppresalReport.aspx.vb" Inherits="AdminReports_frmAppraiseeAppresalReport" %>

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

            table.tbl > tbody > tr > th {
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
    <form id="frmAppresal" method="post" runat="server">
        <div class="pg-title">FeedBack Status</div>
        <table class="table table-bordered table-sm tbl">
            <tr>
                <th>Supervisor</th>
                <th>Peer</th>
            </tr>
            <tr>
                <td valign="top" width="50%">
                    <div id="dvManager" runat="server"></div>
                </td>
                <td valign="top" width="50%">
                    <div id="dvpeer" runat="server"></div>
                </td>
            </tr>
            <tr>
                <th>Direct Report</th>
                <th>Self</th>
            </tr>
            <tr>
                <td valign="top" width="50%">
                    <div id="dvDirectReport" align="center" runat="server"></div>
                </td>
                <td valign="top" width="50%">
                    <div id="dvSelf" runat="server">&nbsp;</div>
                </td>
            </tr>
        </table>
        <table class="tbl-status">
            <tr>
                <td class="clsAppRsp0" width="40" bgcolor="#d9ecec" height="1"></td>
                <td align="left" height="1">Not Started
                </td>
            </tr>
            <tr>
                <td class="clsAppRsp1" width="40" bgcolor="#d5d5ff" height="1"></td>
                <td align="left" height="1">Started but not completed
                </td>
            </tr>
            <tr>
                <td class="clsAppRsp2" width="40" bgcolor="#fff0ff" height="1"></td>
                <td align="left" height="1">Completed
                </td>
            </tr>
        </table>

        <div class="text-center mb-3">
            <%--<a class="button" onclick="fnClose()">Close</a>--%>
            <input id="Button1" type="button" value="Close" onclick="fnClose()" class="btn btn-primary" />
        </div>
    </form>
</body>
</html>
