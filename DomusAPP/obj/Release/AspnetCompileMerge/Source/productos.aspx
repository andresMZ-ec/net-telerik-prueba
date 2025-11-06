<%@ Page Title="Productos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="productos.aspx.vb" Inherits="DomusAPP.productos" %>

<%@ Register Src="~/Content/Componentes/FiltroGrid.ascx" TagPrefix="uc" TagName="Filtro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <uc:Filtro ID="filtroGrid" runat="server" />

    <telerik:RadGrid
        ID="rgPrueba"
        runat="server"
        AutoGenerateColumns="false"
        OnNeedDataSource="rgPrueba_NeedDataSource">
        <MasterTableView DataKeyNames="id" EditMode="InPlace">
            <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="N°" />
                <telerik:GridBoundColumn DataField="nombre" HeaderText="Nombre" />
                <telerik:GridBoundColumn DataField="precio" HeaderText="Precio" />
                <telerik:GridBoundColumn DataField="fecha_creacion" HeaderText="Fecha de Creación" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
        

</asp:Content>
