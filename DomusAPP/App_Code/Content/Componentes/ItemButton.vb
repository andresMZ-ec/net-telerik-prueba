Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace DomusAPP.Content.Componentes

    <DefaultProperty("Texto"), ToolboxData("<{0}:ItemButton runat=server></{0}:ItemButton>")>
    Public Class ItemButton
        Inherits WebControl
        Implements INamingContainer

        ' Propiedades
        <Category("Behavior")>
        Public Property Texto As String
            Get
                Return If(ViewState("Texto"), String.Empty)
            End Get
            Set(value As String)
                ViewState("Texto") = value
            End Set
        End Property

        <Category("Behavior")>
        Public Property CommandName As String
            Get
                Return If(ViewState("CommandName"), String.Empty)
            End Get
            Set(value As String)
                ViewState("CommandName") = value
            End Set
        End Property

        <Category("Behavior")>
        Public Property CssClass As String
            Get
                Return If(ViewState("CssClass"), String.Empty)
            End Get
            Set(value As String)
                ViewState("CssClass") = value
            End Set
        End Property

        ' Evento OnClick
        Public Event OnClick As EventHandler

        Protected Overrides Sub Render(writer As HtmlTextWriter)
            Dim btn As New Button()
            btn.ID = Me.ID
            btn.Text = Texto
            btn.CommandName = CommandName
            If Not String.IsNullOrEmpty(CssClass) Then btn.CssClass = CssClass
            AddHandler btn.Click, AddressOf RaiseClick
            btn.RenderControl(writer)
        End Sub

        Private Sub RaiseClick(sender As Object, e As EventArgs)
            RaiseEvent OnClick(Me, e)
        End Sub
    End Class

End Namespace

