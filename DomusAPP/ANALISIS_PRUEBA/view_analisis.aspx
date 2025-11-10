<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="view_analisis.aspx.vb" Inherits="DomusAPP.view_analisis" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc" TagName="Prueba" Src="~/ANALISIS_PRUEBA/UserControl/uc_prueba.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc:Prueba runat="server" />
</asp:Content>
