<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Usuário</title>
    <style>
        body, div, form { margin:0; padding:0; font-family: Arial, sans-serif; }
        body { background-color:#f4f4f4; padding:30px; }
        .container { background:#fff; padding:20px; border-radius:8px; box-shadow:0 2px 10px rgba(0,0,0,0.1); width:400px; margin-bottom:30px; }
        h1 { color:#333; }
        form div { margin-bottom:15px; text-align:left; }
        label { display:block; font-size:14px; margin-bottom:5px; color:#666; }
        input { width:100%; padding:8px; font-size:14px; border:1px solid #ccc; border-radius:4px; }
        .btn { width:100%; padding:10px; background:#007bff; color:white; border:none; border-radius:4px; cursor:pointer; }
        .btn:hover { background:#0056b3; }
        #mensagem { margin-top:10px; }

        .tabela { width:100%; border-collapse:collapse; background:#fff; box-shadow:0 2px 10px rgba(0,0,0,0.1); margin-top:20px; }
        .tabela th, .tabela td { border:1px solid #ddd; padding:8px; text-align:left; }
        .tabela th { background:#007bff; color:#fff; }
        .btn-editar, .btn-excluir { padding:5px 10px; border:none; border-radius:4px; cursor:pointer; }
        .btn-editar { background:#28a745; color:white; }
        .btn-excluir { background:#dc3545; color:white; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Cadastro -->
        <div class="container">
            <h1>Cadastro de Usuário</h1>
            <div>
                <label for="txtNome">Nome:</label>
                <asp:TextBox ID="txtNome" runat="server" />
            </div>
            <div>
                <label for="txtEmail">E-mail:</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" />
            </div>
            <div>
                <label for="txtTelefone">Telefone:</label>
                <asp:TextBox ID="txtTelefone" runat="server" MaxLength="15" />
            </div>
            <div>
                <label for="txtEndereco">Endereço:</label>
                <asp:TextBox ID="txtEndereco" runat="server" />
            </div>
            <div>
                <label for="txtSenha">Senha:</label>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" />
            </div>
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" CssClass="btn" OnClick="btnCadastrar_Click" />
            <div id="mensagem" runat="server"></div>
        </div>

        <!-- Lista de Usuários -->
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
                    <ItemTemplate><%# Eval("Telefone") %></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTelefoneGrid" runat="server" Text='<%# Bind("Telefone") %>' MaxLength="15" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Senha">
                    <ItemTemplate>****</ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSenhaGrid" runat="server" Text='<%# Bind("Senha") %>' TextMode="Password" />
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
Imports System.Data
Imports System.Data.SqlClient

Partial Class Cadastro
    Inherits System.Web.UI.Page

    ' String de conexão (ajuste conforme seu servidor e banco)
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
    <!-- Lista de Usuários -->
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
                <ItemTemplate><%# Eval("Telefone") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtTelefoneGrid" runat="server" Text='<%# Bind("Telefone") %>' MaxLength="15" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Senha">
                <ItemTemplate>****</ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtSenhaGrid" runat="server" Text='<%# Bind("Senha") %>' TextMode="Password" />
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
