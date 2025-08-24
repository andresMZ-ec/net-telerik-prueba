Public Class NotificacionPrueba
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim notification As New Telerik.Web.UI.RadNotification()
        RadNotificationGLObal.AutoCloseDelay = 1555555
        RadNotificationGLObal.Show()

        ' Inyectar JavaScript para modificar estilos
        Dim script As String = "
        setTimeout(function() {
            var notification = document.querySelector('.RadNotificationGlobal');
            if (notification) {
                notification.style.width = '300px';
                notification.style.maxWidth = '90vw';
                notification.style.left = '50% !important';
                notification.style.transform = 'translateX(-50%)';
            }
        }, 100);"

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ModifyNotification", script, True)
    End Sub

    Protected Sub RadNotificationGLObal_PreRender(sender As Object, e As EventArgs)
        RadNotificationGLObal.Attributes("style") = ""
    End Sub
End Class