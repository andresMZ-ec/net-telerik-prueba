Imports System.Web.Script.Services
Imports System.Web.Services

Public Class view_analisis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    <WebMethod()>
    <ScriptMethod()>
    Public Shared Function ObtenerPesoBalanza() As Decimal
        Dim rnd As New Random()
        Return Math.Round(CDec(rnd.NextDouble() * 100), 2)
    End Function

End Class