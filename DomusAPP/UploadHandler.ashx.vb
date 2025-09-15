Imports System.IO
Imports System.Web
Imports System.Web.Services
Imports DomusAPP.Upload

Public Class UploadHandler
    Implements IHttpHandler, IRequiresSessionState

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim uploadId As String = context.Request.Form("uploadID")
        Dim SESSION_NAME As String = "UploadedFiles" & uploadId

        context.Response.ContentType = "text/plain"

        Dim tempPath As String = context.Server.MapPath("~/TempUploads/")
        If Not Directory.Exists(tempPath) Then
            Directory.CreateDirectory(tempPath)
        End If

        For Each key As String In context.Request.Files
            Dim file As HttpPostedFile = context.Request.Files(key)
            Dim savePath As String = Path.Combine(tempPath, Path.GetFileName(file.FileName))
            file.SaveAs(savePath)

            'Almacenar items en una session
            Dim files As List(Of ArchivoUpload) = TryCast(context.Session(SESSION_NAME), List(Of ArchivoUpload))

            If files Is Nothing Then
                files = New List(Of ArchivoUpload)
            End If

            files.Add(New ArchivoUpload With {
                .Nombre = file.FileName,
                .Tamaño = file.ContentLength,
                .Tipo = file.ContentType,
                .RutaTemporal = savePath,
                .FechaSubida = DateTime.Now
            })

            context.Session(SESSION_NAME) = files

            context.Response.ContentType = "application/json"
            context.Response.Write("{""status"":""ok""}")
        Next

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class