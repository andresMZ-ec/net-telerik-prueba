Imports System.ComponentModel
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ScriptService()>
<ToolboxItem(False)>
Public Class Balanza
    Inherits WebService

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function ObtenerPesoBalanza() As ResultadoBalanza
        Try
            Dim peso As Decimal = ObtenerPesoDesdeBalanza()

            Return New ResultadoBalanza With {
                .Status = 200,
                .Peso = peso,
                .FechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            }
        Catch ex As Exception
            Return New ResultadoBalanza With {
                .Status = False,
                .Mensaje = ex.Message
            }
        End Try
    End Function

    Private Function ObtenerPesoDesdeBalanza() As Decimal
        ' Implementa aquí la conexión con tu balanza
        ' Por ejemplo: puerto serial, API REST, etc.

        ' Simulación para el ejemplo:
        Dim rnd As New Random()
        Return Math.Round(CDec(rnd.NextDouble() * 100), 2)
    End Function

End Class

Public Class ResultadoBalanza
    Public Property Status As Boolean
    Public Property Peso As Decimal
    Public Property FechaHora As String
    Public Property Mensaje As String
End Class