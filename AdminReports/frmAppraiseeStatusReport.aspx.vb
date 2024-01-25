Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports GenpactMSCLSE360
Imports System.Runtime.InteropServices.Marshal
Imports System.Diagnostics

Partial Class AdminReports_frmAppraiseeStatusReport
    Inherits System.Web.UI.Page

    Dim objComm As New DAL
    Dim dsReport As New DataSet


    Public assmntCycleId As Integer
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
    Dim oExcel As Excel.Application
    Dim oBook As Excel.Workbook
    Dim oSheet As Excel.Worksheet

    Dim objCon As SqlClient.SqlConnection
    Dim objCom As SqlCommand
    Dim objDr As SqlClient.SqlDataReader
    Dim arrPara(0, 1) As String
    Dim ctr As Integer
    Dim File As System.IO.File

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            BindData(Session("CycleID"))
            ViewState("AssmntcycleId") = Session("CycleID")
            FillCombo(hdnCycleId)

        End If
    End Sub
    Sub FillCombo(ByRef Combo As DropDownList)
        Dim indx As Integer = 0
        ReDim arrPara(0, 1)
        arrPara(0, 0) = "Noparam"
        arrPara(0, 1) = 0

        Dim objCon1 As SqlConnection = New SqlConnection
        Dim objCom1 As SqlCommand = New SqlCommand
        Dim dr As SqlDataReader = objADO.RunSP("spFillCycle", arrPara, 0, objCon1, objCom1)

        Combo.Items.Add(New ListItem("- Select -", "0"))

        While dr.Read
            Combo.Items.Add(New ListItem(dr.Item("Descr"), dr.Item("CycleId")))

        End While
        objADO.CloseConnection(objCon1, objCom1, dr)
        'dr.Close()
    End Sub
    Private Sub BindData(ByVal CycleID)
        ReDim arrPara(0, 1)
        arrPara(0, 0) = CycleID
        arrPara(0, 1) = 0

        objCon = New SqlConnection
        objCom = New SqlCommand

        objCom.CommandTimeout = 0
        'dsReport = objComm.RunSP("SpGetApseApsrrep", arrPara)
        dsReport = objADO.RunSPDS("SpGetApseApsrrep", arrPara)

        If dsReport.Tables(0).Rows.Count > 0 Then
            dg_DisplayReport.DataSource = dsReport
            dg_DisplayReport.DataBind()
        End If

        Dim i As Integer
        If dsReport.Tables(1).Rows.Count > 0 Then
            For i = 0 To dsReport.Tables(1).Rows.Count - 1
                tdTotal.InnerText = IIf(IsDBNull(dsReport.Tables(1).Rows(i)(0)), 0, dsReport.Tables(1).Rows(i)(0))
                TdCompleted.InnerText = IIf(IsDBNull(dsReport.Tables(1).Rows(i)(1)), 0, dsReport.Tables(1).Rows(i)(1))
                tdRemaining.InnerText = IIf(IsDBNull(dsReport.Tables(1).Rows(i)(2)), 0, dsReport.Tables(1).Rows(i)(2))

            Next
        End If
        objADO.CloseConnection(objCon, objCom)
    End Sub
    Private Sub dg_DisplayReport_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_DisplayReport.ItemCommand
        If e.CommandName = "cmd_ViewDetails" Then
            Dim scriptString As String
            'strScript = "<script>window.open('Rename.aspx?FileName='+'" & sender.CommandArgument & "','Rename','toolbar=0,menubar=0,width=350,height=50,left=300,right=0,top=0,bottom=0,resizable=0')"
            'strScript += ";</" + "script>"

            scriptString = "<script>window.open('frmAppraiseeAppresalReport.aspx?NodeDescr=" & e.CommandArgument & "','AppraseReports','menubar=0,toolbar=0,resizable=1,left=0,top=0,width=1015,height=690,resizable=1,status=1,scrollbars=1')"  ' menubar=0,toolbar=0,resizable=1,left=0,top=0,width=1024,height=768
            scriptString += ";</" + "script>"
            RegisterStartupScript("Startup", scriptString)
            '  If (Not Me.IsStartupScriptRegistered("Startup")) Then
            '  Me.RegisterStartupScript("Startup", scriptString)
            'end If
        End If

    End Sub
    Private Sub dg_DisplayReport_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_DisplayReport.PageIndexChanged
        dg_DisplayReport.CurrentPageIndex = e.NewPageIndex
        BindData(ViewState("AssmntcycleId"))
    End Sub


    Private Sub LinkButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        oExcel = New Excel.Application
        oBook = oExcel.Workbooks.Open(System.Configuration.ConfigurationSettings.AppSettings("ExcelPath") & "\ByAppraiseeTemplate.xlt")
        oSheet = oBook.Worksheets(1)

        ReDim arrPara(0, 1)
        arrPara(0, 0) = ViewState("AssmntcycleId") '"Noparam"
        arrPara(0, 1) = 0

        objCon = New SqlClient.SqlConnection
        objCom = New SqlClient.SqlCommand
        ctr = 1
        objCom.CommandTimeout = 0
        objDr = objADO.RunSP("SpGetApseApsrrep", arrPara, 0, objCon, objCom)

        oSheet.Range("A" & ctr).Value = "Name of Respondent"
        oSheet.Range("B" & ctr).Value = "Planned Feedback"
        oSheet.Range("C" & ctr).Value = "Feedback Done"
        oSheet.Range("D" & ctr).Value = "Feedback Remaining"
        ctr = ctr + 1

        While objDr.Read
            oSheet.Range("A" & ctr).Value = objDr.Item("ApseDescr")
            oSheet.Range("B" & ctr).Value = objDr.Item("ApsrTot")
            oSheet.Range("C" & ctr).Value = objDr.Item("ApsrDone")
            oSheet.Range("D" & ctr).Value = objDr.Item("ApsrBalance")
            ctr = ctr + 1
        End While

        If File.Exists(System.Configuration.ConfigurationSettings.AppSettings("ExcelPath") & "\" & "ExcelReportsByAppraise" & ".xls") Then
            File.Delete(System.Configuration.ConfigurationSettings.AppSettings("ExcelPath") & "\" & "ExcelReportsByAppraise" & ".xls")
        End If
        oBook.SaveAs(System.Configuration.ConfigurationSettings.AppSettings("ExcelPath") & "\" & "ExcelReportsByAppraise" & ".xls")
        oBook.Close()
        oExcel.Quit()

        fnRetunPathForDownload()
        '  fnKillallExcelProcess()

        'Try
        '    Dim obj As Process
        '    For Each obj In Process.GetProcesses
        '        If obj.ProcessName = "EXCEL" Then
        '            obj.Kill()
        '            System.Threading.Thread.Sleep(1000)
        '        End If
        '    Next

        'Catch ex As Exception

        'End Try

    End Sub
    Function fnRetunPathForDownload()
        Dim path As String = ConfigurationSettings.AppSettings("ExcelPath") & "\ExcelReportsByAppraise.xls"
        'Response.Redirect(Path)
        'Return Path
        Response.Clear()
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ExcelReportsByAppraise.xls")
        Response.WriteFile(path)

    End Function
    Function fnKillallExcelProcess()
        Dim proc As System.Diagnostics.Process
        For Each proc In System.Diagnostics.Process.GetProcessesByName("EXCEL")
            proc.Kill()
        Next

    End Function

    Private Sub hdnCycleId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hdnCycleId.SelectedIndexChanged
        'If hdnCycleId.SelectedItem.Value = "1" Then
        '    ViewState("AssmntcycleId") = 1 ' Session("CycleID")
        'End If
        'If hdnCycleId.SelectedItem.Value = "2" Then
        '    'ViewState("AssmntcycleId") = 6
        '    ViewState("AssmntcycleId") = 2 'Session("CycleID")
        'End If
        ' BindData(ViewState("AssmntcycleId"))
        BindData(hdnCycleId.SelectedItem.Value)
    End Sub
End Class

