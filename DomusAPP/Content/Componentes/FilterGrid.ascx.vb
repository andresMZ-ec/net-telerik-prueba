Imports System.Linq.Expressions
Imports System.Reflection
Imports Telerik.Web.UI

Public Enum FilterType
    Vacio
    NoVacio
    Igual
    NoIgual
    Contiene
    NoContiene
    EmpiezaCon
    TerminaCon
    MayorQue
    MenorQue
    Entre
End Enum

Public Class FilterGrid
    Inherits System.Web.UI.UserControl


    Protected Friend Class FilterRule
        Public Property PropName As String
        Public Property FilterType As List(Of FilterType)

        Public Sub New(columnName As String, filterTypes As List(Of FilterType))
            Me.PropName = columnName
            Me.FilterType = filterTypes
        End Sub
    End Class

    <Serializable()>
    Protected Friend Class ConfigurationFilter
        Public Property ColumnName As String
        Public Property PropName As String
        Public Property DataType As Type
        Public Property FilterTypes As List(Of FilterType)

        Public Sub New(columnName As String, propName As String, dataType As Type, filterTypes As List(Of FilterType))
            Me.ColumnName = columnName
            Me.PropName = propName
            Me.DataType = dataType
            Me.FilterTypes = filterTypes
        End Sub
    End Class

    Protected Friend Class FilterCondition
        Public Property ColumnName As String
        Public Property DataType As String
        Public Property PropName As String
        Public Property FilterType As FilterType
        Public Property Value As Object
        Public Property Value2 As Object ' Para filtros entre rangos
        Public Sub New(columnName As String, filterType As FilterType, value As Object, Optional value2 As Object = Nothing)
            Me.ColumnName = columnName
            Me.FilterType = filterType
            Me.Value = value
            Me.Value2 = value2
        End Sub
    End Class


#Region "Propiedades"
    Private _STATEDATASOURCE As String
    Private _STATEFILTERABLECOLUMNS As String
    Private _STATECONFIGURATION As String

    Private Property DataTable As IEnumerable
        Get
            If ViewState(_STATEDATASOURCE) Is Nothing Then
                Return Nothing
            End If
            Return ViewState(_STATEDATASOURCE)
        End Get
        Set(value As IEnumerable)
            ViewState(_STATEDATASOURCE) = value
            DataSource = value
        End Set
    End Property

    Private Property _ConfigurationFilters As List(Of ConfigurationFilter)
        Get
            Return TryCast(ViewState(_STATECONFIGURATION), List(Of ConfigurationFilter))
        End Get
        Set(value As List(Of ConfigurationFilter))
            ViewState(_STATECONFIGURATION) = value
        End Set
    End Property

    Private Property FilterableColumns As List(Of String)
        Get
            Return TryCast(ViewState(_STATEFILTERABLECOLUMNS), List(Of String))
        End Get
        Set(value As List(Of String))
            If value.Count > 0 Then
                ViewState(_STATEFILTERABLECOLUMNS) = value
            Else
                Dim defaultColumns As New List(Of String)

                If DataTable IsNot Nothing Then
                    Dim dataType As Type = Nothing

                    'Verificar si el DataTable tiene elementos
                    Dim listType = DataTable.GetType()

                    If listType.IsGenericType Then
                        dataType = listType.GetGenericArguments()(0)
                    ElseIf TypeOf DataTable Is IEnumerable Then
                        ' Obtener el tipo del primer elemento en la colección
                        Dim enumerator = DataTable.GetEnumerator()
                        If enumerator.MoveNext() Then
                            dataType = enumerator.Current.GetType()
                        End If

                        ' Obtener las propiedades del primer elemento del DataTable
                        Dim firstItem = DataTable.Cast(Of Object)().FirstOrDefault()
                        If firstItem IsNot Nothing Then
                            Dim properties = firstItem.GetType().GetProperties()
                            For Each prop In properties
                                defaultColumns.Add(prop.Name)
                            Next
                        End If
                    End If
                End If

            ' Si no hay columnas, asignar las columnas del DataTable
        End Set
    End Property

    Public Property GridID As String
        Get
            Return hfGridID.Value
        End Get
        Set(value As String)
            hfGridID.Value = value
        End Set
    End Property

    Protected Property DataSource As IEnumerable
        Get
            Return rpFilter.DataSource
        End Get
        Set(value As IEnumerable)
            rpFilter.DataSource = value
            rpFilter.DataBind()
        End Set
    End Property
