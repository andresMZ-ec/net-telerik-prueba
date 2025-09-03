Imports System.Web
Imports System.Reflection
Imports System.Web.UI
Imports System.Linq
Partial Class DialogClaude
    Inherits UserControl

    ' Propiedades públicas configurables
    Public Property Titulo As String
        Get
            Return lblTitulo.Text
        End Get
        Set(value As String)
            lblTitulo.Text = value
        End Set
    End Property

    Public Property Descripcion As String
        Get
            Return lblDescripcion.Text
        End Get
        Set(value As String)
            lblDescripcion.Text = value
        End Set
    End Property

    ' Aquí se guardará el nombre del método y los parámetros
    Public Property MetodoConfirmar As String
    Public Property MetodoCancelar As String
    Public Property ParametrosConfirmar As Object()
    Public Property ParametrosCancelar As Object()

    ' Eventos
    Protected Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        EjecutarMetodo(MetodoConfirmar, ParametrosConfirmar)
        pnlDialog.Visible = False
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        EjecutarMetodo(MetodoCancelar, ParametrosCancelar)
        pnlDialog.Visible = False
    End Sub

    ' Método para ejecutar dinámicamente
    Private Sub EjecutarMetodo(nombreMetodo As String, parametros As Object())
        If Not String.IsNullOrEmpty(nombreMetodo) Then
            Dim pagina As Page = Me.Page
            Dim tipoPagina As Type = pagina.GetType()
            Dim metodo As MethodInfo = tipoPagina.GetMethod(nombreMetodo, BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)

            If metodo IsNot Nothing Then
                metodo.Invoke(pagina, parametros)
            End If
        End If
    End Sub

    ' Mostrar el dialogo
    Public Sub Show()
        pnlDialog.Visible = True
        'updDialog.Update()
    End Sub

End Class