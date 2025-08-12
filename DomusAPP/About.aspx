<%@ Page Title="About" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.vb" Inherits="DomusAPP.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p>Your app description page.</p>
    <p>Use this area to provide additional information.</p>

    <telerik:RadButton ID="btnPus" runat="server" Text="Push" OnClick="btnPrint_Click" />
    <telerik:RadButton ID="btnPrint" runat="server" AutoPostBack="false" Primary="true" RenderMode="Lightweight" OnClick="btnPrint_Click">
        <ContentTemplate>
            <componentes:Iconos ID="Imprimir" runat="server" Tipo="Imprimir" Size="16" />
            <span>Imprimir</span>
        </ContentTemplate>
    </telerik:RadButton>

    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
        OnClick="btnEliminar_Click"
        OnClientClick="return mostrarConfirmacion(this, '¿Desea eliminar este registro?');" />
</asp:Content>
