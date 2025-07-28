Imports Telerik.Web.UI
Imports System.Linq
Imports System.Collections.Generic

<Serializable()>
Public Class AguajeItem
    Public Property id As Int64
    Public Property fecha_inicio As DateTime
    Public Property fecha_fin As DateTime
    Public Property tipo_aguaje As Int64
End Class

Public Class control_area_aguaje
    Inherits System.Web.UI.UserControl

    Private Property ListaAguajes As List(Of AguajeItem)
        Get
            If Session("lista_aguajes") Is Nothing Then
                Session("lista_aguajes") = New List(Of AguajeItem)
            End If
            Return DirectCast(Session("lista_aguajes"), List(Of AguajeItem))
        End Get
        Set(value As List(Of AguajeItem))
            Session("lista_aguajes") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim lista = New List(Of String) From {"elemento 1", "ELEMENTO 2"}

            rpPrueba.DataSource = lista
            rpPrueba.DataBind()

            If ListaAguajes.Count = 0 Then
                CargarDatos()
            End If
        End If
    End Sub

    Private Sub CargarDatos()
        ListaAguajes.Add(New AguajeItem With {.id = 1, .fecha_inicio = New Date(2025, 1, 15), .fecha_fin = New Date(2025, 1, 18), .tipo_aguaje = 1})
        ListaAguajes.Add(New AguajeItem With {.id = 2, .fecha_inicio = New Date(2025, 2, 10), .fecha_fin = New Date(2025, 2, 13), .tipo_aguaje = 2})
        ListaAguajes.Add(New AguajeItem With {.id = 3, .fecha_inicio = New Date(2025, 3, 20), .fecha_fin = New Date(2025, 3, 23), .tipo_aguaje = 1})
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

    Protected Sub rpMeses_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rgAguajes As RadGrid = DirectCast(e.Item.FindControl("rgAguajes"), RadGrid)
            Dim mesActualString As String = DirectCast(e.Item.DataItem, String)

            ' Guardar el mes actual en el ViewState o en un atributo del Grid para NeedDataSource
            rgAguajes.Attributes("CurrentMonth") = mesActualString
        End If
    End Sub

    Protected Sub rgAguajes_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs)
        Dim rgAguajes As RadGrid = DirectCast(sender, RadGrid)
        Dim mesActualString As String = rgAguajes.Attributes("CurrentMonth")

        ' Filtrar los datos para el mes actual
        Dim filteredData As List(Of AguajeItem) = ListaAguajes.Where(Function(a) a.fecha_inicio.ToString("MMMM yyyy") = mesActualString).ToList()
        rgAguajes.DataSource = filteredData
        rgAguajes.DataBind()
    End Sub


    Protected Sub rgAguajes_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)
        Dim rgAguajes As RadGrid = DirectCast(sender, RadGrid)
        Dim editedItem As GridEditableItem = DirectCast(sender, GridEditableItem)
        Dim idToUpdate As Int64 = DirectCast(editedItem.GetDataKeyValue("id"), Int64)

        Dim aguajeToUpdate As AguajeItem = ListaAguajes.FirstOrDefault(Function(a) a.id = idToUpdate)

        If aguajeToUpdate IsNot Nothing Then
            Dim rdpFechaInicio As RadDatePicker = DirectCast(editedItem.FindControl("rdpFechaInicioEdit"), RadDatePicker)
            Dim rdpFechaFin As RadDatePicker = DirectCast(editedItem.FindControl("rdpFechaFinEdit"), RadDatePicker)
            Dim rcbTipoAguaje As RadComboBox = DirectCast(editedItem.FindControl("rcbTipoAguajeEdit"), RadComboBox)

            If rdpFechaInicio.SelectedDate.HasValue Then
                aguajeToUpdate.fecha_inicio = rdpFechaInicio.SelectedDate.Value
            End If
            If rdpFechaFin.SelectedDate.HasValue Then
                aguajeToUpdate.fecha_fin = rdpFechaFin.SelectedDate.Value
            End If
            If rcbTipoAguaje.SelectedValue IsNot Nothing Then
                aguajeToUpdate.tipo_aguaje = rcbTipoAguaje.SelectedValue
            End If
        End If

        rgAguajes.MasterTableView.IsItemInserted = False
        rgAguajes.Rebind()
    End Sub

    Protected Sub rgAguajes_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)
        Dim rgAguajes As RadGrid = DirectCast(sender, RadGrid)

        Select Case e.CommandName
            Case "Edit"

            Case "Update"

            Case "Delete"

            Case "Cancel"
                rgAguajes.MasterTableView.IsItemInserted = False

        End Select

        If e.CommandName <> "Update" Then
            rgAguajes.Rebind()
        End If
    End Sub

    Protected Sub rgAguajes_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)

    End Sub

    Protected Sub btnAddRow_Command(sender As Object, e As CommandEventArgs)
        Dim mesSeleccionado As String = e.CommandArgument.ToString()

        ' Crear una nueva fila vacía
        Dim newAguaje As New AguajeItem With {
            .id = If(ListaAguajes.Any(), ListaAguajes.Max(Function(a) a.id) + 1, 1),
            .fecha_inicio = DateTime.Today,
            .fecha_fin = DateTime.Today.AddDays(1),
            .tipo_aguaje = 1 ' Asignar el primer tipo por defecto
        }

        ListaAguajes.Add(newAguaje)

        ' Encontrar el RadGrid específico para el mes seleccionado
        For Each item As RepeaterItem In rpMeses.Items
            If DirectCast(item.DataItem, String) = mesSeleccionado Then
                Dim rgAguajes As RadGrid = DirectCast(item.FindControl("rgAguajes"), RadGrid)
                ' Poner el Grid en modo de inserción
                rgAguajes.MasterTableView.IsItemInserted = True
                rgAguajes.Rebind()
                Exit For
            End If
        Next
    End Sub
End Class