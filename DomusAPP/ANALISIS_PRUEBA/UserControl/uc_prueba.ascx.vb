Imports System.Web.Services
Imports System.Web.Script.Services

Public Class uc_prueba
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property _ListaBalanzas As List(Of BalanzaCN)
        Get
            Return New List(Of BalanzaCN) From {
                New BalanzaCN With {
                    .Id = 1,
                    .Nombre = "Balanza IP 1",
                    .IP = "192.168.1.2",
                    .Puerto = 3526
                },
                New BalanzaCN With {
                    .Id = 2,
                    .Nombre = "Balanza IP 2",
                    .IP = "192.168.1.3",
                    .Puerto = 3524
                }
            }
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarBalanzas()
        End If
    End Sub

    Private Sub CargarBalanzas()
        With cboxSelectBalanza
            .ClearSelection()
            .Items.Clear()
            .DataValueField = NameOf(BalanzaCN.Id)
            .DataTextField = NameOf(BalanzaCN.Nombre)
            .DataSource = _ListaBalanzas
            .DataBind()
            .SelectedIndex = -1
        End With
    End Sub

    Private Sub EmpezarLecturaBalanza(IP As String, puerto As Integer)
        Dim scriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)

        If scriptManager IsNot Nothing AndAlso scriptManager.IsInAsyncPostBack Then
            ' Usar ScriptManager para postbacks asíncronos (dentro de UpdatePanel)
            Dim script = $"GetBalanceWeight('{tboxPeso.ClientID}', '{IP}', {puerto}, {1500});"
            ScriptManager.RegisterStartupScript(
        Me.Page,
        Me.GetType(),
        "BalanceCaptureKey",
        script,
        True
    )
        Else
            ' Usar Page.ClientScript para postbacks normales o carga inicial
            Dim script = $"GetBalanceWeight('{tboxPeso.ClientID}', '{IP}', {puerto});"
            Page.ClientScript.RegisterStartupScript(
        Me.GetType(),
        "BalanceCaptureKey",
        script,
        True
    )
        End If
    End Sub

    Protected Sub cboxSelectBalanza_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)
        Dim idBalanza = Convert.ToInt64(e.Value)

        If idBalanza > 0 Then
            Dim balanza = _ListaBalanzas.Find(Function(x) x.Id = idBalanza)

            If balanza Is Nothing Then Exit Sub

            EmpezarLecturaBalanza(
                balanza.IP,
                balanza.Puerto
            )
        End If
    End Sub
End Class

Public Class BalanzaCN
    Public Property Id As Int64
    Public Property Nombre As String
    Public Property IP As String
    Public Property Puerto As Integer
End Class