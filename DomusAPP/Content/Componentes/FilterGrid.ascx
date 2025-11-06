<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="FilterGrid.ascx.vb" Inherits="DomusAPP.FilterGrid" %>

<telerik:RadAjaxPanel runat="server">
    <asp:HiddenField ID="hfGridID" runat="server" />
    <asp:Button ID="btnOpenFilter" runat="server" Text="Filtrar por..." />
    <asp:Repeater ID="rpFilter" runat="server" OnItemCommand="rpFilter_ItemCommand" OnItemDataBound="rpFilter_ItemDataBound">
        <ItemTemplate>
            <div>
                <span>icono</span>
                <span><%# Eval("NombreColumna") %>:</span>
                <span><%# Eval("Valor") %></span>
                <button id="btnDeleteFilter" runat="server">
                    <span>x</span>
                </button>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <div class="OverlayFilter">
        <div class="DrawerFilter">
            <div>
                <h1>Búsqueda Avanzada</h1>
                <span>X</span>
            </div>
            <div class="DrawerFilter Body">

            </div>
        </div>
    </div>

</telerik:RadAjaxPanel>