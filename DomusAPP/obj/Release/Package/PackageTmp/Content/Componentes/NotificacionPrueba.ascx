<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NotificacionPrueba.ascx.vb" Inherits="DomusAPP.NotificacionPrueba" %>

<telerik:RadNotification
    ID="RadNotificationGLObal"
    runat="server"
    OnPreRender="RadNotificationGLObal_PreRender"
    OnClientShowing="notificationShowing"
    CssClass="RadNotificationGlobal"
    AutoCloseDelay="500"
    Visible="true"
    Position="TopRight"
    EnableRoundedCorners="false"
    EnableShadow="false"
    Title="Titulo corto"
    Text="Description large">

</telerik:RadNotification>

<script type="text/javascript">
    function notificationShowing(sender, args) {
        console.log("aquo")
        var element = sender.get_element();

        // Limpiar estilos en línea
        element.style.width = "";
        element.style.height = "";
        element.style.top = "";
        element.style.left = "";

        // O ajustar lo que necesites
        element.style.maxWidth = "90vw";
        element.style.maxHeight = "90vh";
    }
</script>