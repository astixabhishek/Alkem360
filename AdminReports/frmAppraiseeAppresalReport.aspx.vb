Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports GenpactMSCLSE360
Imports System.Runtime.InteropServices.Marshal
Imports System.Diagnostics
Partial Class AdminReports_frmAppraiseeAppresalReport
    Inherits System.Web.UI.Page
    Dim arrPara(0, 1) As String
    Dim dsAppresal As New DataSet
    Dim objComm As New DAL
    Public arr As Array
    Public intNodeType As Integer
    Public intNodeId As Integer

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        '      Try
        Dim i As Integer
        ReDim arrPara(1, 1)
        Dim strTable1 As String = ""
        Dim strTable2 As String = ""
        Dim strTable3 As String = ""
        Dim strTable4 As String = ""
        Dim strtable5 As String = ""
        strTable1 = strTable1 & "<table cellpadding=0 cellspacing='1'  border='0' width='100%' align='center'>"

        strTable2 = strTable2 & "<table cellpadding=0 cellspacing='1'  border='0' width='100%' align='center'>"

        strTable3 = strTable3 & "<table cellpadding=0 cellspacing='1'  border='0' width='100%' align='center'>"

        strTable4 = strTable4 & "<table cellpadding=0 cellspacing='1'  border='0' width='100%' align='center'>"

        strtable5 = strtable5 & "<table cellpadding=0 cellspacing='1'  border='0' width='100%' align='center'>"

        If Not Request.QueryString("NodeDescr") Is Nothing Then
            arr = Split(Request.QueryString("NodeDescr"), "$")
            intNodeId = CType(arr(0), Integer)
            intNodeType = CType(arr(1), Integer)
        End If

        arrPara(0, 0) = intNodeId
        arrPara(0, 1) = 0
        arrPara(1, 0) = intNodeType
        arrPara(1, 1) = 0

        '  dsAppresal = objComm.RunSP("SpGetAppraisarsDet", arrPara)
        dsAppresal = objADO.RunSPDS("SpGetAppraisarsDet", arrPara)

        If dsAppresal.Tables(0).Rows.Count > 0 Then

            For i = 0 To dsAppresal.Tables(0).Rows.Count - 1
                If dsAppresal.Tables(0).Rows(i)("Status") = 0 Then
                    strTable1 = strTable1 & "<tr class='clsAppRsp0'>"
                    strTable1 = strTable1 & "<td valign='top'>"
                    strTable1 = strTable1 & dsAppresal.Tables(0).Rows(i)("AprsrDescr")
                    strTable1 = strTable1 & "</td>"
                    strTable1 = strTable1 & "</tr>"
                End If
                If dsAppresal.Tables(0).Rows(i)("Status") = 1 Then
                    strTable1 = strTable1 & "<tr class='clsAppRsp1'>"
                    strTable1 = strTable1 & "<td  valign='top' >"
                    strTable1 = strTable1 & dsAppresal.Tables(0).Rows(i)("AprsrDescr")
                    strTable1 = strTable1 & "</td>"
                    strTable1 = strTable1 & "</tr>"
                End If
                If dsAppresal.Tables(0).Rows(i)("Status") = 2 Then
                    strTable1 = strTable1 & "<tr class='clsAppRsp2'>"
                    strTable1 = strTable1 & "<td  valign='top' >"
                    strTable1 = strTable1 & dsAppresal.Tables(0).Rows(i)("AprsrDescr")
                    strTable1 = strTable1 & "</td>"
                    strTable1 = strTable1 & "</tr>"
                End If
            Next
            strTable1 = strTable1 & "</table>"
            dvpeer.InnerHtml = strTable1
        Else
            strTable1 = strTable1 & "</table>"
            dvpeer.InnerHtml = strTable1
        End If
        If dsAppresal.Tables(2).Rows.Count > 0 Then
            For i = 0 To dsAppresal.Tables(2).Rows.Count - 1
                If dsAppresal.Tables(2).Rows(i)("Status") = 0 Then
                    strTable2 = strTable2 & "<tr class='clsAppRsp0'>"
                    strTable2 = strTable2 & "<td  valign='top' >"
                    strTable2 = strTable2 & dsAppresal.Tables(2).Rows(i)("AprsrDescr")
                    strTable2 = strTable2 & "</td>"
                    strTable2 = strTable2 & "</tr>"
                End If
                If dsAppresal.Tables(2).Rows(i)("Status") = 1 Then
                    strTable2 = strTable2 & "<tr class='clsAppRsp1'>"
                    strTable2 = strTable2 & "<td  valign='top' >"
                    strTable2 = strTable2 & dsAppresal.Tables(2).Rows(i)("AprsrDescr")
                    strTable2 = strTable2 & "</td>"
                    strTable2 = strTable2 & "</tr>"
                End If
                If dsAppresal.Tables(2).Rows(i)("Status") = 2 Then
                    strTable2 = strTable2 & "<tr class='clsAppRsp2'>"
                    strTable2 = strTable2 & "<td  valign='top' >"
                    strTable2 = strTable2 & dsAppresal.Tables(2).Rows(i)("AprsrDescr")
                    strTable2 = strTable2 & "</td>"
                    strTable2 = strTable2 & "</tr>"
                End If
            Next
            strTable2 = strTable2 & "</table>"
            dvManager.InnerHtml = strTable2
        Else
            strTable2 = strTable2 & "</table>"
            dvManager.InnerHtml = strTable2
        End If
        If dsAppresal.Tables(3).Rows.Count Then
            For i = 0 To dsAppresal.Tables(3).Rows.Count - 1
                If dsAppresal.Tables(3).Rows(i)("Status") = 0 Then
                    strTable3 = strTable3 & "<tr class='clsAppRsp0'>"
                    strTable3 = strTable3 & "<td  valign='top' >"
                    strTable3 = strTable3 & dsAppresal.Tables(3).Rows(i)("AprsrDescr")
                    strTable3 = strTable3 & "</td>"
                    strTable3 = strTable3 & "</tr>"
                End If
                If dsAppresal.Tables(3).Rows(i)("Status") = 1 Then
                    strTable3 = strTable3 & "<tr  class='clsAppRsp1'>"
                    strTable3 = strTable3 & "<td  valign='top' >"
                    strTable3 = strTable3 & dsAppresal.Tables(3).Rows(i)("AprsrDescr")
                    strTable3 = strTable3 & "</td>"
                    strTable3 = strTable3 & "</tr>"
                End If
                If dsAppresal.Tables(3).Rows(i)("Status") = 2 Then
                    strTable3 = strTable3 & "<tr  class='clsAppRsp2'>"
                    strTable3 = strTable3 & "<td  valign='top' >"
                    strTable3 = strTable3 & dsAppresal.Tables(3).Rows(i)("AprsrDescr")
                    strTable3 = strTable3 & "</td>"
                    strTable3 = strTable3 & "</tr>"
                End If
            Next
            strTable3 = strTable3 & "</table>"
            dvSelf.Attributes.Add("Align", "center")
            dvSelf.InnerHtml = strTable3
        Else
            strTable3 = strTable3 & "</table>"
            dvSelf.Attributes.Add("Align", "center")
            dvSelf.InnerHtml = strTable3
        End If
        ''If dsAppresal.Tables(4).Rows.Count > 0 Then
        ''    For i = 0 To dsAppresal.Tables(4).Rows.Count - 1
        ''        If dsAppresal.Tables(4).Rows(i)("Status") = 0 Then
        ''            strTable4 = strTable4 & "<tr  class='clsAppRsp0'>"
        ''            strTable4 = strTable4 & "<td  valign='top'>"
        ''            strTable4 = strTable4 & dsAppresal.Tables(4).Rows(i)("AprsrDescr")
        ''            strTable4 = strTable4 & "</td>"
        ''            strTable4 = strTable4 & "</tr>"
        ''        End If
        ''        If dsAppresal.Tables(4).Rows(i)("Status") = 1 Then
        ''            strTable4 = strTable4 & "<tr  class='clsAppRsp1'>"
        ''            strTable4 = strTable4 & "<td  valign='top'>"
        ''            strTable4 = strTable4 & dsAppresal.Tables(4).Rows(i)("AprsrDescr")
        ''            strTable4 = strTable4 & "</td>"
        ''            strTable4 = strTable4 & "</tr>"
        ''        End If
        ''        If dsAppresal.Tables(4).Rows(i)("Status") = 2 Then
        ''            strTable4 = strTable4 & "<tr  class='clsAppRsp2'>"
        ''            strTable4 = strTable4 & "<td  valign='top'>"
        ''            strTable4 = strTable4 & dsAppresal.Tables(4).Rows(i)("AprsrDescr")
        ''            strTable4 = strTable4 & "</td>"
        ''            strTable4 = strTable4 & "</tr>"
        ''        End If
        ''    Next
        ''    strTable4 = strTable4 & "</table>"
        ''    dvExternal.InnerHtml = strTable4
        ''Else
        ''    strTable4 = strTable4 & "</table>"
        ''    dvExternal.InnerHtml = strTable4
        ''End If
        If dsAppresal.Tables(1).Rows.Count > 0 Then
            For i = 0 To dsAppresal.Tables(1).Rows.Count - 1
                If dsAppresal.Tables(1).Rows(i)("Status") = 0 Then
                    strtable5 = strtable5 & "<tr  class='clsAppRsp0'>"
                    strtable5 = strtable5 & "<td  valign='top'>"
                    strtable5 = strtable5 & IIf(Not IsDBNull(dsAppresal.Tables(1).Rows(i)("AprsrDescr")), dsAppresal.Tables(1).Rows(i)("AprsrDescr"), "&nbsp;")
                    strtable5 = strtable5 & "</td>"
                    strtable5 = strtable5 & "</tr>"
                End If
                If dsAppresal.Tables(1).Rows(i)("Status") = 1 Then
                    strtable5 = strtable5 & "<tr  class='clsAppRsp1'>"
                    strtable5 = strtable5 & "<td  valign='top'>"
                    strtable5 = strtable5 & IIf(Not IsDBNull(dsAppresal.Tables(1).Rows(i)("AprsrDescr")), dsAppresal.Tables(1).Rows(i)("AprsrDescr"), "&nbsp;")
                    strtable5 = strtable5 & "</td>"
                    strtable5 = strtable5 & "</tr>"
                End If
                If dsAppresal.Tables(1).Rows(i)("Status") = 2 Then
                    strtable5 = strtable5 & "<tr  class='clsAppRsp2'>"
                    strtable5 = strtable5 & "<td  valign='top'>"
                    strtable5 = strtable5 & IIf(Not IsDBNull(dsAppresal.Tables(1).Rows(i)("AprsrDescr")), dsAppresal.Tables(1).Rows(i)("AprsrDescr"), "&nbsp;")
                    strtable5 = strtable5 & "</td>"
                    strtable5 = strtable5 & "</tr>"
                End If
            Next
            strtable5 = strtable5 & "</table>"
            dvDirectReport.InnerHtml = strtable5
        Else
            strtable5 = strtable5 & "</table>"
            dvDirectReport.InnerHtml = strtable5
        End If

        '     Catch ex As Exception
        '     Response.Write("Following errors are occured:" & ex.Message)
        '  End Try

    End Sub

End Class
