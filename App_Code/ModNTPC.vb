Imports System.Web.Security
Imports System.Data.SqlClient


Namespace GenpactMSCLSE360

Public Module ModNTPC
    Public objADO As New clsConnection.clsConnection
    Function getTicketData() As String
        Dim id As FormsIdentity
        Dim ticket As FormsAuthenticationTicket
        Dim userdata As String
        id = HttpContext.Current.User.Identity
        ticket = id.Ticket
        userdata = CStr(ticket.UserData)
        Return userdata
    End Function
End Module

End Namespace
