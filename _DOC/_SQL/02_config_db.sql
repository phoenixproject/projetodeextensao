use PROJETODEEXTENSAO
go

begin tran

--*******************
----- Sistema  ----**
--*******************

--Tabela Tipo de Usuário
insert into tipo_usuario values ('Admin')
insert into tipo_usuario values ('Usuário Comum')

--Tabela Tipo de Servicço
insert into tipo_servico values ('default')
insert into tipo_servico values ('Carpinteirovalues (a)')
insert into tipo_servico values ('Pintorvalues (a)')
insert into tipo_servico values ('Pedreirovalues (a)')
insert into tipo_servico values ('Serralheirovalues (a)')
insert into tipo_servico values ('Padeirovalues (a)')
insert into tipo_servico values ('Cabelereirovalues (a)')
insert into tipo_servico values ('Motorista')
insert into tipo_servico values ('Papelaria')
insert into tipo_servico values ('Supermercado')
insert into tipo_servico values ('Hotel')

-- Tabela Status
insert into status values ('ativo')
insert into status values ('inativo')

-- Inserindo registros na tabela Usuário
insert into usuario
values ('teste1@gmail.com', '123', 'Paulo', '27-39933-2211', 1, 1, 1) -- Tipo Usuário: admin, Tipo de Usuário: default, Status: ativo
insert into usuario
values ('teste2@gmail.com', '123', 'Hércules', '27-2311-2211', 1, 1, 1) -- Tipo Usuário: admin, Tipo de Usuário: default, Status: ativo
insert into usuario
values ('teste3@gmail.com', '123', 'Jean', '27-5654-2211', 1, 1, 1) -- Tipo Usuário: admin, Tipo de Usuário: default, Status: ativo
insert into usuario
values ('teste4@gmail.com', '123', 'Matheus', '27-4232-2211', 1, 1, 1) -- Tipo Usuário: admin, Tipo de Usuário: default, Status: ativo
insert into usuario
values ('teste5@gmail.com', '123', 'Tallys', '27-3933-2211', 1, 1, 1) -- Tipo Usuário: admin, Tipo de Usuário: default, Status: ativo
insert into usuario
values ('josesilva@gmail.com', '123', 'Jose Sílva', '27-1212-2211', 2, 2, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Carpinteiro, Status: ativo
insert into usuario
values ('jonasxavier@gmail.com', '123', 'Jonas Xavier', '27-4442-2211', 2, 2, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Carpinteiro, Status: ativo
insert into usuario
values ('mara@gmail.com', '123', 'Mara Souza', '27-9873-2211', 2, 3, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Pintor, Status: ativo
insert into usuario
values ('reginaldo@gmail.com', '123', 'Reginaldo Monteiro', '27-3232-2211', 2, 4, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Pedreiro, Status: ativo
insert into usuario
values ('mariosilva@gmail.com', '123', 'Mario Silva', '27-2345-2211', 2, 5, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Serralheiro, Status: ativo
insert into usuario
values ('marcosmonteiro@gmail.com', '123', 'Marcos Monteiro', '27-7363-2211', 2, 6, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Padeeiro, Status: ativo
insert into usuario
values ('neide@gmail.com', '123', 'Neide', '27-7363-2211', 2, 7, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Cabelereira, Status: ativo
insert into usuario
values ('sidney@gmail.com', '123', 'Sidney', '27-1231-2211', 2, 8, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Motorista, Status: ativo
insert into usuario
values ('nadir@gmail.com', '123', 'Nadir Souza', '27-3322-2211', 2, 8, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Papelaria, Status: ativo
insert into usuario
values ('jaime@gmail.com', '123', 'Jaime Rodrigues', '27-8833-2211', 2, 8, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Supermercado, Status: ativo
insert into usuario
values ('rosa@gmail.com', '123', 'Dona Rosa', '27-9983-2211', 2, 8, 1) -- Tipo Usuário: usuario, Tipo de Usuário: Hotel, Status: ativo

commit