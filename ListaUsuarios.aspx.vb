Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Data

Partial Class Lista
    Inherits System.Web.UI.Page

    Private connString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=CadastroDB;Integrated Security=True"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CarregarGrid()
        End If
    End Sub

    Private Sub CarregarGrid()
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT * FROM Usuarios", conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End Using
    End Sub

    ' Edição da Grid
    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs)
        GridView1.EditIndex = e.NewEditIndex
        CarregarGrid()
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        GridView1.EditIndex = -1
        CarregarGrid()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)
        Dim row As GridViewRow = GridView1.Rows(e.RowIndex)

        Dim nome As String = CType(row.Cells(1).Controls(0), TextBox).Text
        Dim email As String = CType(row.Cells(2).Controls(0), TextBox).Text
        Dim telefone As String = CType(row.Cells(3).Controls(0), TextBox).Text
        Dim endereco As String = CType(row.Cells(4).Controls(0), TextBox).Text
        Dim senha As String = CType(row.FindControl("txtSenha"), TextBox).Text

        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("UPDATE Usuarios SET Nome=@Nome, Email=@Email, Telefone=@Telefone, Endereco=@Endereco, Senha=@Senha WHERE Id=@Id", conn)
            cmd.Parameters.AddWithValue("@Nome", nome)
            cmd.Parameters.AddWithValue("@Email", email)
            cmd.Parameters.AddWithValue("@Telefone", telefone)
            cmd.Parameters.AddWithValue("@Endereco", endereco)
            cmd.Parameters.AddWithValue("@Senha", senha)
            cmd.Parameters.AddWithValue("@Id", id)
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using

        GridView1.EditIndex = -1
        CarregarGrid()
    End Sub

    ' Exclusão
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "ExcluirUsuario" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            Using conn As New SqlConnection(connString)
                Dim cmd As New SqlCommand("DELETE FROM Usuarios WHERE Id=@Id", conn)
                cmd.Parameters.AddWithValue("@Id", id)
                conn.Open()
                cmd.ExecuteNonQuery()
            End Using
            CarregarGrid()
        End If
    End Sub

    ' Exportar para Excel
    Protected Sub btnExportarExcel_Click(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Cadastros.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"

        Dim sw As New System.IO.StringWriter()
        Dim hw As New HtmlTextWriter(sw)

        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT Id, Nome, Email, Telefone, Endereco FROM Usuarios", conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            Dim gv As New GridView()
            gv.DataSource = dt
            gv.DataBind()
            gv.RenderControl(hw)
        End Using

        Response.Output.Write(sw.ToString())
        Response.Flush()
        Response.End()
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Permite renderizar GridView fora do Page
    End Sub

End Class
