<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lista.aspx.vb" Inherits="Lista" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Usuários</title>
    <style>
        body { font-family: Arial, sans-serif; background:#f4f4f4; padding:30px; }
        h1 { color:#333; text-align:center; }
        .tabela { width:100%; border-collapse:collapse; background:#fff; box-shadow:0 2px 10px rgba(0,0,0,0.1); margin-top:20px; }
        .tabela th, .tabela td { border:1px solid #ddd; padding:8px; text-align:left; }
        .tabela th { background:#007bff; color:#fff; }
        .btn-editar, .btn-excluir, .btn-exportar { padding:5px 10px; border:none; border-radius:4px; cursor:pointer; }
        .btn-editar { background:#28a745; color:white; }
        .btn-excluir { background:#dc3545; color:white; }
        .btn-exportar { background:#ffc107; color:white; margin-bottom:10px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Lista de Usuários</h1>
        <asp:Button ID="btnExportarExcel" runat="server" Text="Exportar para Excel" CssClass="btn-exportar" OnClick="btnExportarExcel_Click" />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            CssClass="tabela" OnRowEditing="GridView1_RowEditing"
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowUpdating="GridView1_RowUpdating"
            OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="Email" HeaderText="E-mail" />
                <asp:BoundField DataField="Telefone" HeaderText="Telefone" />
                <asp:BoundField DataField="Endereco" HeaderText="Endereço" />
                <asp:TemplateField HeaderText="Senha">
                    <ItemTemplate>****</ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSenha" runat="server" Text='<%# Bind("Senha") %>' TextMode="Password" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" EditText="Editar" CancelText="Cancelar" UpdateText="Salvar" ButtonType="Button" ControlStyle-CssClass="btn-editar" />
                <asp:TemplateField HeaderText="Excluir">
                    <ItemTemplate>
                        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CommandName="ExcluirUsuario" CommandArgument='<%# Eval("Id") %>' CssClass="btn-excluir" OnClientClick="return confirm('Tem certeza que deseja excluir este usuário?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
