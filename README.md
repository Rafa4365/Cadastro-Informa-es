<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Cadastro.aspx.vb" Inherits="Cadastro" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Usuário</title>
    <style>
        body, div, form { margin:0; padding:0; font-family: Arial, sans-serif; }
        body { background-color:#f4f4f4; display:flex; justify-content:center; align-items:center; height:100vh; }
        .container { background-color:#fff; padding:20px; border-radius:8px; box-shadow:0 2px 10px rgba(0,0,0,0.1); width:350px; text-align:center; }
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
            <asp:Button ID="btnVerCadastros" runat="server" Text="Ver Cadastros" PostBackUrl="~/Lista.aspx" />
        </div>
    </form>

    <script type="text/javascript">
        function mascaraTelefone(input) {
            let valor = input.value.replace(/\D/g, '');
            if (valor.length > 11) valor = valor.substring(0, 11);
            if (valor.length <= 10) {
                input.value = valor.replace(/(\d{2})(\d{4})(\d{0,4})/, "($1) $2-$3");
            } else {
                input.value = valor.replace(/(\d{2})(\d{5})(\d{0,4})/, "($1) $2-$3");
            }
        }
    </script>
</body>
</html>
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Partial Class Cadastro
    Inherits System.Web.UI.Page
    Private connString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=CadastroDB;Integrated Security=True"
    ' Botão Cadastrar
    Protected Sub btnCadastrar_Click(sender As Object, e As EventArgs)
        Using conn As New SqlConnection(connString)
            Dim query As String = "INSERT INTO Usuarios (Nome, Email, Telefone, Endereco, Senha) VALUES (@Nome, @Email, @Telefone, @Endereco, @Senha)"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Nome", txtNome.Value)
            cmd.Parameters.AddWithValue("@Email", txtEmail.Value)
            cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Value)
            cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Value)
            cmd.Parameters.AddWithValue("@Senha", txtSenha.Value)
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
        Response.Redirect("Lista.aspx") ' Redireciona para a lista após cadastrar
    End Sub
End Class
