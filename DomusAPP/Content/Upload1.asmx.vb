Imports System.Web.Services
Imports System.Web.Script.Services

<ScriptService()>
Public Class Upload1
    Inherits WebService

    <WebMethod(EnableSession:=True)>
    Public Function CargaPrueba() As String
        Return "ok desde asmx"
    End Function
End Class