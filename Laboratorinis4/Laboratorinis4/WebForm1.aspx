<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Laboratorinis4.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="StyleSheet1.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Error" runat="server"></asp:Label>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Vykdyti" />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Pradiniai duomenys"></asp:Label>
            <asp:Table ID="Table1" runat="server">
            </asp:Table>
        </div>
        <div ID="rand" runat="server">
        </div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Geriausios transporto priemones"></asp:Label>
        </div>
        <div id="rand2" runat="server">
        </div>
        <div>
            <asp:Label ID="Label3" runat="server" Text="Seniausi mikroautobusai šiame filiale"></asp:Label>
            <asp:Table ID="InfoTable" GridLines="Both" runat="server">
            </asp:Table>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Visų filialų sunkvežimiai"></asp:Label>
        </div>
        <div id="rand3" runat="server">
        </div>
        <div>
            <asp:Label ID="Label5" runat="server" Text="Tech. data greitai baigiasi"></asp:Label>
        </div>
        <div id="rand4" runat="server">
        </div>
    </form>
</body>
</html>
