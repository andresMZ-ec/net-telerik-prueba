<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="uc_prueba.ascx.vb" Inherits="DomusAPP.uc_prueba" %>

<div style="padding: 20px;">

    <telerik:RadAjaxPanel runat="server">
        <telerik:RadComboBox
            ID="cboxSelectBalanza"
            runat="server"
            AutoPostBack="true"
            OnSelectedIndexChanged="cboxSelectBalanza_SelectedIndexChanged">
        </telerik:RadComboBox>
    </telerik:RadAjaxPanel>

    <h2>Monitor de Balanza</h2>
    <telerik:RadButton ID="btnSelectBalanza" runat="server" Text="Seleccionar Balanza" />

    <!-- TextBox para mostrar el peso -->
    <telerik:RadTextBox 
        ID="tboxPeso"
        runat="server"
        Label="Peso Actual:"
        ReadOnly="true"
        InputType="Number"
        ToolTip=""
        Width="250px">
    </telerik:RadTextBox>

    <span id="lblUnidad" style="margin-left: 5px; font-weight: bold;">kg</span>
    <span id="lblFechaHora" style="margin-left: 20px; color: #666;"></span>

    <br />
    <br />

    <!-- Indicador de estado -->
    <div id="divEstado" style="padding: 10px; display: inline-block;">
        <span id="lblEstado">Conectando...</span>
        <span id="spanIndicador" style="display: inline-block; width: 10px; height: 10px; border-radius: 50%; background-color: orange; margin-left: 10px;"></span>
    </div>

    <br />
    <br />

    <!-- Otros controles para demostrar que no interfiere -->
    <h3>Otros Controles (no deben verse afectados)</h3>

    <telerik:RadComboBox ID="RadComboBox1" runat="server" Width="250px">
        <Items>
            <telerik:RadComboBoxItem Text="Opción 1" Value="1" />
            <telerik:RadComboBoxItem Text="Opción 2" Value="2" />
            <telerik:RadComboBoxItem Text="Opción 3" Value="3" />
        </Items>
    </telerik:RadComboBox>

    <br />
    <br />

    <button type="button" id="btnTest">Botón de Prueba</button>
    <span id="lblClickCount" style="margin-left: 10px;">Clicks: 0</span>
</div>
