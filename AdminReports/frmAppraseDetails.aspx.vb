Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports GenpactMSCLSE360
Imports System.Runtime.InteropServices.Marshal
Imports System.Diagnostics
Partial Class AdminReports_frmAppraseDetails
    Inherits System.Web.UI.Page
    Dim arrPara(0, 1) As String
    Dim objComm As New DAL
    Dim dr As SqlDataReader
    Dim objCon As SqlConnection
    Dim objCom As SqlCommand


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ID = "frmAppraseDetailsReport"

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '  Try
        ReDim arrPara(0, 1)
        Dim strTable1 As String = ""
        strTable1 = strTable1 & "<table class='table table-bordered table-sm tbl'>"
        strTable1 = strTable1 & "<tr>"
        strTable1 = strTable1 & "<td>"
        strTable1 = strTable1 & "Name"
        strTable1 = strTable1 & "</td>"
        strTable1 = strTable1 & "</tr>"
        Dim intCycleApsrID As Integer
        If Not Request.QueryString("CycleApsrID") Is Nothing Then
            intCycleApsrID = Request.QueryString("CycleApsrID")
            arrPara(0, 0) = intCycleApsrID
            arrPara(0, 1) = 0


            'Dim dr As SqlDataReader
            'dr = objComm.RunSP("SpGetApSeDetails", arrPara, 0)
            objCon = New SqlConnection
            objCom = New SqlCommand
            dr = objADO.RunSP("SpGetApSeDetails", arrPara, 0, objCon, objCom)

            While (dr.Read)
                If Not IsDBNull(dr.Item("Status")) Then
                    If dr.Item("Status") = 0 Then
                        strTable1 = strTable1 & "<tr class='clsAppRsp0'>"
                        strTable1 = strTable1 & "<td valign='top'>"
                        strTable1 = strTable1 & dr.Item("Name")
                        strTable1 = strTable1 & "</td>"
                        strTable1 = strTable1 & "</tr>"
                    End If
                    If dr.Item("Status") = 1 Then
                        strTable1 = strTable1 & "<tr class='clsAppRsp1'>"
                        strTable1 = strTable1 & "<td valign='top'>"
                        strTable1 = strTable1 & dr.Item("Name")
                        strTable1 = strTable1 & "</td>"
                        strTable1 = strTable1 & "</tr>"
                    End If
                    If dr.Item("Status") = 2 Then
                        strTable1 = strTable1 & "<tr class='clsAppRsp2'>"
                        strTable1 = strTable1 & "<td valign='top'>"
                        strTable1 = strTable1 & dr.Item("Name")
                        strTable1 = strTable1 & "</td>"
                        strTable1 = strTable1 & "</tr>"
                    End If
                End If
            End While
            strTable1 = strTable1 & "</table>"
            dvAppraseDetails.InnerHtml = strTable1

        End If
        '    Catch ex As Exception
        '      Response.Write("Following errors are occured:" & ex.Message)
        '   End Try
        objADO.CloseConnection(objCon, objCom, dr)

    End Sub

End Class
