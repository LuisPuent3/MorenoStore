create database store;
use store;

 -- creacion de tabla privilegios y usuarios
 create table Privilegios(
 idPrivilegio int AUTO_INCREMENT PRIMARY KEY,
 nombrePrivilegio varchar(50)
 );
 
 create table Usuarios(
 idUsuarios int auto_increment primary KEY,
 nombres varchar(50),
 apellidoP varchar(50),
 apellidoM varchar(50),
 dni int,
 email varchar(70),
 telefono int,
 fechaNacimiento date,
 idPrivilegio int,
 
 foreign key (idPrivilegio) references Privilegios(idPrivilegio)
 );
 
 select * from Usuarios;
 select * from Privilegos;

insert into Privilegios (nombrePrivilegio) values ('Administrador');
insert into Privilegios (nombrePrivilegio) values ('Clienter');

insert into Usuarios (nombres, apellidoP, apellidoM, dni, email, telefono, fechaNacimiento, idPrivilegio) values ("Luis", "Ortega", "Ramires", 211933, "luis@gmail.com", 2444484749, "1999-03-13", 1)

