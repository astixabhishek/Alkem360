
Partial Class _Welcome
    Inherits Page

    Protected Sub btnContiue_Click(sender As Object, e As EventArgs) Handles btnContiue.Click
        Response.Redirect("frmMain.aspx")
    End Sub
End Class