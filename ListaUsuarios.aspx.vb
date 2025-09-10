Imports System.Data.SqlClient
Imports System.Web.UI.WebControls

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
            Dim cmd As New SqlCommand("SELECT Id, Nome, Email, Telefone, Endereco FROM Usuarios", conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End Using
    End Sub

    ' Exporta para Excel
    Protected Sub btnExportarExcel_Click(sender As Object, e As EventArgs)
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Cadastros.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"

        ' GridView temporário
        Dim gv As New GridView()
        Using conn As New SqlConnection(connString)
            Dim cmd As New SqlCommand("SELECT Id, Nome, Email, Telefone, Endereco FROM Usuarios", conn)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            gv.DataSource = dt
            gv.DataBind()
        End Using

        ' Renderiza para Excel
        Dim sw As New System.IO.StringWriter()
        Dim hw As New HtmlTextWriter(sw)
        gv.RenderControl(hw)
        Response.Output.Write(sw.ToString())
        Response.Flush()
        Response.End()
    End Sub

    ' Necessário para permitir GridView fora do form
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    End Sub

End Class
