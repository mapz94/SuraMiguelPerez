/*
Miguel Pérez
*/

/*
create database Sura
*/

use Sura

create table administradores(
	id int primary key identity (1,1),
	nombre varchar(50) not null, 
	apellido varchar(50)not null,
	contrasena varchar(50)not null
)

create table clientes(
	id int primary key identity(1,1), 
	rut varchar(50)not null, 
	nombre varchar(50)not null, 
	apellido varchar(50)not null, 
	saldo int not null,
	adicional int not null,
	fecha_ing date not null,
)

create table tiposeguros(
	id int primary key  identity(1,1),
	nombreTipo varchar(50) not null,
	valor int not null
)

create table tipopago(
	id int primary key  identity(1,1),
	nombreTipo varchar(50) not null,
	valor int not null
)

create table seguros(
	id int primary key identity(1,1),
	adicional int not null,
	tiposeguro_id int not null,
	tipopago_id int not null,
	cuotas int not null,
	cliente_id int not null,
	constraint FK_cliente
		foreign key (cliente_id) references clientes (id),
	constraint FK_tiposeguro
		foreign key (tiposeguro_id) references tiposeguros (id),
	constraint FK_tipopago
		foreign key (tipopago_id) references tipopago (id)
		/*
			Pretendía utilizar los seguros de mejor forma y que se crearan de acuerdo
			a los calculos.
			Pero no me alcanzó el tiempo siendo una semana intensa.
		*/
)

/*Tipos de pagos*/
insert into tipopago (nombreTipo, valor) values ('Pago Automático', 10000)
insert into tipopago (nombreTipo, valor) values ('Pago Manual', 0)


/*Tipos de seguros*/
insert into tiposeguros (nombreTipo, valor) values ('Seguro Colectivo', 120000)
insert into tiposeguros (nombreTipo, valor) values ('Seguro Emfermedades', 80000)
insert into tiposeguros (nombreTipo, valor) values ('Seguro Hogar', 220000)

/*Solicitudes de la actividad*/
insert into administradores (nombre, apellido, contrasena) values ('stotomas',' ', '123')
insert into clientes (rut, nombre, apellido, saldo, adicional, fecha_ing) values 
('18.359.389-7', 'AlguienA', 'ApellidoA', 78000, 520, GETDATE())
insert into clientes (rut, nombre, apellido, saldo, adicional, fecha_ing) values 
('11.441.845-5', 'AlguienB', 'ApellidoB', 193000, 200, GETDATE())

