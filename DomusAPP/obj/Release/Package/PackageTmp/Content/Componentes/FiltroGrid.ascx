<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="FiltroGrid.ascx.vb" Inherits="DomusAPP.FiltroGrid" %>

<%--<asp:PlaceHolder ID="phPlantillaItem" runat="server" Visible="false">
    <div class="FiltroItem">
        <div class="flex">
            <asp:Label ID="lblColumna" runat="server" />
            <span id="btnLimpiarField" runat="server">Limpiar</span>
        </div>
        <div class="Fields">
            <asp:Literal ID="itemLiteral" runat="server" />
        </div>
    </div>
</asp:PlaceHolder>


<asp:PlaceHolder ID="phItemsFechas" runat="server" Visible="false">
    <telerik:RadDatePicker ID="FechaIn" runat="server" />
    <telerik:RadDatePicker ID="FechaFin" runat="server" />
</asp:PlaceHolder>

<asp:PlaceHolder ID="phItemsLista" runat="server" Visible="false">
    <span id="ItemTag" runat="server" class="Tag"></span>
</asp:PlaceHolder>

<asp:PlaceHolder ID="phItemsPrecio" runat="server" Visible="false">
    <telerik:RadNumericTextBox ID="PrecioMin" runat="server" />
    <telerik:RadNumericTextBox ID="PrecioMax" runat="server" />
</asp:PlaceHolder>--%>

<div class="flex">
    <telerik:RadTextBox ID="BuscadorGlobal" runat="server" EmptyMessage="Buscar por..." />
    <telerik:RadButton ID="btnMostrarFiltro" runat="server">
        <ContentTemplate>
            <span id="contador" runat="server" class="contador" />
            <span>Filtrar</span>
            <span>Icon</span>
        </ContentTemplate>
    </telerik:RadButton>
    <asp:UpdatePanel ID="upFiltro" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:PlaceHolder ID="phFiltro" runat="server">
                <div class="overlay">
                    <div class="modal-filtro" runat="server">
                        <div class="flex">
                            <h2>Búsqueda Avanzada</h2>
                            <span id="CerrarFiltros" runat="server">x</span>
                        </div>
                        <div id="FiltroBody" runat="server" class="FiltroBody">
                            <%-- Aqui se renderizarian N filtros --%>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>


