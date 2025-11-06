<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="DialogClaude.ascx.vb" Inherits="DomusAPP.DialogClaude" ViewStateMode="Enabled" %>

<asp:Panel ID="pnlDialog" runat="server" CssClass="modal-container">
    <div class="modal">
        <h3>
            <asp:Label ID="lblTitulo" runat="server" />Hola</h3>
        <p>
            <asp:Label ID="lblDescripcion" runat="server" /></p>

        <div class="modal-actions">
            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn-confirm" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn-cancel" />
            
        </div>
    </div>
</asp:Panel>

<div>
    Hola mundo
</div>
