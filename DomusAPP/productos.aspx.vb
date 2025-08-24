
Public Class productos
    Inherits System.Web.UI.Page

    <Serializable()>
    Public Class Productos
        Public Property id As Integer
        Public Property nombre As String
        Public Property precio As Decimal
        Public Property fecha_creacion As DateTime
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            filtroGrid.Grid = rgPrueba
            filtroGrid.AgregarColumna(NameOf(Productos.precio), FiltroGrid.TipoFiltro.Date)
            filtroGrid.AgregarColumna("estado", FiltroGrid.TipoFiltro.List, New List(Of String) From {"Aprobado", "Cancelado", "Pausado", "Por Pagar"})
        End If
    End Sub

    Protected Sub rgPrueba_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs)
        Dim listaProductos As List(Of Productos) = New List(Of Productos) From {
            New Productos With {.id = 1, .nombre = "Galleta", .precio = 0.152, .fecha_creacion = New DateTime(2025, 2, 25)},
            New Productos With {.id = 2, .nombre = "Chocolate", .precio = 1.25, .fecha_creacion = New DateTime(2025, 2, 26)}
        }

        filtroGrid.DataSourceGrid = listaProductos
    End Sub
End Class