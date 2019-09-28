CREATE DATABASE DBFisioterapia
GO
Use DBFisioterapia
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
CREATE TABLE Estadocivil
(
	EstadocivilId int IDENTITY(1,1) NOT NULL,
	Codigo char(1) NOT NULL,
	Descripcion nvarchar(100) NOT NULL,
	 CONSTRAINT [PK_dbo.Estadocivil] PRIMARY KEY CLUSTERED 
	(
		EstadocivilId ASC
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
	Ocupacion nvarchar(250),
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
CREATE TABLE AgenteTermico
(
	AgenteTermicoId int IDENTITY(1,1) NOT NULL,
	Nombre	nvarchar(250) NOT NULL,
	Usuariocreacion nvarchar(20) NOT NULL,
	Fechacreacion datetime NOT NULL,
	Usuariomodificacion nvarchar(20),
	Fechamodificacion datetime,
	CONSTRAINT [PK_dbo.AgenteTermico] PRIMARY KEY CLUSTERED 
	(
		AgenteTermicoId ASC
	)
)
GO
CREATE TABLE AgenteElectrofisico
(
	AgenteElectrofisicoId int IDENTITY(1,1) NOT NULL,
	Nombre	nvarchar(250) NOT NULL,
	Usuariocreacion nvarchar(20) NOT NULL,
	Fechacreacion datetime NOT NULL,
	Usuariomodificacion nvarchar(20),
	Fechamodificacion datetime,
	CONSTRAINT [PK_dbo.AgenteElectrofisico] PRIMARY KEY CLUSTERED 
	(
		AgenteElectrofisicoId ASC
	)
)
GO
CREATE TABLE ManiobraTerapeutica
(
	ManiobraTerapeuticaId int IDENTITY(1,1) NOT NULL,
	Nombre	nvarchar(250) NOT NULL,
	Usuariocreacion nvarchar(20) NOT NULL,
	Fechacreacion datetime NOT NULL,
	Usuariomodificacion nvarchar(20),
	Fechamodificacion datetime,
	CONSTRAINT [PK_dbo.ManiobraTerapeutica] PRIMARY KEY CLUSTERED 
	(
		ManiobraTerapeuticaId ASC
	)
)
GO
CREATE TABLE Frecuencia
(
	FrecuenciaId int IDENTITY(1,1) NOT NULL,
	Codigo char(2) NOT NULL,
	Descripcion	nvarchar(100) NOT NULL,
	CONSTRAINT [PK_dbo.Frecuencia] PRIMARY KEY CLUSTERED 
	(
		FrecuenciaId ASC
	)
)
GO
CREATE TABLE Antecedentes
(
	AntecedentesId int IDENTITY(1,1) NOT NULL,
	Descripcion	nvarchar(250) NOT NULL,
	CONSTRAINT [PK_dbo.Antecedentes] PRIMARY KEY CLUSTERED 
	(
		AntecedentesId ASC
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
CREATE UNIQUE NONCLUSTERED INDEX [Estadocivil_Codigo_Index] ON Estadocivil
(
	Codigo ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Estadocivil_Descripcion_Index] ON Estadocivil
(
	Descripcion ASC
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
CREATE UNIQUE NONCLUSTERED INDEX [AgenteTermico_Nombre_Index] ON AgenteTermico
(
	Nombre ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [AgenteElectrofisico_Nombre_Index] ON AgenteElectrofisico
(
	Nombre ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [ManiobraTerapeutica_Nombre_Index] ON ManiobraTerapeutica
(
	Nombre ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Frecuencia_Codigo_Index] ON Frecuencia
(
	Codigo ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Frecuencia_Descripcion_Index] ON Frecuencia
(
	Descripcion ASC
)
GO
CREATE UNIQUE NONCLUSTERED INDEX [Antecedentes_Descripcion_Index] ON Antecedentes
(
	Descripcion ASC
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
--ALTER TABLE Persona  WITH CHECK 
--ADD  CONSTRAINT [FK_dbo.Persona_dbo.Estadocivil_EstadocivilId] 
--FOREIGN KEY(EstadocivilId)
--REFERENCES Estadocivil (EstadocivilId)
--GO
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

--EstadoCivil
INSERT Estadocivil (Codigo,Descripcion) VALUES ('S','Soltero')
INSERT Estadocivil (Codigo,Descripcion) VALUES ('C','Casado')
INSERT Estadocivil (Codigo,Descripcion) VALUES ('V','Viudo')
INSERT Estadocivil (Codigo,Descripcion) VALUES ('D','Divorciado')

--Sexo
INSERT Sexo (Codigo,Descripcion) VALUES ('M','Masculino')
INSERT Sexo (Codigo,Descripcion) VALUES ('F','Femenino')

--Tipodocumento
INSERT Tipodocumento (Codigo,Descripcion) VALUES ('CEX','Carnet de Extranjeria')
INSERT Tipodocumento (Codigo,Descripcion) VALUES ('DNI','Documento Nacional de Identidad')
INSERT Tipodocumento (Codigo,Descripcion) VALUES ('PAS','Pasaporte')

--AgenteTermico
INSERT AgenteTermico (Nombre,Usuariocreacion,Fechacreacion) VALUES ('Compresa Caliente','SYSTEM',GETDATE())
INSERT AgenteTermico (Nombre,Usuariocreacion,Fechacreacion) VALUES ('Compresa Fria','SYSTEM',GETDATE())
INSERT AgenteTermico (Nombre,Usuariocreacion,Fechacreacion) VALUES ('Contraste','SYSTEM',GETDATE())

--AgenteElectrofisico
INSERT AgenteElectrofisico (Nombre,Usuariocreacion,Fechacreacion) VALUES ('Electroanalgesico','SYSTEM',GETDATE())
INSERT AgenteElectrofisico (Nombre,Usuariocreacion,Fechacreacion) VALUES ('Electroestimulacion','SYSTEM',GETDATE())
INSERT AgenteElectrofisico (Nombre,Usuariocreacion,Fechacreacion) VALUES ('Magnetoterapia','SYSTEM',GETDATE())
INSERT AgenteElectrofisico (Nombre,Usuariocreacion,Fechacreacion) VALUES ('Ultrasonido','SYSTEM',GETDATE())
INSERT AgenteElectrofisico (Nombre,Usuariocreacion,Fechacreacion) VALUES ('T. Combinada','SYSTEM',GETDATE())
INSERT AgenteElectrofisico (Nombre,Usuariocreacion,Fechacreacion) VALUES ('Laserterapia','SYSTEM',GETDATE())

--ManiobraTerapeutica
INSERT ManiobraTerapeutica(Nombre,Usuariocreacion,Fechacreacion) VALUES ('Masaje Relajante','SYSTEM',GETDATE())
INSERT ManiobraTerapeutica(Nombre,Usuariocreacion,Fechacreacion) VALUES ('Masaje Descontracturante','SYSTEM',GETDATE())
INSERT ManiobraTerapeutica(Nombre,Usuariocreacion,Fechacreacion) VALUES ('Estiramiento','SYSTEM',GETDATE())
INSERT ManiobraTerapeutica(Nombre,Usuariocreacion,Fechacreacion) VALUES ('Fortalecimiento','SYSTEM',GETDATE())
INSERT ManiobraTerapeutica(Nombre,Usuariocreacion,Fechacreacion) VALUES ('RPG','SYSTEM',GETDATE())
INSERT ManiobraTerapeutica(Nombre,Usuariocreacion,Fechacreacion) VALUES ('Activacion Mimica F.','SYSTEM',GETDATE())
INSERT ManiobraTerapeutica(Nombre,Usuariocreacion,Fechacreacion) VALUES ('TAPE','SYSTEM',GETDATE())

--Frecuencia
INSERT Frecuencia (Codigo,Descripcion) VALUES ('DI','DIARIO')
INSERT Frecuencia (Codigo,Descripcion) VALUES ('ID','INTERMEDIO')

--Antecedentes
INSERT Antecedentes (Descripcion) VALUES ('Riesgo de Caidas')
INSERT Antecedentes (Descripcion) VALUES ('Riesgo de Quemaduras')
INSERT Antecedentes (Descripcion) VALUES ('Esta embarazada')
INSERT Antecedentes (Descripcion) VALUES ('Presenta varices')
INSERT Antecedentes (Descripcion) VALUES ('Tiene Diabetes')
INSERT Antecedentes (Descripcion) VALUES ('Tiene HTA')
INSERT Antecedentes (Descripcion) VALUES ('Diagnostico de Cancer')
INSERT Antecedentes (Descripcion) VALUES ('Usa Marcapaso')
INSERT Antecedentes (Descripcion) VALUES ('Tiene enfermedad cardiaca')
INSERT Antecedentes (Descripcion) VALUES ('Tiene elementos Osteosintesis')