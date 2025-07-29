Imports Telerik.Web.UI
Imports System.Linq
Imports System.Collections.Generic

Public Class add_grid_aguaje
    Inherits System.Web.UI.UserControl

    ' Clase de modelo de datos para AguajeEvento
    ' Incluimos MesNombre y MesNumero para la agrupación
    <Serializable()>
    Public Class AguajeEvento
        Public Property Id As Integer
        Public Property FechaInicio As DateTime
        Public Property FechaFin As DateTime
        Public Property TipoAguaje As String
        Public Property MesNombre As String
            Get
                Return Me.FechaInicio.ToString("MMMM yyyy") ' Obtiene el nombre del mes y año
            End Get
            Set(value As String)
                ' No se necesita setter si se calcula de FechaInicio
            End Set
        End Property
        Public Property MesNumero As Integer
            Get
                Return Me.FechaInicio.Month ' Obtiene el número del mes para ordenar
            End Get
            Set(value As Integer)
                ' No se necesita setter si se calcula de FechaInicio
            End Set
        End Property
    End Class


    ' Clase auxiliar para los datos del ComboBox
    Public Class RadComboBoxItemData
        Public Property Text As String
        Public Property Value As String

        Public Sub New(ByVal text As String, ByVal value As String)
            Me.Text = text
            Me.Value = value
        End Sub
    End Class

    ' Propiedad para simular la base de datos de los aguajes
    ' Usamos Session para simular la persistencia de datos entre postbacks
    Private Property AguajeData As List(Of AguajeEvento)
        Get
            If Session("AguajeData") Is Nothing Then
                Session("AguajeData") = New List(Of AguajeEvento)()
            End If
            Return DirectCast(Session("AguajeData"), List(Of AguajeEvento))
        End Get
        Set(value As List(Of AguajeEvento))
            Session("AguajeData") = value
        End Set
    End Property

    ' Propiedad para los tipos de aguaje (lo que precarga el RadComboBox)
    Private ReadOnly Property TiposAguaje As List(Of RadComboBoxItemData)
        Get
            Dim tipos As New List(Of RadComboBoxItemData)
            tipos.Add(New RadComboBoxItemData("Aguaje de Luna Nueva", "Luna Nueva"))
            tipos.Add(New RadComboBoxItemData("Aguaje de Luna Llena", "Luna Llena"))
            tipos.Add(New RadComboBoxItemData("Aguaje de Conjunción", "Conjunción"))
            tipos.Add(New RadComboBoxItemData("Aguaje de Oposición", "Oposición"))
            Return tipos
        End Get
    End Property

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Inicializar datos simulados la primera vez que se carga el control
            ' NOTA: En un caso real, esto vendría de una base de datos o servicio
            If AguajeData.Count = 0 Then
                SimularCargaInicialDeDatos()
            End If
            rgAguajes.Rebind() ' Rebind inicial para cargar los datos y grupos
        End If
    End Sub

    ' Método para simular la carga inicial de algunos datos
    Private Sub SimularCargaInicialDeDatos()
        ' Datos de ejemplo para que veas cómo se agrupan
        AguajeData.Add(New AguajeEvento With {.Id = 1, .FechaInicio = New Date(2025, 1, 15), .FechaFin = New Date(2025, 1, 18), .TipoAguaje = "Luna Nueva"})
        AguajeData.Add(New AguajeEvento With {.Id = 2, .FechaInicio = New Date(2025, 2, 10), .FechaFin = New Date(2025, 2, 13), .TipoAguaje = "Luna Llena"})
        AguajeData.Add(New AguajeEvento With {.Id = 3, .FechaInicio = New Date(2025, 3, 20), .FechaFin = New Date(2025, 3, 23), .TipoAguaje = "Conjunción"})
        AguajeData.Add(New AguajeEvento With {.Id = 4, .FechaInicio = New Date(2025, 1, 28), .FechaFin = New Date(2025, 1, 31), .TipoAguaje = "Oposición"})
    End Sub


    Protected Sub rgAguajes_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs)
        Dim currentYear As Integer = DateTime.Today.Year
        Dim allMonthsData As New List(Of AguajeEvento)

        ' Añadir un elemento "placeholder" para cada mes del año
        ' Esto asegura que todos los meses aparezcan como grupos, incluso si no tienen aguajes reales
        For i As Integer = 1 To 12
            Dim placeholderDate As New Date(currentYear, i, 1)
            allMonthsData.Add(New AguajeEvento With {
                .Id = -(i), ' Usamos IDs negativos para marcarlos como placeholders
                .FechaInicio = placeholderDate,
                .FechaFin = placeholderDate,
                .TipoAguaje = "Placeholder"
            })
        Next

        ' Añadir los datos reales de aguajes
        allMonthsData.AddRange(AguajeData)

        ' Filtrar los placeholders si ya hay datos reales para ese mes,
        ' o si no queremos que se muestren como filas vacías.
        ' Para el objetivo de "mostrar grupos vacíos", es mejor no eliminarlos aquí,
        ' y luego ocultar las filas de placeholder en ItemDataBound.

        rgAguajes.DataSource = allMonthsData.OrderBy(Function(a) a.FechaInicio).ToList()
    End Sub

    ' Evento para ocultar las filas de placeholder y cargar el RadComboBox
    Protected Sub rgAguajes_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs)
        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As AguajeEvento = DirectCast(e.Item.DataItem, AguajeEvento)
            If dataItem.Id < 0 Then ' Si es un placeholder
                e.Item.Visible = False ' Ocultar la fila
            End If
        End If

        If e.Item.IsInEditMode AndAlso (e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem OrElse e.Item.ItemType = GridItemType.EditItem) Then
            Dim rcbTipoAguajeEdit As RadComboBox = DirectCast(e.Item.FindControl("rcbTipoAguajeEdit"), RadComboBox)
            If rcbTipoAguajeEdit IsNot Nothing Then
                rcbTipoAguajeEdit.DataSource = TiposAguaje
                rcbTipoAguajeEdit.DataBind()
            End If
        End If
    End Sub

    ' Evento ItemCommand para manejar comandos personalizados (como "AddRowForMonth")
    Protected Sub rgAguajes_ItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "AddRowForMonth" Then
            Dim selectedMonthNumber As Integer = CInt(e.CommandArgument)
            Dim newId As Integer = If(AguajeData.Any(), AguajeData.Max(Function(a) a.Id) + 1, 1)

            ' Crear una nueva fila con el mes y año correctos
            Dim newAguaje As New AguajeEvento With {
                .Id = newId,
                .FechaInicio = New Date(DateTime.Today.Year, selectedMonthNumber, 1),
                .FechaFin = New Date(DateTime.Today.Year, selectedMonthNumber, 1).AddDays(1),
                .TipoAguaje = TiposAguaje.FirstOrDefault()?.Value ' Asignar el primer tipo por defecto
            }

            AguajeData.Add(newAguaje)

            ' Poner el grid en modo de inserción para la nueva fila
            rgAguajes.MasterTableView.ClearEditItems()
            Dim newIndex As Integer = AguajeData.IndexOf(newAguaje)
            rgAguajes.EditIndexes.Add(newIndex)
            rgAguajes.Rebind()

        ElseIf e.CommandName = "Edit" Then
            rgAguajes.MasterTableView.ClearEditItems()
            e.Item.Edit = True
            rgAguajes.Rebind()
        ElseIf e.CommandName = "Delete" Then
            ' Se manejará en rgAguajes_DeleteCommand
        ElseIf e.CommandName = "Update" Then
            ' Se manejará en rgAguajes_UpdateCommand
        ElseIf e.CommandName = "Cancel" Then
            rgAguajes.MasterTableView.ClearEditItems()
            rgAguajes.MasterTableView.IsItemInserted = False
            rgAguajes.Rebind()
        End If
    End Sub

    ' Evento UpdateCommand para manejar la actualización de filas
    Protected Sub rgAguajes_UpdateCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs)
        Dim editedItem As GridEditableItem = DirectCast(e.Item, GridEditableItem)
        Dim idToUpdate As Integer = DirectCast(editedItem.GetDataKeyValue("Id"), Integer)

        Dim aguajeToUpdate As AguajeEvento = AguajeData.FirstOrDefault(Function(a) a.Id = idToUpdate)

        If aguajeToUpdate IsNot Nothing Then
            Dim rdpFechaInicioEdit As RadDatePicker = DirectCast(editedItem.FindControl("rdpFechaInicioEdit"), RadDatePicker)
            Dim rdpFechaFinEdit As RadDatePicker = DirectCast(editedItem.FindControl("rdpFechaFinEdit"), RadDatePicker)
            Dim rcbTipoAguajeEdit As RadComboBox = DirectCast(editedItem.FindControl("rcbTipoAguajeEdit"), RadComboBox)

            If rdpFechaInicioEdit.SelectedDate.HasValue Then
                aguajeToUpdate.FechaInicio = rdpFechaInicioEdit.SelectedDate.Value
            End If
            If rdpFechaFinEdit.SelectedDate.HasValue Then
                aguajeToUpdate.FechaFin = rdpFechaFinEdit.SelectedDate.Value
            End If
            If rcbTipoAguajeEdit.SelectedValue IsNot Nothing Then
                aguajeToUpdate.TipoAguaje = rcbTipoAguajeEdit.SelectedValue
            End If
        End If

        rgAguajes.MasterTableView.IsItemInserted = False
        rgAguajes.Rebind()
    End Sub

    ' Evento DeleteCommand para manejar la eliminación de filas
    Protected Sub rgAguajes_DeleteCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs)
        Dim idToDelete As Integer = DirectCast(e.Item.DataItem, AguajeEvento).Id
        Dim itemToRemove As AguajeEvento = AguajeData.FirstOrDefault(Function(a) a.Id = idToDelete)
        If itemToRemove IsNot Nothing Then
            AguajeData.Remove(itemToRemove)
        End If
        rgAguajes.Rebind()
    End Sub

    ' Evento para enlazar datos al RadComboBox en el modo de edición (alternativa a ItemDataBound para DataBound)
    Protected Sub rcbTipoAguajeEdit_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        Dim rcb As RadComboBox = DirectCast(sender, RadComboBox)
        rcb.DataSource = TiposAguaje
        rcb.DataBind()
    End Sub
#End Region

End Class