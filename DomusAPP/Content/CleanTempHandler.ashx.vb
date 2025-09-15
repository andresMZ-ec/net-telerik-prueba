Imports System.IO
Imports System.Web
Imports System.Web.Services
Imports Telerik.Web.UI.Widgets

Public Class CleanTempHandler
    Implements IHttpHandler, IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim Guid As String = context.Request.Form("Guid")
        Dim SESSION_NAME = "UploadedFiles" & Guid

        Dim files As List(Of ArchivoUpload) = TryCast(context.Session(SESSION_NAME), List(Of ArchivoUpload))

        If files IsNot Nothing Then
            For Each archivo As ArchivoUpload In files
                Try
                    If File.Exists(archivo.RutaTemporal) Then
                        File.Delete(archivo.RutaTemporal)
                    End If
                Catch
                    ' Ignorar errores de IO
                End Try
            Next
        End If

        context.Session(SESSION_NAME) = Nothing

        context.Response.ContentType = "application/json"
        context.Response.Write("{""status"":""ok""}")
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class