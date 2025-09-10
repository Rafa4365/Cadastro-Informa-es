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
Protected Sub btnExportarExcel_Click(sender As Object, e As EventArgs)
    Response.Clear()
    Response.Buffer = True
    Response.AddHeader("content-disposition", "attachment;filename=Cadastros.xls")
    Response.Charset = ""
    Response.ContentType = "application/vnd.ms-excel"

    ' Cria GridView temporário
    Dim gv As New GridView()
    Using conn As New SqlConnection(connString)
        Dim cmd As New SqlCommand("SELECT Id, Nome, Email, Telefone, Endereco FROM Usuarios", conn)
        conn.Open()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        gv.DataSource = dt
        gv.DataBind()
    End Using

    ' Renderiza GridView no Excel
    Dim sw As New System.IO.StringWriter()
    Dim hw As New System.Web.UI.HtmlTextWriter(sw)
    gv.RenderControl(hw)
    Response.Output.Write(sw.ToString())
    Response.Flush()
    Response.End()
End Sub

' Necessário para permitir renderizar GridView fora do Page
Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    ' Confirma que o controle pode ser renderizado
End Sub
Protected Sub btnExportarExcel_Click(sender As Object, e As EventArgs)
    ' Limpa a resposta e configura para Excel
    Response.Clear()
    Response.Buffer = True
    Response.AddHeader("content-disposition", "attachment;filename=Cadastros.xls")
    Response.Charset = ""
    Response.ContentType = "application/vnd.ms-excel"

    ' Cria GridView temporário para exportar os dados
    Dim gv As New GridView()
    Using conn As New SqlConnection(connString)
        Dim cmd As New SqlCommand("SELECT Id, Nome, Email, Telefone, Endereco FROM Usuarios", conn)
        conn.Open()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        gv.DataSource = dt
        gv.DataBind()
    End Using

    ' Renderiza o GridView no Excel
    Dim sw As New System.IO.StringWriter()
    Dim hw As New System.Web.UI.HtmlTextWriter(sw)
    gv.RenderControl(hw)
    Response.Output.Write(sw.ToString())
    Response.Flush()
    Response.End()
End Sub

' Necessário para permitir renderizar GridView fora do Page
Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    ' Confirma que o controle pode ser renderizado
End Sub
Private connString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=CadastroDB;Integrated Security=True"
