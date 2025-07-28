<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="add_grid_aguaje.ascx.vb" Inherits="APP_TELERIK.add_grid_aguaje" %>
<%@ Register Assembly="Telerik.WEB.UI" Namespace="Telerik.WEB.UI" TagPrefix="telerik" %>

<style>
    /* Estilos básicos para el User Control */
    .month-container {
        border: 1px solid #ddd;
        border-radius: 5px;
        margin-bottom: 20px;
        padding: 15px;
        background-color: #f9f9f9;
    }

    .month-header {
        background-color: #e9ecef;
        padding: 10px 15px;
        margin: -15px -15px 15px -15px; /* Ajustar para que ocupe todo el ancho */
        border-bottom: 1px solid #ddd;
        border-top-left-radius: 5px;
        border-top-right-radius: 5px;
        display: flex;
        justify-content: space-between;
        align-items: center;
    }

    .month-title {
        font-size: 1.2em;
        font-weight: bold;
        color: #333;
    }

    .telerik-grid-container {
        margin-top: 10px;
    }
</style>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Material" />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
    <telerik:RadPageLayout ID="rpLayout" runat="server">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn Span="10">
                        <h2>Registro de Aguajes</h2>
                        <p>Ingresa los meses según el año de operación</p>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="2">
                        <telerik:RadComboBox ID="cboxSelectPeriodo" runat="server" Label="Periodo:">
                        </telerik:RadComboBox>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                <Content>
                    <asp:Repeater ID="rpMeses" runat="server">
                        <ItemTemplate>
                            <div class="month-container">
                                <div class="month-header">
                                    <span class="month-title"><%# Container.DataItem %></span> <%-- Nombre del mes --%>
                                    <telerik:RadButton
                                        ID="btnAddRow"
                                        runat="server"
                                        Text="Añadir Aguaje"
                                        Skin="Material"
                                        AutoPostBack="true"
                                        CommandName="AddRow"
                                        CommandArgument="<%# Container.DataItem %>"
                                        OnCommand="btnAddRow_Command" />
                                </div>
                            </div>
                            <div class="telerik-grid-container">
                                <telerik:RadGrid
                                    ID="rgAguajes"
                                    runat="server"
                                    AllowPaging="false"
                                    AllowSorting="false"
                                    AutoGenerateColumns="false"
                                    AllowFilteringByColumn="false"
                                    OnNeedDataSource="rgAguajes_NeedDataSource"
                                    OnUpdateCommand="rgAguajes_UpdateCommand"
                                    OnItemCommand="rgAguajes_ItemCommand"
                                    Skin="Material"
                                    ShowFooter="false">
                                    <MasterTableView EditMode="InPlace" DataKeyNames="id">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Fecha Inicio" UniqueName="FechaInicio">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFechaInicio" runat="server" Text="<%# Eval("fecha_inicio", "{0:dd/MM/yyyy}") %>" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadDatePicker ID="rdpFechaInicio" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                                                        SelectedDate='<%# Bind("fecha_inicio") %>'>
                                                        <Calendar runat="server" />
                                                    </telerik:RadDatePicker>
                                                </EditItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridButtonColumn CommandName="Edit" Text="Editar" UniqueName="EditColumn" HeaderStyle-Width="70px"></telerik:GridButtonColumn>
                                            <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" HeaderStyle-Width="70px" ConfirmText="¿Está seguro de que desea eliminar este registro?"></telerik:GridButtonColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </Content>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</telerik:RadAjaxPanel>
