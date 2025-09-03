Imports System.ComponentModel
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace DomusAPP.Content.Componentes

    <ToolboxData("<{0}:GridActions runat=server></{0}:GridActions>")>
    Public Class GridActions
        Inherits CompositeControl
        Implements INamingContainer

        ' Propiedades
        <Category("Behavior")>
        Public Property CommandArgument As String
            Get
                Return If(ViewState("CommandArgument"), String.Empty)
            End Get
            Set(value As String)
                ViewState("CommandArgument") = value
            End Set
        End Property

        ' Colección Items
        Private _items As ItemButtonCollection
        <PersistenceMode(PersistenceMode.InnerProperty)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public ReadOnly Property Items As ItemButtonCollection
            Get
                If _items Is Nothing Then
                    _items = New ItemButtonCollection()
                End If
                Return _items
            End Get
        End Property

        Protected Overrides Sub CreateChildControls()
            Controls.Clear()

            ' Botón principal (ellipsis)
            Dim mainBtn As New Button()
            mainBtn.Text = "⋯"
            mainBtn.CssClass = "grid-actions-btn"
            mainBtn.OnClientClick = "toggleGridActionsMenu('" & Me.ClientID & "_menu'); return false;"
            Controls.Add(mainBtn)

            ' Contenedor del menú
            Dim pnl As New Panel()
            pnl.ID = Me.ClientID & "_menu"
            pnl.CssClass = "grid-actions-menu"
            pnl.Style("display") = "none"

            ' Agregar los ItemButton
            For Each item As ItemButton In Items
                pnl.Controls.Add(item)
                pnl.Controls.Add(New LiteralControl("<br/>"))
            Next

            Controls.Add(pnl)
        End Sub

        Protected Overrides Sub RenderContents(writer As HtmlTextWriter)
            EnsureChildControls()
            For Each ctrl As Control In Controls
                ctrl.RenderControl(writer)
            Next
        End Sub
    End Class
End Namespace

