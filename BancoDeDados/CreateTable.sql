CREATE TABLE Cargos(
    CargoId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Descricao VARCHAR(20) NOT NULL 
);

CREATE TABLE Usuario(
Email Varchar (255) Primary key not null,
Senha varchar (128) not null,
Nome Varchar(80) not null,
CargoId int not null references Cargos(CargoId)
);

CREATE TABLE Funcionarios(
    FuncionarioId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    NomeDoFuncionario VARCHAR(255) NOT NULL,
    Cpf VARCHAR(14) NOT NULL,
    NascimentoFuncionario DATE NOT NULL,
    DataDeAdmissao DATE NOT NULL,
    CelularFuncionario VARCHAR(11) NOT NULL,
    EmailFuncionario VARCHAR(255) NOT NULL,
    CargoId INT FOREIGN KEY  REFERENCES Cargos(CargoId)

);

CREATE TABLE Liderancas(
    LiderancaId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    FuncionarioId INT FOREIGN KEY REFERENCES Funcionarios(FuncionarioId),
    DescricaoEquipe VARCHAR(255) NOT NULL
);

CREATE TABLE Equipes(
    EquipeId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    LiderancaId INT FOREIGN KEY REFERENCES Liderancas(LiderancaId),
    FuncionarioId INT FOREIGN KEY REFERENCES Funcionarios(FuncionarioId)
);

CREATE TABLE Ponto(
    PontoId BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
    DataHorarioPonto DATETIME NOT NULL,
    Justificativa VARCHAR(255),
    FuncionarioId INT FOREIGN KEY REFERENCES Funcionarios(FuncionarioId)
);