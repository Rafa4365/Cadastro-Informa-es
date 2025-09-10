Imports System.Data.SqlClient

Partial Class Cadastro
    Inherits System.Web.UI.Page

    Protected Sub btnCadastrar_Click(sender As Object, e As EventArgs)
        Dim nome As String = txtNome.Value.Trim()
        Dim email As String = txtEmail.Value.Trim()
        Dim telefone As String = txtTelefone.Value.Trim()
        Dim endereco As String = txtEndereco.Value.Trim()
        Dim senha As String = txtSenha.Value.Trim()

        If String.IsNullOrEmpty(nome) OrElse String.IsNullOrEmpty(email) OrElse String.IsNullOrEmpty(telefone) OrElse String.IsNullOrEmpty(endereco) OrElse String.IsNullOrEmpty(senha) Then
            mensagem.InnerText = "Preencha todos os campos."
            mensagem.Style("color") = "red"
            Return
        End If

        Dim strConexao As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConexaoDB").ConnectionString
        Using conexao As New SqlConnection(strConexao)
            Dim sql As String = "INSERT INTO Usuarios (Nome, Email, Telefone, Endereco, Senha) VALUES (@Nome, @Email, @Telefone, @Endereco, @Senha)"
            Using cmd As New SqlCommand(sql, conexao)
                cmd.Parameters.AddWithValue("@Nome", nome)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@Telefone", telefone)
                cmd.Parameters.AddWithValue("@Endereco", endereco)
                cmd.Parameters.AddWithValue("@Senha", senha)
                conexao.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        mensagem.InnerText = "Usuário cadastrado com sucesso!"
        mensagem.Style("color") = "green"

        txtNome.Value = ""
        txtEmail.Value = ""
        txtTelefone.Value = ""
        txtEndereco.Value = ""
        txtSenha.Value = ""
    End Sub
End Class
