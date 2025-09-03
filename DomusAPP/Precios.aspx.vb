Public Class Precios
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'AjaxManager.AjaxSettings.AddAjaxSetting(ConfirmDialog1.btnConfirmar, RadGrid1)
        End If
    End Sub

    Protected Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        'ConfirmDialog1.Titulo = "Confirmar acción"
        'ConfirmDialog1.Descripcion = "¿Está seguro de que desea continuar?"

        '' Configurar qué métodos se ejecutan
        'ConfirmDialog1.MetodoConfirmar = "MetodoOk"
        'ConfirmDialog1.MetodoCancelar = "MetodoCancel"

        '' Parámetros dinámicos (pueden ser Integer, String, Boolean, List, etc.)
        'ConfirmDialog1.ParametrosConfirmar = New Object() {123, "Hola", True}
        'ConfirmDialog1.ParametrosCancelar = New Object() {New List(Of String) From {"A", "B"}}

        '' Mostrar modal
        'ConfirmDialog1.Show()
    End Sub

    ' Método que se ejecutará al confirmar
    Private Sub MetodoOk(id As Integer, mensaje As String, flag As Boolean)
        Response.Write($"OK - ID:{id}, MSG:{mensaje}, FLAG:{flag}")
    End Sub

    ' Método que se ejecutará al cancelar
    Private Sub MetodoCancel(lista As List(Of String))
        Response.Write("Cancelado. Lista: " & String.Join(",", lista))
    End Sub

End Class