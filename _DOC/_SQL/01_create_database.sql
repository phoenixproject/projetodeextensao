use PROJETODEEXTENSAO
go

begin tran

--*******************
----- Sistema  ----**
--*******************

CREATE TABLE Tipo_Usuario (
	tp_usuario					integer not null identity(1,1),
	ds_tp_usuario				varchar(30) not null
);

CREATE TABLE Tipo_Servico (
	tp_servico					integer not null identity(1,1),
	ds_tp_servico				varchar(100) not null
);

CREATE TABLE Status (
	cd_status					integer not null identity(1,1),
	ds_status					varchar(10) not null
);

CREATE TABLE Usuario (
	cd_usuario						integer not null identity(1,1),
	email							varchar(100) not null,	
	password						varchar(300) not null,
	nome							varchar(100) not null,	
	telefone						varchar(30) not null,		
	tp_usuario						integer not null,
	tp_servico						integer not null,
	cd_status						integer not null	
);

-- Criação das Primay Key do Sistema

ALTER TABLE Usuario ADD CONSTRAINT PK_EP_Usuario_cd_usuario PRIMARY KEY(cd_usuario);
ALTER TABLE Tipo_Usuario ADD CONSTRAINT PK_Tipo_Usuario_tp_usuario PRIMARY KEY(tp_usuario);
ALTER TABLE Tipo_Servico ADD CONSTRAINT PK_Tipo_Servico_tp_servico PRIMARY KEY(tp_servico);
ALTER TABLE Status ADD CONSTRAINT PK_Status_cd_status PRIMARY KEY(cd_status);

-- Criação das Foreign Key do Sistema

ALTER TABLE Usuario ADD CONSTRAINT FK_Usuario_Tipo_Usuario_tp_usuario FOREIGN KEY (tp_usuario) REFERENCES Tipo_Usuario(tp_usuario);
ALTER TABLE Usuario ADD CONSTRAINT FK_Usuario_Tipo_Servico_tp_servico FOREIGN KEY (tp_servico) REFERENCES Tipo_Servico(tp_servico);
ALTER TABLE Usuario ADD CONSTRAINT FK_Usuario_Status_cd_status FOREIGN KEY (cd_status) REFERENCES Status(cd_status);

commit