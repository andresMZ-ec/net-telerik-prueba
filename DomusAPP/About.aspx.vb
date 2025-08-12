
Public Class About
    Inherits Page

    Private _Notificacion As Notificacion

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        _Notificacion = CType(Master, SiteMaster).Notification

        If _Notificacion.ResultadoConfirmacin.HasValue Then
            If _Notificacion.ResultadoConfirmacin.Value Then
                _Notificacion.Mostrar("Eliminado", "hecho")
            Else
                _Notificacion.Mostrar("Cancelado", "cancelado")
            End If
        End If
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs)
        _Notificacion.Confirmar("Guardar?", "¿Estás seguro?")

        AddHandler _Notificacion.Confirmar_Respuesta, AddressOf Guardar
    End Sub

    Private Sub Guardar()
        Dim ok = True
    End Sub

    Protected Sub btnEliminar_Click(sender As Object, e As EventArgs)
        _Notificacion.Mostrar("Guardar registro", "guardado exitoso")
    End Sub
End Class