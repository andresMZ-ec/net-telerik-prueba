<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Precios.aspx.vb" Inherits="DomusAPP.Precios" %>

<%@ Register TagPrefix="custom" Namespace="DomusAPP.Content.Componentes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function toggleGridActionsMenu(id) {
            var menu = document.getElementById(id);
            if (menu.style.display === "none") {
                menu.style.display = "block";
            } else {
                menu.style.display = "none";
            }
        }
    </script>


    <asp:Button ID="btnTest" runat="server" Text="Mostrar Confirmación" OnClick="btnTest_Click" />


    <custom:GridActions runat="server">
        <items>
            <custom:ItemButton runat="server" Texto="Imprimir" CssClass="btn-sm" />
            <custom:ItemButton runat="server" Texto="Habilitar" CssClass="btn-sm" />
        </items>
    </custom:GridActions>


    <div>
    </div>

    <%--<telerik:RadGrid
        ID="RadGrid1"
        runat="server"
        AutoGenerateColumns="false">
        <mastertableview datakeynames="id">
            <columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="text" />
            </columns>
        </mastertableview>
    </telerik:RadGrid>--%>


    <style>
        .btn {
            padding: 10px 20px;
            margin: 5px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
        }

        .btn-danger {
            background-color: #dc3545;
            color: white;
        }

        .btn-success {
            background-color: #28a745;
            color: white;
        }

        .btn-warning {
            background-color: #ffc107;
            color: black;
        }

        .grid-actions-btn {
            cursor: pointer;
            background-color: #f0f0f0;
            border: 1px solid #ccc;
            padding: 4px 8px;
            border-radius: 4px;
        }

        .grid-actions-menu {
            position: absolute;
            background: white;
            border: 1px solid #ccc;
            padding: 6px;
            z-index: 1000;
        }
    </style>

</asp:Content>
