Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    ''' <summary>
    ''' Propiedad pública para acceder al método de notificación POPUP
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Notification() As Notificacion
        Get
            Return Me.FindControl("Notificacion")
        End Get
    End Property

    Public ReadOnly Property Confirmacion() As ModalConfirmacion
        Get
            Return Me.FindControl("rwConfirmacion")
        End Get
    End Property

End Class