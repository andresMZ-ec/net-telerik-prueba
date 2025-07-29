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
                    <telerik:RadGrid ID="rgAguajes" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                        OnNeedDataSource="rgAguajes_NeedDataSource" OnUpdateCommand="rgAguajes_UpdateCommand"
                        OnItemCommand="rgAguajes_ItemCommand" OnDeleteCommand="rgAguajes_DeleteCommand"
                        OnItemDataBound="rgAguajes_ItemDataBound"
                        Skin="Material" ShowFooter="false" AllowFilteringByColumn="false" AllowSorting="false"
                        AllowAutomaticUpdates="false" AllowAutomaticDeletes="false" AllowAutomaticInserts="false"
                        AllowGrouping="True">
                        <%-- Habilitar agrupación --%>

                        <MasterTableView EditMode="InPlace" DataKeyNames="Id">
                            <GroupByExpressions>
                                <%-- Agrupar por la propiedad 'MesNombre' --%>
                                <telerik:GridGroupByExpression>
                                    <SelectFields>
                                        <telerik:GridGroupByField FieldName="MesNombre" HeaderText="Mes" SortOrder="Ascending" />
                                    </SelectFields>
                                    <GroupByFields>
                                        <telerik:GridGroupByField FieldName="MesNumero" SortOrder="Ascending" />
                                        <%-- Usamos MesNumero para ordenar correctamente --%>
                                    </GroupByFields>
                                </telerik:GridGroupByExpression>
                            </GroupByExpressions>

                            <GroupHeaderTemplate>
                                <%-- Plantilla para el encabezado de cada grupo (mes) --%>
                                <div class="group-header-content">
                                    <span style="font-size: 1.2em; font-weight: bold;"><%# Eval("MesNombre") %></span>
                                    <telerik:RadButton ID="btnAddRowForMonth" runat="server" Text="Añadir Aguaje"
                                        CommandName="AddRowForMonth" CommandArgument='<%# Eval("MesNumero") %>' AutoPostBack="true"
                                        CssClass="add-row-button" Skin="Material" />
                                </div>
                            </GroupHeaderTemplate>

                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Fecha Inicio" UniqueName="FechaInicioColumn" DataField="FechaInicio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaInicio" runat="server" Text='<%# Eval("FechaInicio", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="rdpFechaInicioEdit" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                                            SelectedDate='<%# Bind("FechaInicio") %>'>
                                            <Calendar runat="server" />
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderText="Fecha Fin" UniqueName="FechaFinColumn" DataField="FechaFin">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFechaFin" runat="server" Text='<%# Eval("FechaFin", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadDatePicker ID="rdpFechaFinEdit" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                                            SelectedDate='<%# Bind("FechaFin") %>'>
                                            <Calendar runat="server" />
                                        </telerik:RadDatePicker>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn HeaderText="Tipo Aguaje" UniqueName="TipoAguajeColumn" DataField="TipoAguaje">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoAguaje" runat="server" Text='<%# Eval("TipoAguaje") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="rcbTipoAguajeEdit" runat="server" DataTextField="Text" DataValueField="Value"
                                            SelectedValue='<%# Bind("TipoAguaje") %>' AppendDataBoundItems="true" OnDataBound="rcbTipoAguajeEdit_DataBound">
                                            <%-- Los items se cargarán en code-behind --%>
                                        </telerik:RadComboBox>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridButtonColumn CommandName="Edit" Text="Editar" UniqueName="EditColumn" HeaderStyle-Width="70px"></telerik:GridButtonColumn>
                                <telerik:GridButtonColumn CommandName="Delete" Text="Eliminar" UniqueName="DeleteColumn" HeaderStyle-Width="70px" ConfirmText="¿Está seguro de que desea eliminar este registro?"></telerik:GridButtonColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </Content>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</telerik:RadAjaxPanel>
