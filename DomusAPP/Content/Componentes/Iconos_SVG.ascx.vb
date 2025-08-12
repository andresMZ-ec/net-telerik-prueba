Public Class iconos_svg
    Inherits System.Web.UI.UserControl

#Region "Propiedades del componente"

    Public Enum TipoIcono
        [LogoDomus]
        [Imprimir]
        [Guardar]
        [X]
        [ExportarExcel]
        [Agregar]
        [NuevoUsuario]
        [GrupoUsuario]
        [Filtro]
        [OrdenAsc]
        [OrdenDesc]
        [Copiar]
    End Enum

    Public Property Tipo As TipoIcono = TipoIcono.LogoDomus
    Public Property Opacity As Integer = 1
    Public Property FillColor As String = "currentColor"
    Public Property Size As Integer = 16
    Public Property StrokeWidth As Integer = 1
    Public Property StrokeColor As String = "currentColor"
    Public Property CssClass As String

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Pre_Render(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
        If Not String.IsNullOrEmpty(Tipo) Then
            Dim icon As String = ObtenerIcono(Tipo)

            If Not String.IsNullOrEmpty(icon) Then
                Dim svg As String = icon.Replace("<svg", String.Format("<svg width=""{0}"" height=""{1}"" stroke-width=""{2}"" fill=""{3}"" stroke=""{4}"" class=""{5}"" style=""fill-opacity: {6};""", Size, Size, StrokeWidth, FillColor, StrokeColor, CssClass, Opacity))

                containerRender.Text = svg
            End If
        End If
    End Sub

    Private Function ObtenerIcono(Tipo As TipoIcono) As String
        Select Case Tipo
            Case TipoIcono.Imprimir
                Return "<svg viewBox=""0 0 24 24""><path class=""cls-1"" d=""M20,5H19V3a3,3,0,0,0-3-3H8A3,3,0,0,0,5,3V5H4A4,4,0,0,0,0,9v8a4,4,0,0,0,4,4H5a3,3,0,0,0,3,3h8a3,3,0,0,0,3-3h1a4,4,0,0,0,4-4V9A4,4,0,0,0,20,5ZM7,3A1,1,0,0,1,8,2h8a1,1,0,0,1,1,1V5H7ZM17,21a1,1,0,0,1-1,1H8a1,1,0,0,1-1-1V17H17Zm5-4a2,2,0,0,1-2,2H19V17a1,1,0,0,0,0-2H5a1,1,0,0,0,0,2v2H4a2,2,0,0,1-2-2V9A2,2,0,0,1,4,7H20a2,2,0,0,1,2,2Z""/><path class=""cls-1"" d=""M6,9H5a1,1,0,0,0,0,2H6A1,1,0,0,0,6,9Z""/><path class=""cls-1"" d=""M11,9H10a1,1,0,0,0,0,2h1a1,1,0,0,0,0-2Z""/></svg>"

            Case TipoIcono.Guardar
                Return "<svg fill=""none"" height=""24"" stroke=""currentColor"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""2"" viewBox=""0 0 24 24"" width=""24""><path d=""M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z""/><polyline points=""17 21 17 13 7 13 7 21""/><polyline points=""7 3 7 8 15 8""/></svg>"

            Case TipoIcono.X
                Return "<svg width=""14"" height=""14"" viewBox=""0 0 14 14"" fill=""none""><path d=""M0.3 13.7C0.5 13.9 0.7 14 1 14C1.3 14 1.5 13.9 1.7 13.7L7 8.4L12.3 13.7C12.5 13.9 12.8 14 13 14C13.2 14 13.5 13.9 13.7 13.7C14.1 13.3 14.1 12.7 13.7 12.3L8.4 7L13.7 1.7C14.1 1.3 14.1 0.7 13.7 0.3C13.3 -0.1 12.7 -0.1 12.3 0.3L7 5.6L1.7 0.3C1.3 -0.1 0.7 -0.1 0.3 0.3C-0.1 0.7 -0.1 1.3 0.3 1.7L5.6 7L0.3 12.3C-0.1 12.7 -0.1 13.3 0.3 13.7Z"" fill=""black""/></svg>"

            Case TipoIcono.ExportarExcel
                Return "<svg viewBox=""0 0 256 256""><rect fill=""none"" height=""256"" width=""256""/><line fill=""none"" stroke=""#000"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""16"" x1=""152"" x2=""208"" y1=""96"" y2=""96""/><line fill=""none"" stroke=""#000"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""16"" x1=""152"" x2=""208"" y1=""160"" y2=""160""/><path d=""M64,72V40a8,8,0,0,1,8-8H200a8,8,0,0,1,8,8V216a8,8,0,0,1-8,8H72a8,8,0,0,1-8-8V184"" fill=""none"" stroke=""#000"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""16""/><line fill=""none"" stroke=""#000"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""16"" x1=""136"" x2=""136"" y1=""184"" y2=""224""/><line fill=""none"" stroke=""#000"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""16"" x1=""136"" x2=""136"" y1=""32"" y2=""72""/><rect fill=""none"" height=""112"" rx=""8"" stroke=""#000"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""16"" width=""120"" x=""32"" y=""72""/><line fill=""none"" stroke=""#000"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""16"" x1=""74"" x2=""110"" y1=""104"" y2=""152""/><line fill=""none"" stroke=""#000"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""16"" x1=""110"" x2=""74"" y1=""104"" y2=""152""/></svg>"

            Case TipoIcono.Agregar
                Return "<svg height=""32px"" version=""1.1"" viewBox=""0 0 32 32"" width=""32px""><path d=""M28,14H18V4c0-1.104-0.896-2-2-2s-2,0.896-2,2v10H4c-1.104,0-2,0.896-2,2s0.896,2,2,2h10v10c0,1.104,0.896,2,2,2  s2-0.896,2-2V18h10c1.104,0,2-0.896,2-2S29.104,14,28,14z""/></svg>"

            Case TipoIcono.NuevoUsuario
                Return "<svg viewBox=""0 0 24 24""><circle cx=""15"" cy=""8"" r=""4""/><path d=""M15,14c-6.1,0-8,4-8,4v2h16v-2C23,18,21.1,14,15,14z""/><line fill=""none"" stroke=""#000000"" stroke-miterlimit=""10"" stroke-width=""2"" x1=""5"" x2=""5"" y1=""7"" y2=""15""/><line fill=""none"" stroke=""#000000"" stroke-miterlimit=""10"" stroke-width=""2"" x1=""9"" x2=""1"" y1=""11"" y2=""11""/></svg>"

            Case TipoIcono.GrupoUsuario
                Return "<svg height=""70px"" viewBox=""0 0 70 70"" width=""70px""><g><path d=""M18.001,40.174c1.841-1.132,4.094-2.016,6.516-2.557c1.5,0.668,3.095,1.097,4.722,1.279   c1.043-0.376,2.119-0.707,3.223-0.975c-0.712-0.877-1.286-1.869-1.704-2.938c-2.058-0.042-3.989-0.598-5.67-1.554   c-4.024,0.687-7.581,2.157-10.219,4.02C14.358,37.81,14.033,38.376,14,39l0.003,5.895C13.943,46.039,14.854,47,16,47h5.009l0-0.999   l0-0.079l0.004-0.079c0.055-1.053,0.442-2.036,1.084-2.843h-4.094L18.001,40.174z""/><path d=""M30.041,31.938C30.015,31.629,30,31.316,30,31c0-1.061,0.159-2.084,0.441-3.056c-1.943-0.271-3.449-1.927-3.449-3.944   c0-2.206,1.794-4,4-4c1.53,0,2.846,0.873,3.519,2.138c1.075-0.789,2.295-1.386,3.616-1.744C36.807,17.789,34.111,16,30.992,16   c-4.418,0-8,3.582-8,8C22.992,28.096,26.072,31.468,30.041,31.938z""/><path d=""M55.191,44.329c-2.156-1.624-5.23-2.919-8.723-3.656C44.828,41.515,42.975,42,41.008,42c-2.15,0-4.165-0.576-5.913-1.57   c-4.024,0.687-7.581,2.157-10.219,4.02c-0.511,0.361-0.836,0.926-0.869,1.55l0.003,5.895C23.951,53.039,24.863,54,26.008,54h27.995   c1.064,0,1.942-0.833,1.997-1.895l0.007-6.155C56.008,45.31,55.702,44.714,55.191,44.329z M52.002,50H28.01l-0.001-2.826   c1.841-1.132,4.094-2.016,6.516-2.557C36.564,45.525,38.777,46,41.008,46c2.029,0,3.996-0.377,5.866-1.122   c1.989,0.523,3.756,1.25,5.132,2.112L52.002,50z""/><path d=""M33,31c0,4.418,3.582,8,8,8s8-3.582,8-8s-3.582-8-8-8S33,26.582,33,31z M41,27c2.206,0,4,1.794,4,4c0,2.206-1.794,4-4,4   s-4-1.794-4-4C37,28.794,38.794,27,41,27z""/></g></svg>"

            Case TipoIcono.Filtro
                Return "<svg viewBox=""0 0 32 32""><path d=""  M3.241,7.646L13,19v9l6-4v-5l9.759-11.354C29.315,6.996,28.848,6,27.986,6H4.014C3.152,6,2.685,6.996,3.241,7.646z"" fill=""none"" stroke=""#000000"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-miterlimit=""10"" stroke-width=""2""/></svg>"

            Case TipoIcono.OrdenAsc
                Return "<svg height=""20"" viewBox=""0 0 20 20"" width=""20""><g><path d=""M 3.5 0 L 0 3.5 L 0 5 L 3 2 L 3 20 L 4 20 L 4 2 L 7 5 L 7 3.5 L 3.5 0 z M 7 9 L 7 10 L 11 10 L 11 9 L 7 9 z M 7 13 L 7 14 L 15 14 L 15 13 L 7 13 z M 7 17 L 7 18 L 20 18 L 20 17 L 7 17 z ""/></g></svg>"

            Case TipoIcono.OrdenDesc
                Return "<svg height=""20"" viewBox=""0 0 20 20"" width=""20""><g><path d=""M 3 0 L 3 18 L 0 15 L 0 16.5 L 3.5 20 L 7 16.5 L 7 15 L 4 18 L 4 0 L 3 0 z M 7 2 L 7 3 L 20 3 L 20 2 L 7 2 z M 7 6 L 7 7 L 15 7 L 15 6 L 7 6 z M 7 10 L 7 11 L 11 11 L 11 10 L 7 10 z ""/></g></svg>"

            Case TipoIcono.Copiar
                Return "<svg height=""48"" viewBox=""0 0 48 48"" width=""48""><path d=""M0 0h48v48h-48z"" fill=""none""/><path d=""M32 2h-24c-2.21 0-4 1.79-4 4v28h4v-28h24v-4zm6 8h-22c-2.21 0-4 1.79-4 4v28c0 2.21 1.79 4 4 4h22c2.21 0 4-1.79 4-4v-28c0-2.21-1.79-4-4-4zm0 32h-22v-28h22v28z""/></svg>"
            Case Else
                Return "<svg height=""20px"" version=""1.1"" viewBox=""0 0 14 20"" width=""14px"" ><g fill=""none"" fill-rule=""evenodd"" id=""Icons"" stroke=""none"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""1""><g id=""Group"" stroke=""#000000"" stroke-width=""2"" transform=""translate(-5.000000, -2.000000)""><g id=""Shape"" transform=""translate(6.000000, 3.000000)""><circle cx=""9.00008312"" cy=""8.99991689"" r=""2.99999847""/><path d=""M6.00008774,5.99991842 L6.00008774,0 L3.00008774,0 C1.34323278,0 8.77387263e-05,1.34314508 8.77387263e-05,3 C8.77387263e-05,4.65685186 1.34323278,6 3.00008774,6 L6.00008774,5.99991842 Z""/><path d=""M6.00008769,11.9998368 L6.00008769,5.99991842 L3.00008769,5.99991842 C1.34323276,5.99991842 8.76940228e-05,7.34306349 8.76940228e-05,8.99991842 C8.76940228e-05,10.6567703 1.34323276,11.9999184 3.00008769,11.9999184 L6.00008769,11.9998368 Z""/><path d=""M6.00008464,5.99991842 L6.00008464,0 L9.00008464,0 C10.6569365,0 12.0000846,1.34314508 12.0000846,3 C12.0000846,4.65685186 10.6569365,6 9.00008464,6 L6.00008464,5.99991842 Z""/><path d=""M6.00008464,11.9998368 L6.00008464,14.9998741 C6.00008464,16.2132639 5.26918472,17.3071844 4.14816339,17.7715396 C3.02714207,18.2358949 1.73678273,17.9792376 0.878780942,17.1212471 C0.0207791575,16.2632567 -0.235895256,14.9729008 0.228445182,13.8518733 C0.692785619,12.7308458 1.78669644,11.9999154 3.00008617,11.9999154 L6.00008464,11.9998368 Z""/></g></g></g></svg>"
        End Select
    End Function


End Class