<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAppraiserStatusReport.aspx.vb" Inherits="AdminReports_frmAppraiserStatusReport" %>

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
    <form id="frmApraseUserReport" method="post" runat="server">
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

        <asp:LinkButton Visible="false" ID="LinkButton1" runat="server" CssClass="clsArl9Blue">Click here to Download the file</asp:LinkButton>
        <%--<a href="#" id="DownLoadReport" runat="server"></a>
        <div id="dvMain" style="visibility: visible" runat="server"></div>
        <div id="dvinc" runat="server" align="right">&nbsp;</div>--%>

        <asp:DataGrid ID="dg_DisplayReport" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-sm tbl" BorderStyle="None"
            CellPadding="0" GridLines="Vertical" AllowPaging="True" PageSize="40" AllowSorting="True">
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            <FooterStyle BackColor="#B4B4B4"></FooterStyle>
            <Columns>
                <asp:BoundColumn ItemStyle-Width="20%" DataField="ApsrDescr" HeaderText="Appraiser Name" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                <asp:BoundColumn ItemStyle-Width="20%" DataField="ApseTot" HeaderText="Planned Feedback" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                <asp:BoundColumn ItemStyle-Width="20%" DataField="ApseDone" HeaderText="Feedback Done" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                <asp:BoundColumn ItemStyle-Width="20%" DataField="Apsebalance" HeaderText="Feedback Remaining" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                <asp:TemplateColumn ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle></ItemStyle>
                    <HeaderTemplate>
                        Details
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lk_ViewDetails" runat="server" CommandName="cmd_ViewDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"CycleApsrId")%>'>View Details</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" ForeColor="Black" BackColor="#B4B4B4" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>


    </form>
</body>
</html>
