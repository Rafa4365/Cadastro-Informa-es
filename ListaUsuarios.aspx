<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lista.aspx.vb" Inherits="Lista" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Usuários</title>
    <style>
        body { background:#f4f4f4; font-family: Arial, sans-serif; padding:30px; }
        .tabela { width:100%; border-collapse:collapse; background:#fff; box-shadow:0 2px 10px rgba(0,0,0,0.1); }
        .tabela th, .tabela td { border:1px solid #ddd; padding:8px; text-align:left; }
        .tabela th { background:#007bff; color:#fff; }
        .btn-editar, .btn-excluir { padding:5px 10px; border:none; border-radius:4px; cursor:pointer; }
        .btn-editar { background:#28a745; color:white; }
        .btn-excluir { background:#dc3545; color:white; }
        .btn-export { margin-top:10px; padding:10px 20px; background:#007bff; color:white; border:none; border-radius:4px; cursor:pointer; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Lista de Usuários</h1>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            CssClass="tabela">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="Email" HeaderText="E-mail" />
                <asp:BoundField DataField="Telefone" HeaderText="Telefone" />
                <asp:BoundField DataField="Endereco" HeaderText="Endereço" />
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar para Excel" CssClass="btn-export" OnClick="btnExportarExcel_Click" />
    </form>
</body>
</html>

