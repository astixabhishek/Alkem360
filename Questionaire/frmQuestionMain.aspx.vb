Imports System.Data.SqlClient
Imports System.Data
Partial Class Questionaire_frmQuestionMain
    Inherits System.Web.UI.Page

    Dim RspID As Integer
    Dim LevelID As Integer
    Dim Name As String
    Dim PGNmbr As Integer
    Dim MaxPGNmbr As Integer
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RspID = Request.QueryString("RspId")
        Name = Request.QueryString("Name")
        PGNmbr = Request.QueryString("PgNmbr")
        MaxPGNmbr = Request.QueryString("MaxPGNmbr")
        hdnMaxPGNmbr.Value = MaxPGNmbr + 1
        hdnRspID.Value = RspID
        hdnPGNmbr.Value = Request.QueryString("PgNmbr")
        hdnName.Value = Replace(Name, "_", " ")
        LevelID = Request.QueryString("LevelID")
        hdnLevelID.Value = LevelID
        Dim panelLogout As Panel
        panelLogout = DirectCast(Page.Master.FindControl("panelLogout"), Panel)
        panelLogout.Visible = False

    End Sub


End Class
