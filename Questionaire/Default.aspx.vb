Imports System.Data.SqlClient
Imports System.Data
Partial Class Questionaire_Default
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim AssmntTypeID As Integer = Request.QueryString("AssmntTypeID")

        Dim CycleID As Integer = Request.QueryString("CycleID")
        Dim CycleApseApsrMapID As Integer = Request.QueryString("CycleApseApsrMapID")
        Dim RspId As Integer = Request.QueryString("RspId")
        Dim LevelID As Integer = Request.QueryString("LevelID")
        Dim Name As String = Request.QueryString("Name")
        Dim RlshpID As Integer = Request.QueryString("RlshpId")
        Dim strReturn As String = ""
        Dim strReturnValue As Integer
        If Not IsPostBack Then

            Dim Objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
            Dim objCom As New SqlCommand("[spRSPManage]", Objcon)
            Dim drdr As SqlDataReader
            objCom.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session("LoginId")
            objCom.Parameters.Add("@AssmntTypeID", SqlDbType.Int).Value = AssmntTypeID
            objCom.Parameters.Add("@CycleID", SqlDbType.Int).Value = CycleID
            objCom.Parameters.Add("@CycleApseApsrMapID", SqlDbType.Int).Value = CycleApseApsrMapID
            objCom.Parameters.Add("@RSPID", SqlDbType.Int).Value = RspId
            objCom.Parameters.Add("@LevelID", SqlDbType.Int).Value = LevelID


            objCom.CommandType = CommandType.StoredProcedure
            objCom.CommandTimeout = 0
            Dim ReturnRspID As Integer
            Dim PgNmbr As Integer
            Dim MaxPGNmbr As Integer

            Try
                Objcon.Open()
                drdr = objCom.ExecuteReader()
                drdr.Read()
                ReturnRspID = drdr.Item("RSPID")
                PgNmbr = drdr.Item("PGNmbr")
                MaxPGNmbr = drdr.Item("Max_PgNo")
                strReturnValue = 1
            Catch ex As Exception
                strReturnValue = 2
                strReturn = ex.Message

            Finally
                objCom.Dispose()
                Objcon.Close()
                Objcon.Dispose()
            End Try
            If (strReturnValue = 1) Then
                Response.Redirect("frmQuestionMain.aspx?RspId=" & ReturnRspID & "&PgNmbr=" & PgNmbr & "&MaxPGNmbr=" & MaxPGNmbr & "&Name=" & Name & "&LevelID=" & LevelID)
            Else
                ' Response.Redirect("frmError.aspx")
            End If
        End If

    End Sub
End Class
