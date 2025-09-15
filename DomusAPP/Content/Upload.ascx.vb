Imports System.IO
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Telerik.Web.UI

Public Class Upload
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property Archivos As List(Of ArchivoUpload)
        Get
            Dim SESSION_NAME As String = "UploadedFiles" & fileInputUpload.ClientID
            Dim files As List(Of ArchivoUpload) = TryCast(Session(SESSION_NAME), List(Of ArchivoUpload))

            If files Is Nothing Then
                files = New List(Of ArchivoUpload)
                Session(SESSION_NAME) = files
            End If
            Return files
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.ClientScript.IsStartupScriptRegistered(Me.GetType(), Me.ClientID & "_init") Then
            Dim script As String = $"
            Sys.Application.add_load(function() {{
                InicializarEventosUploadComponente(
                    '{btnFileUpload.ClientID}',
                    '{fileInputUpload.ClientID}',
                    '{btnProcessFiles.UniqueID}'
                );
            }});"

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), Me.ClientID & "_init", script, True)
        End If

        If Not IsPostBack Then
            BindRepeater()
        End If
    End Sub

    <WebMethod()>
    Public Shared Function CargaPrueba() As String
        Return "ok"
    End Function

    Protected Sub btnProcessFiles_Click(sender As Object, e As EventArgs)
        BindRepeater()
    End Sub

    Private Sub BindRepeater()
        rptArchivos.DataSource = Archivos
        rptArchivos.DataBind()
    End Sub

End Class

Public Class ArchivoUpload
    Public Property Nombre As String
    Public Property Tamaño As Long
    Public Property Tipo As String
    Public Property RutaTemporal As String
    Public Property FechaSubida As DateTime
End Class
