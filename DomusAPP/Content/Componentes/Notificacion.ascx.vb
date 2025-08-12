Imports DomusAPP.iconos_svg

Public Class Notificacion
    Inherits System.Web.UI.UserControl

    Public Event Confirmar_Respuesta As EventHandler

    Public Property ResultadoConfirmacin As Boolean?
        Get
            Return CType(ViewState("ConfirmacionState"), Boolean?)
        End Get
        Set(value As Boolean?)
            ViewState("ConfirmacionState") = value
        End Set
    End Property


    Public Sub Mostrar(ByVal titulo As String, ByVal mensaje As String)
        RadNotificacion.Title = titulo
        lblNotificacion.Text = mensaje
        RadNotificacion.Show()
    End Sub

    Public Sub Confirmar(ByVal titulo As String, ByVal description As String, Optional ByVal icono As TipoIcono = TipoIcono.X)
        lblTitulo.Text = titulo
        lblMensaje.Text = description
        ResultadoConfirmacin = Nothing
        rWindowGlobal.VisibleOnPageLoad = True
    End Sub

    Protected Sub btnConfirmar_Click(sender As Object, e As EventArgs)
        RaiseEvent Confirmar_Respuesta(Me, EventArgs.Empty)
        rWindowGlobal.VisibleOnPageLoad = False
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        ResultadoConfirmacin = False
        rWindowGlobal.VisibleOnPageLoad = False
    End Sub
End Class