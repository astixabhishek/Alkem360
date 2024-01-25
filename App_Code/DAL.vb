Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Web.Mail


Namespace GenpactMSCLSE360

Public Class DAL
    Public arrPara(0, 1) As String
    Public conn As New SqlConnection
    Public IPForMail As String
    Public Shared flagMsg As Integer
    Public Function OpenConnection()
        'conn = New SqlConnection("Persist Security Info=False;User ID=sa;PASSWord=AK6105;Initial Catalog=EYExSearch;Data Source=astixsun")
        'conn = New SqlConnection("Persist Security Info=False;User ID=eyman;PASSWord=eyindia;Initial Catalog=EYExSearch;Data Source=EYDEL")
        conn = New SqlConnection(System.Configuration.ConfigurationSettings.AppSettings("strConn"))

        If conn.State = 0 Then
            conn.Open()
        End If
    End Function
    Public Function MailSender(ByVal toWho As String, ByVal SendersName As String, ByVal Subject As String, ByVal Body As String, ByVal IPForMail As String) As Boolean
        Dim objMail As MailMessage
        Dim objMailAttach As MailAttachment
        'Create the Mail Attachment
        Try
            objMail = New MailMessage
            objMail.From = "claimstrack@astixsolutions.com"
            objMail.To = toWho
            objMail.Subject = Subject
            objMail.BodyFormat = MailFormat.Html
            objMail.Body = Body
            SmtpMail.SmtpServer = IPForMail
            SmtpMail.Send(objMail)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ReplaceQuotes(ByVal strtxt As String) As String
        Return Replace(strtxt, "'", "''")
    End Function


    Public Function ReplaceQuotes1(ByVal strtxt As String) As String
        Return Replace(strtxt, "'", "`")
    End Function

    Public Function MakeDate(ByVal DateText As Date) As String
        Return Format(DateText, "dd-MMM-yyyy")
    End Function

    Public Function fnSplitAttr(ByVal strAttr As String, ByVal attindex As Int16) As String
        Dim splitarr() As String
        splitarr = strAttr.Split("|")
        Return splitarr(attindex)
    End Function

    Public Sub FillCtrl(ByRef ControlName As Object, ByVal StoreProcName As String, ByVal IDFieldName As String, ByVal DesFieldName As String, ByVal arrPara As Array)
        ControlName.DataSource = RunSP(StoreProcName, arrPara, 0)
        ControlName.DataValueField = IDFieldName
        ControlName.DataTextField = DesFieldName
        ControlName.DataBind()
    End Sub
    Public Function RunSP(ByVal SPName As String, ByVal arrPara As Array, ByVal RetType As Integer)

        ' 0 - Number,   1 - string
        ' RetType=0 for return reader , 1 for return nothing.
        Dim ConnSP As New SqlConnection
        Dim Comm As New SqlCommand
        Dim strPar As String
        Comm.CommandTimeout = 6000
        Dim i As Integer
        If arrPara(0, 0) <> "Noparam" Then
            For i = 0 To UBound(arrPara)
                Select Case arrPara(i, 1)
                    Case 0
                        If Len(Trim(strPar)) = 0 Then
                            strPar = arrPara(i, 0)
                        Else
                            strPar = strPar & "," & arrPara(i, 0)
                        End If
                    Case 1
                        If Len(Trim(strPar)) = 0 Then
                            strPar = "'" & arrPara(i, 0) & "'"
                        Else
                            strPar = strPar & "," & "'" & arrPara(i, 0) & "'"
                        End If
                End Select
            Next
            strPar = SPName & " " & strPar
        Else
            strPar = SPName
        End If
        If ConnSP.State = ConnectionState.Open Then ConnSP.Close()
        ConnSP.ConnectionString = System.Configuration.ConfigurationSettings.AppSettings("strConn")
        ConnSP.Open()
        Comm.Connection = ConnSP
        Comm.CommandText = strPar
        If RetType = 0 Then
            RunSP = Comm.ExecuteReader(CommandBehavior.CloseConnection)
        Else
            Comm.ExecuteNonQuery()
            ConnSP.Close()
        End If
        'Catch
        'End Try
    End Function

    'Added By Vikas (21/12/2004) to Return a DataSet
    Public Function RunSP(ByVal SPName As String, ByVal arrPara As Array)
        Dim ConnSP As New SqlConnection
        Dim strPar As String
        Dim i As Integer
        If arrPara(0, 0) <> "Noparam" Then
            For i = 0 To UBound(arrPara)
                Select Case arrPara(i, 1)
                    Case 0
                        If Len(Trim(strPar)) = 0 Then
                            strPar = arrPara(i, 0)
                        Else
                            strPar = strPar & "," & arrPara(i, 0)
                        End If
                    Case 1
                        If Len(Trim(strPar)) = 0 Then
                            strPar = "'" & arrPara(i, 0) & "'"
                        Else
                            strPar = strPar & "," & "'" & arrPara(i, 0) & "'"
                        End If
                End Select
            Next
            strPar = SPName & " " & strPar
        Else
            strPar = SPName
        End If
        If ConnSP.State = ConnectionState.Open Then ConnSP.Close()
        ConnSP.ConnectionString = System.Configuration.ConfigurationSettings.AppSettings("strConn")
        ConnSP.Open()
        Dim rsAdap As New SqlDataAdapter(strPar, ConnSP)
        Dim dsRslts As New DataSet
        rsAdap.Fill(dsRslts)
        ConnSP.Close()
        ConnSP = Nothing
        Return dsRslts
    End Function

    Public Function DownloadImageFromDatabaseToServer(ByVal Exid As Integer) As String
        Dim dreader As SqlDataReader
        Dim imagepath As String
        Dim strimagepath As String
        Dim imagename As String

        Dim virtualpath As String = ConfigurationSettings.AppSettings("virtualpath")
        Dim strvirtualpath As String = ConfigurationSettings.AppSettings("strvirtualpath")
        ReDim arrPara(0, 1)

        arrPara(0, 0) = Exid
        arrPara(0, 1) = 0
        dreader = RunSP("spGetImage", arrPara, 0)

        If dreader.Read Then
            Dim b(dreader.GetBytes(0, 0, Nothing, 0, Integer.MaxValue) - 1) As Byte
            dreader.GetBytes(0, 0, b, 0, b.Length)
            imagename = "Resume" & Exid & ".Doc"

            Dim fs As New System.IO.FileStream(virtualpath & imagename, IO.FileMode.Create, IO.FileAccess.Write)

            fs.Write(b, 0, b.Length)
            fs.Close()
            imagepath = virtualpath & imagename
            strimagepath = strvirtualpath & imagename

        End If
        Return strimagepath
    End Function

    Public Function AddNewTreeNode(ByVal NodeID As Integer, ByVal NodeType As Integer, ByVal HierId As Integer, ByVal NodeDesc As String, ByVal EmptitleID As Integer, ByVal EmpFName As String, ByVal EmpMName As String, ByVal JobPosID As Integer, ByVal PNodeId As Integer, ByVal PNodeType As Integer, ByVal VldFrom As String, ByVal VldTo As String, ByVal RepType As Integer, ByVal SecFlag As Integer, ByVal lblLstLvl As Integer, ByVal LoginIDUpd As Integer, ByVal Email As String, ByVal Address As String, ByVal CityID As Integer, ByVal PosDesg As String, ByVal PosNmbr As Integer, ByVal PosCompetencies As String, ByVal PosKSA As String, ByVal PosKRA As String, ByVal PosDeliverables As String, ByVal PosPrevCand As String, ByVal PosComments As String, ByVal BizTerms As String) As SqlDataReader
        'SecFlag = 2
        If VldFrom = "" Then
            VldFrom = CStr(Today)
        End If
        If VldTo = "" Then
            VldTo = "31-dec-9999"
        End If

        ReDim arrPara(27, 1)

        arrPara(0, 0) = NodeID
        arrPara(0, 1) = 0
        arrPara(1, 0) = NodeType
        arrPara(1, 1) = 0
        arrPara(2, 0) = HierId
        arrPara(2, 1) = 0
        arrPara(3, 0) = NodeDesc
        arrPara(3, 1) = 1
        arrPara(4, 0) = EmptitleID
        arrPara(4, 1) = 0
        arrPara(5, 0) = ReplaceQuotes(EmpFName)
        arrPara(5, 1) = 1
        arrPara(6, 0) = ReplaceQuotes(EmpMName)
        arrPara(6, 1) = 1
        arrPara(7, 0) = JobPosID
        arrPara(7, 1) = 0

        arrPara(8, 0) = PNodeId
        arrPara(8, 1) = 0
        arrPara(9, 0) = PNodeType
        arrPara(9, 1) = 0
        arrPara(10, 0) = VldFrom
        arrPara(10, 1) = 1
        arrPara(11, 0) = VldTo
        arrPara(11, 1) = 1
        arrPara(12, 0) = RepType
        arrPara(12, 1) = 0
        arrPara(13, 0) = SecFlag
        arrPara(13, 1) = 0
        arrPara(14, 0) = lblLstLvl
        arrPara(14, 1) = 0
        arrPara(15, 0) = LoginIDUpd
        arrPara(15, 1) = 0
        arrPara(16, 0) = ReplaceQuotes(Email)
        arrPara(16, 1) = 1

        arrPara(17, 0) = ReplaceQuotes(Address)
        arrPara(17, 1) = 1

        arrPara(18, 0) = CityID
        arrPara(18, 1) = 0

        arrPara(19, 0) = ReplaceQuotes(PosDesg)
        arrPara(19, 1) = 1

        arrPara(20, 0) = PosNmbr
        arrPara(20, 1) = 0

        arrPara(21, 0) = ReplaceQuotes(PosCompetencies)
        arrPara(21, 1) = 1

        arrPara(22, 0) = ReplaceQuotes(PosKSA)
        arrPara(22, 1) = 1

        arrPara(23, 0) = ReplaceQuotes(PosKRA)
        arrPara(23, 1) = 1

        arrPara(24, 0) = ReplaceQuotes(PosDeliverables)
        arrPara(24, 1) = 1

        arrPara(25, 0) = ReplaceQuotes(PosPrevCand)
        arrPara(25, 1) = 1

        arrPara(26, 0) = ReplaceQuotes(PosComments)
        arrPara(26, 1) = 1

        arrPara(27, 0) = ReplaceQuotes(BizTerms)
        arrPara(27, 1) = 1


        Return RunSP("spTreeAddNodeID", arrPara, 0)
    End Function

    Public Sub subcloseCon(ByRef Con As SqlConnection)
        Con.Close()
        Con.Dispose()
        Con = Nothing
    End Sub
    Public Function fnMake4TDTable(ByVal spName As String, ByVal ArrParameter As Array, ByVal strClassCol As String, ByVal strClassData As String, ByVal iBorder As Integer, ByVal iTableWidth As Integer, ByVal iLableColWidth As Integer, ByVal iDataColWidth As Integer, Optional ByVal iSepratorColWidth As Integer = 2, Optional ByVal strSeprator As String = ":", Optional ByVal BdrColor As String = "#cccccc", Optional ByVal cPadding As Integer = 0) As String
        Dim strHTML As String = "<TABLE bordercolor=" & BdrColor & " cellspacing=0 cellpadding=" & cPadding & "  border=" & iBorder & " width=" & iTableWidth & "%>"
        Dim strCol As String = ""
        '    Dim objCon As SqlConnection
        'objCon = fnOpenConnection()

        Dim objDr As SqlDataReader
        objDr = RunSP(spName, ArrParameter, 0)
        Dim iRow As Integer
        Dim iCol As Integer
        iRow = 1
        While (objDr.Read)
            strHTML &= "<TR>"
            For iCol = 0 To objDr.FieldCount - 1
                If (iCol Mod 2 = 0) Then
                    strHTML &= "<TD class=" & strClassCol & " width=" & iLableColWidth & "%>"
                    strCol = IIf(IsDBNull(objDr(iCol)), "&nbsp;", objDr(iCol))
                    strHTML &= strCol
                    strHTML &= "</TD>"

                    strHTML &= "<TD  align='center' class=" & strClassCol & " width=" & iSepratorColWidth & "% >"
                    If (strCol = "&nbsp;") Then
                        strCol = ""
                    Else
                        strCol = strSeprator
                    End If

                    strHTML &= IIf(IsDBNull(objDr(iCol)), "&nbsp;", strCol)
                    strHTML &= "</TD>"
                Else
                    strHTML &= "<TD class=" & strClassData & " width=" & iDataColWidth & "%>"
                    strCol = IIf(IsDBNull(objDr(iCol)), "&nbsp;", objDr(iCol))
                    strHTML &= strCol
                    strHTML &= "</TD>"
                End If


            Next
            strHTML &= "</TR>"
        End While
        strHTML &= "</TABLE>"
        objDr.Close()
        '    subcloseCon(objCon)

        Return strHTML
    End Function



End Class

End Namespace
