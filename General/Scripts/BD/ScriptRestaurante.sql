CREATE DATABASE DBRestaurante
GO
Use DBRestaurante
GO
CREATE TABLE Departamento
(
	DepartamentoId int IDENTITY(1,1) NOT NULL,
	Nombre nvarchar(100) NOT NULL,
	 CONSTRAINT [PK_dbo.Departamento] PRIMARY KEY CLUSTERED 
	(
		DepartamentoId ASC
	)
)
GO
CREATE TABLE Distrito
(
	DistritoId int IDENTITY(1,1) NOT NULL,
	DepartamentoId int NOT NULL,
	Nombre nvarchar(100) NOT NULL,
	CONSTRAINT [PK_dbo.Distrito] PRIMARY KEY CLUSTERED 
	(
		DistritoId ASC
	)
)
GO
CREATE TABLE Tipodocumento
(
	TipodocumentoId int IDENTITY(1,1) NOT NULL,
	Codigo char(3) NOT NULL,
	Descripcion nvarchar(100) NOT NULL,
	 CONSTRAINT [PK_dbo.Tipodocumento] PRIMARY KEY CLUSTERED 
	(
		TipodocumentoId ASC
	)
)
GO
CREATE TABLE Sexo
(
	SexoId int IDENTITY(1,1) NOT NULL,
	Codigo char(1) NOT NULL,
	Descripcion nvarchar(100) NOT NULL,
	 CONSTRAINT [PK_dbo.Sexo] PRIMARY KEY CLUSTERED 
	(
		SexoId ASC
	)
)
GO
CREATE TABLE Persona
(
	PersonaId int IDENTITY(1,1) NOT NULL,
	TipodocumentoId int NOT NULL,
	Nrodocumento nvarchar(30) NOT NULL,
	Nombre nvarchar(50) NOT NULL,
	Apellidopaterno nvarchar(50) NOT NULL,
	Apellidomaterno nvarchar(50) NOT NULL,
	Nrotelefono nvarchar(12),
	Fecnacimiento datetime,
	DistritoId int NOT NULL,
	Direccion nvarchar(200) NOT NULL,
	--Ocupacion nvarchar(250),
	--EstadocivilId int NOT NULL,
	SexoId int NOT NULL,
	Usuariocreacion nvarchar(20) NOT NULL,
	Fechacreacion datetime NOT NULL,
	Usuariomodificacion nvarchar(20),
	Fechamodificacion datetime,
	CONSTRAINT [PK_dbo.Persona] PRIMARY KEY CLUSTERED 
	(
		PersonaId ASC
	)
)
GO
--UNIQUE
CREATE UNIQUE NONCLUSTERED INDEX [Departamento_Nombre_Index] ON Departamento
(
	Nombre ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Distrito_Nombre_Index] ON Distrito
(
	Nombre ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Sexo_Codigo_Index] ON Sexo
(
	Codigo ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Sexo_Descripcion_Index] ON Sexo
(
	Descripcion ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Tipodocumento_Codigo_Index] ON Tipodocumento
(
	Codigo ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Tipodocumento_Descripcion_Index] ON Tipodocumento
(
	Descripcion ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Persona_Nrodocumento_Index] ON Persona
(
	Nrodocumento ASC
)
GO
--FOREIGN
ALTER TABLE Distrito  WITH CHECK 
ADD  CONSTRAINT [FK_dbo.Distrito_dbo.Departamento_DepartamentoId] 
FOREIGN KEY(DepartamentoId)
REFERENCES Departamento (DepartamentoId)
GO
ALTER TABLE Persona  WITH CHECK 
ADD  CONSTRAINT [FK_dbo.Persona_dbo.Distrito_DistritoId] 
FOREIGN KEY(DistritoId)
REFERENCES Distrito (DistritoId)
GO
ALTER TABLE Persona  WITH CHECK 
ADD  CONSTRAINT [FK_dbo.Persona_dbo.Sexo_SexoId] 
FOREIGN KEY(SexoId)
REFERENCES Sexo (SexoId)
GO
ALTER TABLE Persona  WITH CHECK 
ADD  CONSTRAINT [FK_dbo.Persona_dbo.Tipodocumento_TipodocumentoId] 
FOREIGN KEY(TipodocumentoId)
REFERENCES Tipodocumento (TipodocumentoId)
GO

--INSERT INTO 
--Departamento
INSERT Departamento (Nombre) VALUES (N'Lima')

--Distrito
INSERT Distrito (DepartamentoId,Nombre) VALUES (1,N'Los Olivos')
INSERT Distrito (DepartamentoId,Nombre) VALUES (1,N'Puente Piedra')
INSERT Distrito (DepartamentoId,Nombre) VALUES (1,N'San Miguel')
INSERT Distrito (DepartamentoId,Nombre) VALUES (1,N'Lince')
INSERT Distrito (DepartamentoId,Nombre) VALUES (1,N'SMP')
INSERT Distrito (DepartamentoId,Nombre) VALUES (1,N'SJL')

--Sexo
INSERT Sexo (Codigo,Descripcion) VALUES ('M','Masculino')
INSERT Sexo (Codigo,Descripcion) VALUES ('F','Femenino')

--Tipodocumento
INSERT Tipodocumento (Codigo,Descripcion) VALUES ('CEX','Carnet de Extranjeria')
INSERT Tipodocumento (Codigo,Descripcion) VALUES ('DNI','Documento Nacional de Identidad')
INSERT Tipodocumento (Codigo,Descripcion) VALUES ('PAS','Pasaporte')