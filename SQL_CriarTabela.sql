-- Criação da tabela Usuarios
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Telefone NVARCHAR(20) NULL,
    Endereco NVARCHAR(200) NULL,
    Senha NVARCHAR(100) NOT NULL
);

-- Índice único no e-mail
CREATE UNIQUE INDEX UX_Usuarios_Email ON Usuarios(Email);
