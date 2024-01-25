Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Text.RegularExpressions
Imports System.Data

Partial Class Login
    Inherits System.Web.UI.Page


    Dim strLocalIP As String
    Dim drdr As SqlDataReader
    Dim objCon As SqlConnection
    Dim objCom As SqlCommand
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        btnLogin.Attributes.Add("onclick", "javascript:return fnValidate()")
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim drdr As SqlDataReader

        If (Request.ServerVariables("REMOTE_ADDR") = "") Then
            strLocalIP = "Unknown"  'not able to get the IP.
        Else
            strLocalIP = Request.ServerVariables("REMOTE_ADDR")
        End If

        Dim PwdStatus As Integer

        Dim NodeID As Integer
        Dim strReturn As String = ""

        Dim Objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
        Dim objCom As New SqlCommand("[spSecUserLogin]", Objcon)
        objCom.Parameters.Add("@UserName", SqlDbType.VarChar).Value = txtLoginID.Text
        objCom.Parameters.Add("@Password", SqlDbType.VarChar).Value = txtPassword.Text
        objCom.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = strLocalIP
        objCom.Parameters.Add("@SessionID", SqlDbType.VarChar).Value = Session.SessionID
        objCom.Parameters.Add("@BrowserVersion", SqlDbType.VarChar).Value = Request.Browser.Type
        objCom.Parameters.Add("@Resolution", SqlDbType.VarChar).Value = hdnResolution.Value
        objCom.CommandType = CommandType.StoredProcedure
        objCom.CommandTimeout = 0
        Try
            Objcon.Open()
            drdr = objCom.ExecuteReader()
            drdr.Read()
            If (drdr.Item("LoginID") = 0) Then

                dvMessage.InnerText = "Invalid Login-Id or Password. Try Again !!"
                txtPassword.Text = ""
            Else
                Session("RId") = drdr.Item("RoleID")
                Session("LoginId") = drdr.Item("LoginID")
                Session("FullName") = drdr.Item("FullName")
                If Not IsDBNull(drdr.Item("CycleID")) Then
                    Session("CycleID") = drdr.Item("CycleID")
                Else
                    Session("CycleID") = 1
                End If
                If Not IsDBNull(drdr.Item("AssmntTypeID")) Then
                    Session("AssmntTypeID") = drdr.Item("AssmntTypeID")
                Else
                    Session("AssmntTypeID") = 1
                End If
                If Not IsDBNull(drdr.Item("EndDate")) Then
                    Session("EndDate") = drdr.Item("EndDate")
                Else
                    Session("EndDate") = Format(DateTime.Now.Date, "dd-MM-yyyy")
                End If
                PwdStatus = drdr.Item("PwdStatus")
                NodeID = 1 'drdr.item("NodeID")
                Session("Flag") = 1

                If Session("RId") = 6 Then
                    Response.Redirect("Data/frmMain.aspx")
                Else
                    Response.Redirect("Data/Welcome.aspx?NodeID=" & NodeID)
                End If
            End If

        Catch ex As Exception
            strReturn = ex.Message
        Finally
            objCom.Dispose()
            Objcon.Close()
            Objcon.Dispose()
        End Try
        'Comment for Check Git Hub

    End Sub

    'Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
    '    txtLoginID.Text = ""
    '    txtPassword.Text = ""
    '    dvMessage.InnerText = ""
    'End Sub

End Class