#End Region

    Private Function GetFilterTypes(dataType As Type) As List(Of FilterType)
        Dim filterTypes As New List(Of FilterType) From {
            FilterType.Vacio,
            FilterType.NoVacio
        }
        If dataType Is GetType(String) Then
            filterTypes.AddRange(New List(Of FilterType) From {
                FilterType.Igual,
                FilterType.NoIgual,
                FilterType.Contiene,
                FilterType.NoContiene,
                FilterType.EmpiezaCon,
                FilterType.TerminaCon
            })
        ElseIf dataType Is GetType(Integer) OrElse
            dataType Is GetType(Double) OrElse
            dataType Is GetType(Decimal) OrElse
            dataType Is GetType(DateTime) Then
            filterTypes.AddRange(New List(Of FilterType) From {
                FilterType.Igual,
                FilterType.NoIgual,
                FilterType.MayorQue,
                FilterType.MenorQue,
                FilterType.Entre
            })
        ElseIf dataType Is GetType(Boolean) Then
            filterTypes.AddRange(New List(Of FilterType) From {
                FilterType.Igual,
                FilterType.NoIgual
            })
        End If
        Return filterTypes
    End Function

    ''' <summary>
    ''' Permite establecer la base de datos del filtro
    ''' </summary>
    ''' <typeparam name="T">Clase para identificar el tipo de datos de las columnas</typeparam>
    ''' <param name="value">Conjunto de datos</param>
    Protected Friend Sub SetDataSource(Of T As IEnumerable)(value As T)
        If value Is Nothing Then
            Throw New ArgumentNullException(NameOf(value), "No se puede asignar un origen de datos nulo.")
        End If

        If TypeOf value Is IEnumerable Then
            DataTable = value
        Else
            Throw New Exception("El tipo de dato no es compatible. Se espera una colección Enumerable.")
        End If
    End Sub

    ''' <summary>
    ''' Establece las columnas que serán filtrables en la Grid
    ''' </summary>
    ''' <typeparam name="T">Clase que contiene el DataSource de la grid</typeparam>
    ''' <param name="columns">Se especifica el nombre de la propiedad de la clase</param>
    Protected Friend Sub SetFilterableColumns(Of T)(ParamArray columns() As String)
        If columns Is Nothing OrElse columns.Length = 0 Then
            Throw New ArgumentException("Se debe proporcionar al menos una regla de filtro válida.", NameOf(columns))
        End If

        Dim configurationFilter As New List(Of ConfigurationFilter)()
        Dim columnsNames = columns _
            .Select(Function(c) c.Trim().ToLower()) _
            .Distinct() _
            .ToList()
        Dim columnsClass = GetType(T).GetProperties().Select(Function(p) p.Name).ToList()
        Dim invalids = columnsNames.Where(Function(c) Not columnsClass.Contains(c)).ToList()

        If invalids.Any() Then
            Dim invalidCols As String = String.Join(", ", invalids)
            Throw New ArgumentException($"Las siguientes columnas no son válidas para el tipo {GetType(T).Name}: {invalidCols}. Especifique los nombres de la propiedad  de la clase")
        End If

        'Buscar columnas de la Grid
        Dim grid As RadGrid = TryCast(Me.FindControl(GridID), RadGrid)
        Dim GridBoundColumnGrid As New List(Of GridBoundColumn)

        If grid Is Nothing Then
            Throw New Exception($"No se encontró la Grid con ID '{GridID}'. Asegúrese de que el ID sea correcto y que la grid esté en el mismo contenedor que el filtro.")
        End If

        For Each column In grid.MasterTableView.RenderColumns
            If TypeOf column Is GridBoundColumn Then
                GridBoundColumnGrid.Add(TryCast(column, GridBoundColumn))
            ElseIf TypeOf column Is GridTemplateColumn Then
                Dim templateCol As GridTemplateColumn = TryCast(column, GridTemplateColumn)

                Dim prop = templateCol.GetType().GetProperty("DataField", BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
                Dim dataField As String = Nothing

                If prop IsNot Nothing Then
                    dataField = TryCast(prop.GetValue(templateCol, Nothing), String)
                End If

                Dim boundCol As New GridBoundColumn() With {
                    .DataField = dataField,
                    .HeaderText = templateCol.HeaderText,
                    .UniqueName = templateCol.UniqueName
                }

                GridBoundColumnGrid.Add(boundCol)
            End If
        Next

        Dim columnsGrid = GridBoundColumnGrid _
            .Where(Function(c) columnsNames.Contains(c.DataField)) _
            .ToList()

        If columnsGrid.Count > 0 Then
            For Each column In columns
                Dim colGrid = columnsGrid.FirstOrDefault(Function(c) c.DataField.ToLower().Trim() = column)

                If colGrid IsNot Nothing Then
                    Dim propInfo = GetType(T).GetProperty(column)
                    Dim dataType As Type = propInfo.PropertyType
                    Dim filters As New List(Of FilterType)

                    If Nullable.GetUnderlyingType(dataType) IsNot Nothing Then
                        dataType = Nullable.GetUnderlyingType(dataType)
                    End If

                    filters = GetFilterTypes(dataType)

                    Dim configFilter As New ConfigurationFilter(
                        colGrid.HeaderText,
                        column,
                        dataType,
                        filters)

                    configurationFilter.Add(configFilter)
                End If
            Next
        End If

        _ConfigurationFilters = configurationFilter
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _STATEDATASOURCE = "DATA_SOURCE_" & Me.ClientID
        _STATEFILTERABLECOLUMNS = "FILTERABLE_COLUMNS_" & Me.ClientID
        _STATECONFIGURATION = "CONFIGURATION_FILTERS_" & Me.ClientID
    End Sub

    Protected Sub rpFilter_ItemCommand(source As Object, e As RepeaterCommandEventArgs)

    End Sub

    Protected Sub rpFilter_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

    End Sub
End Class