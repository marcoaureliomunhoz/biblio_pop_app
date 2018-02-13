create table Autor (
    AutorId             int identity, 
    Nome                varchar(50), 
    Sobrenome           varchar(100), 
    Email               varchar(300), 
    constraint PK_Autor primary key (AutorId)
);
GO 

create table Editora (
    EditoraId           int identity, 
    Nome                varchar(50), 
    Site                varchar(500), 
    constraint PK_Editora primary key (EditoraId)
);
GO 

create table Livro (
    LivroId             int identity, 
    Titulo              varchar(100), 
    Estante             varchar(50), 
    AnoPublicacao       int,
    EditoraId           int, 
    constraint PK_Livro primary key (LivroId), 
    constraint FK_Livro_Editora foreign key (EditoraId) references Editora (EditoraId)
); 
GO

create table LivroAutoria (
    LivroId             int, 
    AutorId             int, 
    constraint PK_LivroAutoria primary key (LivroId, AutorId), 
    constraint FK_LivroAutoria_Livro foreign key (LivroId) references Livro (LivroId), 
    constraint FK_LivroAutoria_Autor foreign key (AutorId) references Autor (AutorId)
);
GO 