Imports System.IO
Imports Microsoft.SqlServer
Imports SelectPdf


Public Class ReportePrueba

    Private Shared Function ImagenABase64(ruta As String) As String
        If File.Exists(ruta) Then
            Dim imageArray As Byte() = File.ReadAllBytes(ruta)
            Return Convert.ToBase64String(imageArray)
        End If
        Return ""
    End Function

    Public Shared Sub ReporteAnalisis(server As HttpServerUtility, response As HttpResponse)
        Dim rutaPlantilla As String = server.MapPath("~/Templates/Prueba.html")
        Dim htmlContent As String = File.ReadAllText(rutaPlantilla)

        Dim rutaLogo As String = server.MapPath("~/images/icon-192x192.png")
        Dim base64Logo As String = ImagenABase64(rutaLogo)
        htmlContent = htmlContent.Replace("@LOGO_URL@", $"data:image/png;base64,{base64Logo}")
        htmlContent = htmlContent.Replace("@FECHA@", DateTime.Now.ToString("dd/MM/yyyy"))
        htmlContent = htmlContent.Replace("@CLIENTE@", "Constructora XYZ S.A.")
        htmlContent = htmlContent.Replace("@DESCRIPCION_GENERAL@", "Este reporte detalla los hallazgos en la zona norte...")

        ' 3. Construir la Tabla Dinámica (HTML)
        Dim sbTabla As New StringBuilder()
        ' Aquí recorrerías tu DataTable o Lista de objetos
        For i As Integer = 1 To 5
            sbTabla.Append("<tr>")
            sbTabla.Append($"<td>Elemento {i}</td>")
            sbTabla.Append($"<td>{i * 100} unidades</td>")
            sbTabla.Append("<td>Aprobado</td>")
            sbTabla.Append("</tr>")
        Next
        htmlContent = htmlContent.Replace("@FILAS_TABLA@", sbTabla.ToString())

        Dim sbGaleria As New StringBuilder()
        ' Supongamos que tienes una lista de rutas de imágenes
        Dim listaImagenes As New List(Of String) From {
            "~/images/icon-192x192.png", "~/images/icon-512x512.png",
            "~/images/icon-192x192.png", "~/images/icon-512x512.png"
        }

        For Each imgPath In listaImagenes
            ' Convertir imagen a Base64 es recomendable para evitar problemas de rutas en el PDF
            Dim imgFisica As String = server.MapPath(imgPath)
            Dim base64Img As String = ImagenABase64(imgFisica)

            sbGaleria.Append("<div class='foto-item'>")
            sbGaleria.Append($"<img src='data:image/png;base64,{base64Img}' />")
            sbGaleria.Append("<div class='foto-desc'>Evidencia fotográfica</div>")
            sbGaleria.Append("</div>")
        Next
        htmlContent = htmlContent.Replace("@IMAGENES_GALERIA@", sbGaleria.ToString())

        ' 5. Convertir a PDF usando SelectPdf
        Dim converter As New HtmlToPdf()

        ' Opciones de configuración (A4, Márgenes, etc.)
        converter.Options.PdfPageSize = PdfPageSize.A4
        converter.Options.MarginTop = 20
        converter.Options.MarginBottom = 20

        Dim doc As PdfDocument = converter.ConvertHtmlString(htmlContent)

        Dim stream As New MemoryStream()
        doc.Save(stream)
        doc.Close() ' Cerramos el documento de SelectPdf

        ' Convertir el stream a un arreglo de bytes
        Dim bytes As Byte() = stream.ToArray()
        stream.Close()

        ' 6. Forzar la descarga manualmente (Sin depender de la librería)
        response.Clear()
        response.ContentType = "application/pdf"

        ' "attachment" fuerza la descarga. Si quieres verlo en el navegador usa "inline"
        response.AddHeader("content-disposition", "attachment;filename=" & "Reporte_" & DateTime.Now.ToString("yyyyMMdd") & ".pdf")

        response.Buffer = True
        response.Cache.SetCacheability(HttpCacheability.NoCache)
        response.BinaryWrite(bytes)

        ' Finalizar la respuesta de forma segura para evitar ThreadAbortException
        response.Flush()
        response.SuppressContent = True
        HttpContext.Current.ApplicationInstance.CompleteRequest()
    End Sub

End Class
