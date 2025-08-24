Imports Telerik.Web.UI

Public Class FiltroGrid
    Inherits UserControl

#Region "Propiedades y Estados Privados"
    Private Class FiltroItem
        Public Property DataField As String
        Public Property TipoDato As TipoFiltro
        Public Property Opciones As List(Of String)
    End Class

    Private Property _Columnas As New List(Of FiltroItem)
    Private Property _DataSourceFiltrado As Object

#End Region

    Public ReadOnly Property DataSourceFiltrado As Object
        Get
            If _DataSourceFiltrado Is Nothing Then
                Return DataSourceGrid
            End If
            Return _DataSourceFiltrado
        End Get
    End Property

    Public Enum TipoFiltro
        [String]
        [Decimal]
        [Date]
        [Boolean]
        [List]
    End Enum

    Public Property Grid As RadGrid
    Public Property DataSourceGrid As Object
        Get
            Dim obj = TryCast(ViewState("DataSourceGrid"), Object)
            If obj Is Nothing Then
                obj = New Object
                ViewState("DataSourceGrid") = obj
            End If
            Return obj
        End Get
        Set(value As Object)
            ViewState("DataSourceGrid") = value
            _DataSourceFiltrado = value
            Grid.DataSource = value
        End Set
    End Property

    Public Property MostrarBuscador As Boolean = True
    Public Property SeleccionarColumnas As Boolean = False

    Public Sub AgregarColumna(DataFIeld As String, TipoDato As TipoFiltro, Optional Opciones As List(Of String) = Nothing)
        Me._Columnas.Add(New FiltroItem With {
            .DataFIeld = DataFIeld,
            .TipoDato = TipoDato,
            .Opciones = Opciones
        })
    End Sub

    Private Sub RestaurarDataSource()
        _DataSourceFiltrado = DataSourceGrid
    End Sub

    Private Function ObtenerCampoFiltrar(tipo As TipoFiltro) As FiltroItem
        Dim dataType As Type = GetType(String)

        Select Case tipo
            Case TipoFiltro.Boolean
                dataType = GetType(Boolean)
            Case TipoFiltro.Date
                dataType = GetType(DateTime)
            Case TipoFiltro.List
                dataType = GetType(Dictionary(Of Integer, String))
            Case Else
                dataType = GetType(String)
        End Select

        Return New FiltroItem
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RenderizarFiltros()
        End If
    End Sub


    Private Sub RenderizarFiltros()
        FiltroBody.Controls.Clear()

        For Each col In _Columnas
            Dim ItemBase As New HtmlGenericControl("div")   'Div principal
            Dim HeaderDiv As New HtmlGenericControl("div")  'Div para el titulo de columna y el botón de limpiar
            Dim BodyFields As New HtmlGenericControl("div") 'Div para los campos del filtro

            ItemBase.Attributes.Add("class", "FiltroItem")
            HeaderDiv.Attributes.Add("class", "flex Header")
            BodyFields.Attributes.Add("class", "Fields")

            'Completar Header
            Dim LabelColumna As New Label With {.ID = "lblColumn_" & col.DataField, .Text = col.DataField, .CssClass = "Title"}
            Dim BotonLimpiar As New Button With {.ID = "btnLimpiar_" & col.DataField, .Text = "Limpiar", .CssClass = "Clean"}
            HeaderDiv.Controls.Add(LabelColumna)
            HeaderDiv.Controls.Add(BotonLimpiar)

            'Añadir controles de
            Select Case col.TipoDato
                Case TipoFiltro.Date
                    Dim DatePickerInicio As New RadDatePicker With {.ID = "FechaInicio_" & col.DataField}
                    Dim DatePickerFin As New RadDatePicker With {.ID = "FechaFin_" & col.DataField}
                    BodyFields.Controls.Add(DatePickerInicio)
                    BodyFields.Controls.Add(DatePickerFin)
                Case Else

            End Select

            'Completar Item
            ItemBase.Controls.Add(HeaderDiv)
            ItemBase.Controls.Add(BodyFields)
            FiltroBody.Controls.Add(ItemBase)
        Next
    End Sub

End Class