<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PruebaUpload.aspx.vb" Inherits="DomusAPP.PruebaUpload" %>

<%@ Register Src="~/Content/Upload.ascx" TagPrefix="custom" TagName="Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                <Scripts>
                    <asp:ScriptReference Path="~/Scripts/Upload.js" />
                </Scripts>
            </asp:ScriptManager>

            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <custom:Upload ID="up1" runat="server" />
                    <asp:Button ID="btnVer" runat="server" Text="ver" OnClick="btnVer_Click" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnVer" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>
