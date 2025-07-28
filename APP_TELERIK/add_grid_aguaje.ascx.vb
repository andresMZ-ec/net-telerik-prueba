Public Class add_grid_aguaje
    Inherits System.Web.UI.UserControl

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarMeses()
        End If
    End Sub

    Private Sub CargarMeses()
        ' Podrías generar esto dinámicamente si solo quieres meses con datos
        Dim meses As New List(Of String)
        For i As Integer = 1 To 12
            meses.Add(New DateTime(2000, i, 1).ToString("MMMM yyyy")) ' Formato "Enero 2025"
        Next
        rpMeses.DataSource = meses
        rpMeses.DataBind()
    End Sub

    Protected Sub btnAddRow_Command(sender As Object, e As CommandEventArgs)

    End Sub
#End Region

End Class