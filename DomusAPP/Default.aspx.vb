Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnReporte_Click(sender As Object, e As EventArgs)
        ReportePrueba.ReporteAnalisis(Server, Response)
    End Sub
End Class