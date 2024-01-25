Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports GenpactMSCLSE360

Partial Class AdminReports_frmSystemAccessReport_NEW
    Inherits System.Web.UI.Page

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub


    Dim arrPara(0, 1) As String

    Dim dr As SqlDataReader
    Dim objCon As SqlConnection
    Dim objCom As SqlCommand
    Dim obj As New DAL
    Public assmntCycleId As Integer
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim dr As SqlDataReader
        dr = obj.RunSP("spUsrLogins", arrPara, 0)

        'objCon = New SqlConnection
        'objCom = New SqlCommand

        'dr = objADO.RunSP("spUsrLogins", arrPara, 0, objCon, objCom)

        'Dim strReturn As String = ""
        'Dim dr As SqlDataReader
        'Dim Objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
        'Dim objCom As New SqlCommand("[spUsrLogins]", Objcon)
        'objCom.CommandType = CommandType.StoredProcedure
        'objCom.CommandTimeout = 0
        'Try
        '    Objcon.Open()
        '    dr = objCom.ExecuteReader()
        '    dr.Read()
        'Catch ex As Exception
        '    strReturn = ex.Message
        'Finally
        '    objCom.Dispose()
        '    Objcon.Close()
        '    Objcon.Dispose()
        'End Try

        Dim dtTabel As New Table()
        With dtTabel.Attributes
            '.Add("border", "0")
            '.Add("bordercolor", "#2B7FA1")
            '.Add("bgcolor", "#EFE7F7")
            '.Add("align", "center")
            '.Add("width", "100%")
            '.Add("cellspacing", "0")
            '.Add("cellpadding", "0")
            .Add("class", "table table-bordered table-sm tbl") 'tblreport

        End With

        Dim tblRow As TableRow
        Dim tblcell As TableCell

        tblRow = New TableRow()
        tblcell = New TableCell()
        'tblcell.Attributes.Add("class", "thbg")
        tblcell.Text = "Date"
        tblcell.Attributes.Add("align", "center")
        tblRow.Cells.Add(tblcell)

        tblcell = New TableCell()
        'tblcell.Attributes.Add("class", "thbg")
        tblcell.Text = "Name"
        tblcell.Attributes.Add("class", "center")
        tblRow.Cells.Add(tblcell)

        tblcell = New TableCell()
        'tblcell.Attributes.Add("class", "thbg")
        tblcell.Text = "Login Time"
        tblcell.Attributes.Add("align", "center")
        tblRow.Cells.Add(tblcell)

        tblcell = New TableCell()
        'tblcell.Attributes.Add("class", "thbg")
        tblcell.Text = "Logout Time"
        tblcell.Attributes.Add("align", "center")
        tblRow.Cells.Add(tblcell)
        dtTabel.Rows.Add(tblRow)



        While dr.Read
            Dim vardate As Date
            tblRow = New TableRow()
            If (dr.Item("LoginDate") <> vardate) Then
                tblcell = New TableCell()
                'tblcell.Attributes.Add("class", "clsVrdn8a")
                tblcell.Attributes.Add("align", "Center")
                tblcell.Text = dr.Item("LoginDate")
                tblRow.Cells.Add(tblcell)
                'dtTabel.Rows.Add(tblRow)
                vardate = dr.Item("LoginDate")
            Else
                tblcell = New TableCell()
                tblcell.Text = ""
                tblRow.Cells.Add(tblcell)
            End If
            'tblRow = New TableRow()
            tblcell = New TableCell()
            tblcell.Attributes.Add("align", "Left")
            tblcell.Text = IIf(IsDBNull(dr.Item("Descr")), "-", dr.Item("Descr"))
            tblRow.Cells.Add(tblcell)
            tblcell = New TableCell()
            tblcell.Attributes.Add("align", "center")
            tblcell.Text = IIf(IsDBNull(dr.Item("LoginTime")), "-", dr.Item("LoginTime"))
            tblRow.Cells.Add(tblcell)
            tblcell = New TableCell()
            tblcell.Attributes.Add("align", "center")
            tblcell.Text = IIf(IsDBNull(dr.Item("Logouttime")), "-", dr.Item("Logouttime"))
            tblRow.Cells.Add(tblcell)
            dtTabel.Rows.Add(tblRow)
        End While
        tbldiv.Controls.Add(dtTabel)
        dr.Close()

        '  objADO.CloseConnection(objCon, objCom, dr)
    End Sub
End Class
