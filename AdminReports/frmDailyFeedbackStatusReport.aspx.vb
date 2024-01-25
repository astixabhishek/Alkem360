Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports GenpactMSCLSE360
Partial Class AdminReports_frmDailyFeedbackStatusReport
    Inherits System.Web.UI.Page
    Dim arrPara(0, 1) As String
    Dim obj As New DAL

    Dim drdr As SqlDataReader
    Dim objCon As SqlConnection
    Dim objCom As SqlCommand
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack Then
            binddata(Session("CycleID"))
            ViewState("AssmntcycleId") = Session("CycleID")
        End If

    End Sub

    Private Sub hdnCycleId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hdnCycleId.SelectedIndexChanged
        If hdnCycleId.SelectedItem.Value = "1" Then
            ViewState("AssmntcycleId") = 1 'Session("CycleID")
        End If
        If hdnCycleId.SelectedItem.Value = "2" Then

            ViewState("AssmntcycleId") = 2 'Session("CycleID")
        End If
        binddata(ViewState("AssmntcycleId"))
    End Sub
    Function binddata(ByVal cycleid)
        Dim arrPara(0, 1) As Integer
        Dim drdr As SqlDataReader

        arrPara(0, 0) = cycleid
        arrPara(0, 1) = 0

        objCon = New SqlConnection
        objCom = New SqlCommand

        drdr = objADO.RunSP("spDailyRptStatus", arrPara, 0, objCon, objCom)
        'drdr = obj.RunSP("spDailyRptStatus", arrPara, 0)

        Dim tblMain As New Table
        With tblMain.Attributes
            '.Add("border", 1)
            '.Add("bordercolor", "#72ABC0")
            '.Add("bgcolor", "#B7D5DF")
            '.Add("align", "center")
            '.Add("width", "90%")
            '.Add("cellspacing", "0")
            '.Add("cellpadding", "0")
            .Add("class", "table table-bordered table-sm tbl")
        End With

        Dim tblMain_r As TableRow
        Dim tblMain_c As TableCell

        tblMain_r = New TableRow
        tblMain_c = New TableCell
        'tblMain_c.Attributes.Add("class", "thbg")
        tblMain_c.Text = "Appraisee"
        tblMain_c.Attributes.Add("align", "center")
        tblMain_r.Cells.Add(tblMain_c)

        tblMain_c = New TableCell
        'tblMain_c.Attributes.Add("class", "thbg")
        tblMain_c.Text = "Appraiser"
        tblMain_c.Attributes.Add("align", "center")
        tblMain_r.Cells.Add(tblMain_c)

        tblMain_c = New TableCell
        'tblMain_c.Attributes.Add("class", "thbg")
        tblMain_c.Text = "Start Date"
        tblMain_c.Attributes.Add("align", "center")
        tblMain_r.Cells.Add(tblMain_c)

        tblMain_c = New TableCell
        'tblMain_c.Attributes.Add("class", "thbg")
        tblMain_c.Text = "End Date"
        tblMain_c.Attributes.Add("align", "center")
        tblMain_r.Cells.Add(tblMain_c)

        tblMain_c = New TableCell
        'tblMain_c.Attributes.Add("class", "thbg")
        tblMain_c.Text = "Status"
        tblMain_c.Attributes.Add("align", "center")
        tblMain_r.Cells.Add(tblMain_c)

        tblMain.Rows.Add(tblMain_r)

        'objCon = New SqlConnection
        'objCom = New SqlCommand
        'drdr = objADO.RunSP("spDailyRptStatus", arrPara, 0, objCon, objCom)

        While drdr.Read
            tblMain_r = New TableRow
            tblMain_c = New TableCell
            tblMain_c.Attributes.Add("align", "Left")
            tblMain_c.Text = IIf(IsDBNull(drdr.Item("Appraisee")), "-", (drdr.Item("Appraisee")))
            tblMain_r.Cells.Add(tblMain_c)

            tblMain_c = New TableCell
            tblMain_c.Attributes.Add("align", "Left")
            tblMain_c.Text = IIf(IsDBNull(drdr.Item("Appraiser")), "-", (drdr.Item("Appraiser")))
            tblMain_r.Cells.Add(tblMain_c)

            tblMain_c = New TableCell
            tblMain_c.Attributes.Add("align", "center")
            If (IsDBNull(drdr.Item("DtStart"))) Then
                tblMain_c.Text = "-"
            Else
                tblMain_c.Text = obj.MakeDate(drdr.Item("DtStart"))
            End If
            tblMain_r.Cells.Add(tblMain_c)

            tblMain_c = New TableCell
            tblMain_c.Attributes.Add("align", "center")
            If (IsDBNull(drdr.Item("DtEnd"))) Then
                tblMain_c.Text = "-"
            Else
                tblMain_c.Text = obj.MakeDate(drdr.Item("DtEnd"))
            End If
            'IIf(IsDBNull(drdr.Item("DtEnd")), "-", obj.MakeDate(drdr.Item("DtEnd")))
            tblMain_r.Cells.Add(tblMain_c)

            tblMain_c = New TableCell
            tblMain_c.Attributes.Add("align", "center")
            tblMain_c.Text = IIf(IsDBNull(drdr.Item("Status")), "-", (drdr.Item("Status")))
            tblMain_r.Cells.Add(tblMain_c)

            tblMain.Rows.Add(tblMain_r)
        End While
        divMain.Controls.Add(tblMain)
        drdr.Close()

        '  objADO.CloseConnection(objCon, objCom, drdr)
    End Function
End Class
