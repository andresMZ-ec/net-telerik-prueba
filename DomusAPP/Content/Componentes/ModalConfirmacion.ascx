<%@ Control Language="vb" ClassName="ModalConfirmacion" AutoEventWireup="false" CodeBehind="ModalConfirmacion.ascx.vb" Inherits="DomusAPP.ModalConfirmacion" %>

<telerik:RadWindow ID="rwConfirmacion" runat="server" Width="400px" Height="200px"
    Modal="true" VisibleStatusbar="false" Behaviors="Close" Title="Confirmar acción">
    <ContentTemplate>
        <div style="text-align:center; padding:20px;">
            <asp:Label ID="lblMensaje" runat="server" Text="¿Está seguro?"></asp:Label>
            <br /><br />
            <asp:Button ID="btnAceptar" runat="server" Text="Confirmar" OnClientClick="confirmarAccion(); return false;" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="cerrarConfirmacion(); return false;" />
        </div>
    </ContentTemplate>
</telerik:RadWindow>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

<script type="text/javascript">
    var botonPostback = null;

    function mostrarConfirmacion(boton, mensaje) {
        botonPostback = boton;
        $find("<%= rwConfirmacion.ClientID %>").show();
        document.getElementById("<%= lblMensaje.ClientID %>").innerText = mensaje;
        return false; // Cancela postback
    }

    function confirmarAccion() {
        $find("<%= rwConfirmacion.ClientID %>").close();
        if (botonPostback) {
            __doPostBack(botonPostback.name, '');
        }
    }

    function cerrarConfirmacion() {
        $find("<%= rwConfirmacion.ClientID %>").close();
    }
</script>
