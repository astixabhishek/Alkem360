Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services

Partial Class _frmMain
    Inherits System.Web.UI.Page
    Public RoleId As Integer
    Dim SessionCheck As Integer
    Dim drdr As SqlDataReader
    Dim objCon As SqlConnection
    Dim objCom As SqlCommand
    Dim arrPara(0, 1) As String
    Public NodeID As Integer
    Public LoginID As Integer
    Dim FullID As String
    Public AssmntIDAndCycleIDandCycApseAssmntTypeID As String = ""
    Public CycleID As Integer
    Public AssmntTypeID As Integer


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CycleID = Session("CycleID")

        AssmntTypeID = IIf(IsNothing(Session("AssmntTypeID")), 1, Session("AssmntTypeID"))
        NodeID = Request.QueryString("NodeID")
        hdnChkFlg.Value = IIf(IsNothing(Request.QueryString("ChkFlg")), 0, Request.QueryString("ChkFlg"))

        RoleId = Session("RId")

        SessionCheck = CType(Session("LoginId"), Integer)
        If (SessionCheck = Nothing) Then
            Dim strPath As String = ""
            strPath = System.Configuration.ConfigurationSettings.AppSettings("PhysicalPath") & "Login.aspx?LngID=2"
            Response.Write("<script>parent.parent.location.href='" & strPath & "'</script>")
            Return
        End If

        dvMain.InnerHtml = fnCreateHierarchy()
    End Sub

    Function fnCreateHierarchy() As String
        Dim strMenutable As New StringBuilder
        Dim strChild As New StringBuilder
        Dim flagchild As Boolean = False
        Dim Pkey As String = ""
        Dim PNodeid As Integer
        Dim PNodeidOld As Integer
        Dim count As Integer = 0
        Dim FullString As String
        Dim strReturnHtml As String = ""
        Dim Objcon As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConn"))
        Dim objCom As New SqlCommand("[SPMenuGetMenuHierarchy]", Objcon)
        objCom.Parameters.Add("@MenuNode", SqlDbType.Int).Value = 0
        objCom.Parameters.Add("@CycleID", SqlDbType.Int).Value = Session("CycleID")
        objCom.Parameters.Add("@LoginID", SqlDbType.Int).Value = Session("LoginId")
        objCom.Parameters.Add("@lngID", SqlDbType.Int).Value = 1

        objCom.CommandType = CommandType.StoredProcedure
        objCom.CommandTimeout = 0
        Dim strReturn As String = ""
        Try
            Objcon.Open()
            drdr = objCom.ExecuteReader()

            Dim LastChildNodeCreated As Integer = 0
            Dim ParentJustCreated As Integer = 0



            strMenutable.Append("<ul class='list-unstyled'>")
            With drdr
                While (.Read)
                    If .Item("IndexNumP") Is System.DBNull.Value Then

                    Else

                        If (.Item("PHierID") = 0) Then

                            If .Item("LstLevel") = 10 Then
                                If ParentJustCreated = 2 Then
                                    strMenutable.Append("</ul></li>")
                                    ParentJustCreated = 0
                                End If
                                If ParentJustCreated = 4 Then
                                    strMenutable.Append("</ul></li></ul></li>")
                                End If
                                If ParentJustCreated = 3 Then
                                    strMenutable.Append("</ul></li></ul></li>")
                                End If
                                strMenutable.Append("<li class='active'><a href='#'><b>EY MULTI STAKEHOLDER FEEDBACK</b></a></li>")
                                strMenutable.Append("<li class='has-submenu'><a href='#' id='" & drdr.Item("HierID") & "^" & .Item("LstLevel") & "'>")
                                strMenutable.Append(drdr.Item("Descr"))
                                strMenutable.Append("</a>")
                                ParentJustCreated = 1
                                '  strMenutable.Append("</ul>") ' Main div
                            Else
                                FullID = drdr.Item("Pkey")
                                If FullID.Split("|")(0) = 40 Then
                                    FullString = FullID.Split("|")(5)

                                    For count = 0 To FullString.Split("~").Length - 2
                                        If AssmntIDAndCycleIDandCycApseAssmntTypeID = "" Then
                                            AssmntIDAndCycleIDandCycApseAssmntTypeID = FullString.Split("~")(count).Split("^")(0) & "$" & FullString.Split("~")(count).Split("^")(2) & "$" & FullString.Split("~")(count).Split("^")(3)
                                        Else
                                            AssmntIDAndCycleIDandCycApseAssmntTypeID &= "|" & FullString.Split("~")(count).Split("^")(0) & "$" & FullString.Split("~")(count).Split("^")(2) & "$" & FullString.Split("~")(count).Split("^")(3)
                                        End If

                                    Next
                                    AssmntIDAndCycleIDandCycApseAssmntTypeID = AssmntIDAndCycleIDandCycApseAssmntTypeID & "|"

                                End If
                                If ParentJustCreated = 1 Then
                                    strMenutable.Append("<ul class='list-unstyled'>")
                                    ParentJustCreated = 2
                                End If
                                strMenutable.Append("<li><a href='#' id='" & drdr.Item("HierID") & "^" & .Item("LstLevel") & "'  onclick=fnAction('" & drdr.Item("HierID") & "')>")
                                strMenutable.Append(drdr.Item("Descr"))
                                strMenutable.Append("</a></li>")
                                strMenutable.Append("</ul>")
                            End If
                        Else

                            Pkey = .Item("HierID") & "^" & .Item("LstLevel")
                            If .Item("LstLevel") = 10 Then
                                If ParentJustCreated = 2 Then
                                    ParentJustCreated = 0
                                End If
                                If ParentJustCreated = 1 Then
                                End If
                                strMenutable.Append("<li><a href='#' id='" & drdr.Item("PhierID") & "^" & .Item("LstLevel") & "'>")
                                strMenutable.Append(drdr.Item("Descr"))
                                strMenutable.Append("</a>")
                                ParentJustCreated = 4

                            Else
                                '''''' Start Sub Menu '''''''''
                                If ParentJustCreated = 4 Then
                                    strMenutable.Append("<ul class='list-unstyled'>")
                                End If
                                If ParentJustCreated = 3 Then
                                    strMenutable.Append("<ul class='list-unstyled'>")
                                End If
                                If ParentJustCreated = 1 Then  '' Has Child
                                    strMenutable.Append("<ul class='list-unstyled'>")
                                    ParentJustCreated = 2
                                End If

                                If ParentJustCreated = 4 Then
                                    If CType(.Item("IndexNumP"), String).LastIndexOf(".") = 3 Then

                                        strMenutable.Append("<li><a href='#' id='" & drdr.Item("HierID") & "^" & .Item("LstLevel") & "' onclick=fnAction('" & drdr.Item("HierID") & "')>")
                                    Else

                                        strMenutable.Append("<li><a href='#' id='" & drdr.Item("HierID") & "^" & .Item("LstLevel") & "' onclick=fnAction('" & drdr.Item("HierID") & "')>")
                                    End If
                                    strMenutable.Append(drdr.Item("Descr"))
                                    strMenutable.Append("</a></li>")

                                Else
                                    If CType(.Item("IndexNumP"), String).LastIndexOf(".") = 3 Then

                                        strMenutable.Append("<li><a href='#' id='" & drdr.Item("HierID") & "^" & .Item("LstLevel") & "' onclick=fnAction('" & drdr.Item("HierID") & "')>")
                                    Else

                                        strMenutable.Append("<li><a href='#' id='" & drdr.Item("HierID") & "^" & .Item("LstLevel") & "' onclick=fnAction('" & drdr.Item("HierID") & "')>")
                                    End If
                                    strMenutable.Append(drdr.Item("Descr"))
                                    strMenutable.Append("</a></li>")
                                End If
                                strMenutable.Append("</ul></li>")
                                If ParentJustCreated = 3 Then
                                End If
                                LastChildNodeCreated = 1
                            End If

                        End If

                    End If
                End While
                If ParentJustCreated = 4 Then
                    ParentJustCreated = 0
                End If
                If ParentJustCreated = 3 Then
                    ParentJustCreated = 0
                End If
                If ParentJustCreated = 2 Then
                    ParentJustCreated = 0
                End If

                strReturn = strMenutable.ToString()
            End With

        Catch ex As Exception
            strReturn = ex.Message
        Finally
            objCom.Dispose()
            Objcon.Close()
            Objcon.Dispose()
        End Try
        Return strReturn
    End Function

End Class
