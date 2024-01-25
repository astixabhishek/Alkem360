<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAppraiseeStatusReport.aspx.vb" Inherits="AdminReports_frmAppraiseeStatusReport" %>

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
   <form id="frmAppraseDisplay" method="post" runat="server">
        <table class="table table-bordered table-sm">
            <tr>
                <td>View By</td>
                <td>:</td>
                <td><asp:DropDownList ID="hdnCycleId" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList></td>

                <td>Total No. Of Appraisals</td>
                <td>:</td>
                <td id="tdTotal" runat="server"></td>

                <td>Completed</td>
                <td>:</td>
                <td id="TdCompleted" runat="server"></td>

                <td>Remaining</td>
                <td>:</td>
                <td id="tdRemaining" runat="server"></td>
            </tr>
        </table>

        <asp:DataGrid ID="dg_DisplayReport" runat="server" PageSize="40" AllowPaging="True" BorderStyle="None"
            GridLines="Vertical" CellPadding="0" CellSpacing="0" AutoGenerateColumns="False" CssClass="table table-bordered table-sm tbl">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            <FooterStyle BackColor="#B4B4B4"></FooterStyle>
            <Columns>
                <asp:BoundColumn ItemStyle-Width="20%" DataField="ApseDescr" HeaderText="Name of Appraise" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                <asp:BoundColumn ItemStyle-Width="20%" DataField="ApsrTot" HeaderText="Planned Feedback" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                <asp:BoundColumn ItemStyle-Width="20%" DataField="ApsrDone" HeaderText="Feedback Done" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                <asp:BoundColumn ItemStyle-Width="20%" DataField="Apsrbalance" HeaderText="Feedback Remaining" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                <asp:TemplateColumn ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        Detail
                    </HeaderTemplate>

                    <ItemTemplate>
                        <asp:LinkButton ID="lk_ViewDetails" runat="server" CommandName="cmd_ViewDetails" CommandArgument='<%# (DataBinder.Eval(Container.DataItem,"NodeId"))  & "$" & (DataBinder.Eval(Container.DataItem,"NodeType"))%>'>View Details </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" BackColor="#B4B4B4" ForeColor="Black" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>

        <div id="dvinc" align="right" runat="server">&nbsp;</div>
       
    </form>
</body>
</html>
