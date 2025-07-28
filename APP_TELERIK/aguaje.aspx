<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="aguaje.aspx.vb" Inherits="APP_TELERIK.aguaje" %>

<%@ Register Src="~/add_grid_aguaje.ascx" TagPrefix="uc" TagName="AddAguaje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="AguajeTimeControl">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="AguajeTimeControl" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadWindow ID="rwModalAggAguaje" runat="server" Modal="true" />

    <telerik:RadPageLayout ID="rpLayoutMain" runat="server">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn>
                        <h2>Gestión de Aguajes</h2>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn>
                        <telerik:RadButton ID="btnAggAguaje" runat="server" Skin="Bootstrap" Primary="true" />
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                <Content>
                    <div>dsds</div>
                    <uc:AddAguaje ID="AddAguaje" runat="server" />
                </Content>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</asp:Content>
