<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Upload.ascx.vb" Inherits="DomusAPP.Upload" %>

<asp:UpdatePanel ID="upUpload" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <div class="FileUploadItems">
            <asp:Button
                ID="btnFileUpload"
                runat="server"
                Text="Seleccionar Archivo"
                CausesValidation="false" />

            <input type="file" id="fileInputUpload" runat="server" multiple />

            <asp:Button ID="btnProcessFiles" runat="server" Text="Procesar"
                OnClick="btnProcessFiles_Click" />

            <asp:Repeater ID="rptArchivos" runat="server">
                <ItemTemplate>
                    <div class="archivo-item">
                        <%# Eval("Nombre") %> - <%# Eval("Tamaño") %> bytes
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnProcessFiles" EventName="click" />
    </Triggers>
</asp:UpdatePanel>

