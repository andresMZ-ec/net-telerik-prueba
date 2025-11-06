<%@ Control Language="vb" ClassName="Notificacion" AutoEventWireup="false" CodeBehind="Notificacion.ascx.vb" Inherits="DomusAPP.Notificacion" %>

<telerik:RadAjaxManager runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rWindowGlobal">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnConfirmar" />
                <telerik:AjaxUpdatedControl ControlID="btnCancelar" />
            </UpdatedControls> 
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<telerik:RadNotification ID="RadNotificacion" runat="server"
    AutoCloseDelay="7000"
    Animation="Slide"
    AnimationDuration="500"
    EnableEmbeddedSkins="false"
    Position="BottomRight"
    ShowCloseButton="true"
    ShowIcon="true"
    TitleIcon="none"
    Width="300px">
    <ContentTemplate>
        <div style="padding: 10px;">
            <asp:Label ID="lblNotificacion" runat="server" Text=""></asp:Label>
        </div>
    </ContentTemplate>
</telerik:RadNotification>

<telerik:RadWindowManager ID="rWindowManagerGlobal" runat="server">
    <Windows>
        <telerik:RadWindow
            ID="rWindowGlobal"
            runat="server"
            VisibleOnPageLoad="false"
            KeepInScreenBounds="true"
            Behaviors="Close"
            CssClass="rwConfirmacion"
            ShowContentDuringLoad="false"
            Skin="Bootstrap">
            <ContentTemplate>
                <div style="padding: 15px; text-align: center; font-size: 14px;">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="true" /><br />
                    <br />
                    <asp:Label ID="lblMensaje" runat="server" /><br />
                    <br />
                    <telerik:RadButton ID="btnConfirmar" runat="server" Text="Confirmar" AutoPostBack="true" OnClick="btnConfirmar_Click" />
                    <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" AutoPostBack="true" OnClick="btnCancelar_Click" />
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
