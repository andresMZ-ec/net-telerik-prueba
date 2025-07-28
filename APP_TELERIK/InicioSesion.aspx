<%@ Page Title="Inicio de Sesión" Language="VB" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="~/InicioSesion.aspx.vb" Inherits="APP_TELERIK.InicioSesion" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Iniciar Sesión</h3>
        </div>
        <div>
            <fieldset>
                <div class="rdfRow">
                    <telerik:RadLabel ID="lblTxtCorreo" runat="server" Text="Correo Electrónico" AssociatedControlID="txtCorreo" CssClass="mb-3">
                        <telerik:RadTextBox ID="txtCorreo" runat="server" Width="100%" />
                        <div id="errorTxtCorreo" runat="server" />
                    </telerik:RadLabel>
                </div>
            </fieldset>
            <telerik:RadLabel ID="lblContrasema" runat="server" Text="Contraseña" AssociatedControlID="txtContrasena" CssClass="mb-3">
                <telerik:RadTextBox ID="txtContrasena" runat="server" Width="100%" />
                <div id="errorTxtContrasena" runat="server" />
            </telerik:RadLabel>
            <div class="form-group">
                <asp:Literal ID="LiteralLoginMensaje" runat="server"></asp:Literal>
            </div>
            <p><a href="~/ResetPassword.aspx">¿Olvidaste tu contraseña?</a></p>
        </div>
    </div>
</asp:Content>
