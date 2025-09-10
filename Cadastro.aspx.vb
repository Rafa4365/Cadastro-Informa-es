Imports System.Data
Imports System.Data.SqlClient

Partial Class Cadastro
    Inherits System.Web.UI.Page

    ' String de conexão (ajuste para seu servidor e banco)
    Private connString As String = "Data Source=SEU_SERVIDOR;Initial Catalog=SEU_BANCO;Integrated Security=True"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarUsuarios()
        End If
    End Sub

    ' Cadastro de usuário
    Protected Sub btnCadastrar_Click(sender As Object, e As EventArgs)
        Dim nome As String = txtNome.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim telefone As String = txtTelefone.Text.Trim()
        Dim endereco As String = txtEndereco.Text.Trim()
        Dim senha As String = txtSenha.Text.Trim()

        If nome = "" Or email = "" Or senha = "" Then
            mensagem.InnerText = "Preencha todos os campos obrigatórios!"
            Return
        End If

        Using conn As New SqlConnection(connString)
            Dim sql As String = "INSERT INTO Usuarios (Nome, Email, Telefone, Endereco, Senha) VALUES (@Nome, @Email, @Telefone, @Endereco, @Senha)"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@Nome", nome)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@Telefone", telefone)
                cmd.Parameters.AddWithValue("@Endereco", endereco)
                cmd.Parameters.AddWithValue("@Senha", senha)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        mensagem.InnerText = "Usuário cadastrado com sucesso!"
        LimparCampos()
        CarregarUsuarios()
    End Sub

    ' Carregar usuários no GridView
    Private Sub CarregarUsuarios()
        Using conn As New SqlConnection(connString)
            Dim sql As String = "SELECT * FROM Usuarios"
            Using da As New SqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                GridView1.DataSource = dt
                GridView1.DataBind()
            End Using
        End Using
    End Sub

    ' Edição de linha no GridView
    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs)
        GridView1.EditIndex = e.NewEditIndex
        CarregarUsuarios()
    End Sub

    ' Cancelar edição
    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        GridView1.EditIndex = -1
        CarregarUsuarios()
    End Sub

    ' Atualizar usuário
    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)
        Dim row As GridViewRow = GridView1.Rows(e.RowIndex)

        Dim nome As String = CType(row.Cells(1).Controls(0), TextBox).Text.Trim()
        Dim email As String = CType(row.Cells(2).Controls(0), TextBox).Text.Trim()
        Dim endereco As String = CType(row.Cells(3).Controls(0), TextBox).Text.Trim()
        Dim telefone As String = CType(row.FindControl("txtTelefoneGrid"), TextBox).Text.Trim()
        Dim senha As String = CType(row.FindControl("txtSenhaGrid"), TextBox).Text.Trim()

        Using conn As New SqlConnection(connString)
            Dim sql As String = "UPDATE Usuarios SET Nome=@Nome, Email=@Email, Endereco=@Endereco, Telefone=@Telefone, Senha=@Senha WHERE Id=@Id"
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@Nome", nome)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@Endereco", endereco)
                cmd.Parameters.AddWithValue("@Telefone", telefone)
                cmd.Parameters.AddWithValue("@Senha", senha)
                cmd.Parameters.AddWithValue("@Id", id)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        GridView1.EditIndex = -1
        CarregarUsuarios()
    End Sub

    ' Excluir usuário
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "ExcluirUsuario" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            Using conn As New SqlConnection(connString)
                Dim sql As String = "DELETE FROM Usuarios WHERE Id=@Id"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Id", id)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            CarregarUsuarios()
        End If
    End Sub

    ' Limpar campos após cadastro
    Private Sub LimparCampos()
        txtNome.Text = ""
        txtEmail.Text = ""
        txtTelefone.Text = ""
        txtEndereco.Text = ""
        txtSenha.Text = ""
    End Sub
End Class
Using conn As New SqlConnection(connString)
    Dim query As String = "INSERT INTO Usuarios (Nome, Email, Telefone, Endereco, Senha) VALUES (@Nome,@Email,@Telefone,@Endereco,@Senha)"
    Dim cmd As New SqlCommand(query, conn)
    cmd.Parameters.AddWithValue("@Nome", txtNome.Text)
    cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
    cmd.Parameters.AddWithValue("@Telefone", txtTelefone.Text)
    cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text)
    cmd.Parameters.AddWithValue("@Senha", txtSenha.Text)
    conn.Open()
    cmd.ExecuteNonQuery()
End Using
