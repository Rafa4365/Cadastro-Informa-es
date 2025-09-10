# Cadastro-Informa-es
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Cadastro.aspx.vb" Inherits="Cadastro" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Usuário</title>
    <style>
        body, div, form { margin:0; padding:0; font-family: Arial, sans-serif; }
        body { background-color:#f4f4f4; display:flex; justify-content:center; align-items:center; height:100vh; }
        .container { background-color:#fff; padding:20px; border-radius:8px; box-shadow:0 2px 10px rgba(0,0,0,0.1); width:300px; text-align:center; }
        h1 { color:#333; }
        form div { margin-bottom:15px; text-align:left; }
        label { display:block; font-size:14px; margin-bottom:5px; color:#666; }
        input { width:100%; padding:8px; font-size:14px; border:1px solid #ccc; border-radius:4px; }
        button { width:100%; padding:10px; background-color:#007bff; color:white; border:none; border-radius:4px; cursor:pointer; }
        button:hover { background-color:#0056b3; }
        #mensagem { margin-top:10px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Cadastro de Usuário</h1>
            <div>
                <label for="txtNome">Nome:</label>
                <input type="text" id="txtNome" runat="server" />
            </div>
            <div>
                <label for="txtEmail">E-mail:</label>
                <input type="email" id="txtEmail" runat="server" />
            </div>
            <div>
                <label for="txtTelefone">Telefone:</label>
                <input type="text" id="txtTelefone" runat="server" maxlength="15" oninput="mascaraTelefone(this)" />
            </div>
            <div>
                <label for="txtEndereco">Endereço:</label>
                <input type="text" id="txtEndereco" runat="server" />
            </div>
            <div>
                <label for="txtSenha">Senha:</label>
                <input type="password" id="txtSenha" runat="server" />
            </div>
            <button id="btnCadastrar" runat="server" onserverclick="btnCadastrar_Click">Cadastrar</button>
            <div id="mensagem" runat="server"></div>
        </div>
    </form>
</body>
</html>
    <head runat="server">
        <style>
        body { background:#f4f4f4; font-family: Arial, sans-serif; padding:30px; }
        .tabela { width:100%; border-collapse:collapse; background:#fff; box-shadow:0 2px 10px rgba(0,0,0,0.1); }
        .tabela th, .tabela td { border:1px solid #ddd; padding:8px; text-align:left; }
        .tabela th { background:#007bff; color:#fff; }
        .btn-editar, .btn-excluir { padding:5px 10px; border:none; border-radius:4px; cursor:pointer; }
        .btn-editar { background:#28a745; color:white; }
        .btn-excluir { background:#dc3545; color:white; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Lista de Usuários</h1>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            CssClass="tabela" OnRowEditing="GridView1_RowEditing"
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowUpdating="GridView1_RowUpdating"
            OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                <asp:BoundField DataField="Email" HeaderText="E-mail" />
                <asp:BoundField DataField="Endereco" HeaderText="Endereço" />
                <asp:TemplateField HeaderText="Telefone">
                    <ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTelefone" runat="server" Text='<%# Eval("Telefone") %>' MaxLength="15" />
                    </EditItemTemplate>
                </asp:TemplateField>
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



