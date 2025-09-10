Imports System.Data.SqlClient

Partial Class ListaUsuarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarUsuarios()
        End If
    End Sub

    Private Sub CarregarUsuarios()
        Dim strConexao As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConexaoDB").ConnectionString
        Using conexao As New SqlConnection(strConexao)
            Dim sql As String = "SELECT Id, Nome, Email, Telefone, Endereco, Senha FROM Usuarios ORDER BY Id DESC"
            Using cmd As New SqlCommand(sql, conexao)
                conexao.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                GridView1.DataSource = dr
                GridView1.DataBind()
            End Using
        End Using
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs)
        GridView1.EditIndex = e.NewEditIndex
        CarregarUsuarios()
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        GridView1.EditIndex = -1
        CarregarUsuarios()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)
        Dim nome As String = DirectCast(GridView1.Rows(e.RowIndex).Cells(1).Controls(0), TextBox).Text
        Dim email As String = DirectCast(GridView1.Rows(e.RowIndex).Cells(2).Controls(0), TextBox).Text
        Dim telefone As String = DirectCast(GridView1.Rows(e.RowIndex).Cells(3).Controls(0), TextBox).Text
        Dim endereco As String = DirectCast(GridView1.Rows(e.RowIndex).Cells(4).Controls(0), TextBox).Text
        Dim senha As String = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtSenha"), TextBox).Text

        Dim strConexao As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConexaoDB").ConnectionString
        Using conexao As New SqlConnection(strConexao)
            Dim sql As String = "UPDATE Usuarios SET Nome=@Nome, Email=@Email, Telefone=@Telefone, Endereco=@Endereco, Senha=@Senha WHERE Id=@Id"
            Using cmd As New SqlCommand(sql, conexao)
                cmd.Parameters.AddWithValue("@Id", id)
                cmd.Parameters.AddWithValue("@Nome", nome)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@Telefone", telefone)
                cmd.Parameters.AddWithValue("@Endereco", endereco)
                cmd.Parameters.AddWithValue("@Senha", senha)
                conexao.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        GridView1.EditIndex = -1
        CarregarUsuarios()
    End Sub

    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "ExcluirUsuario" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            Dim strConexao As String = System.Configuration.ConfigurationManager.ConnectionStrings("ConexaoDB").ConnectionString
            Using conexao As New SqlConnection(strConexao)
                Dim sql As String = "DELETE FROM Usuarios WHERE Id=@Id"
                Using cmd As New SqlCommand(sql, conexao)
                    cmd.Parameters.AddWithValue("@Id", id)
                    conexao.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            CarregarUsuarios()
        End If
    End Sub
End Class
