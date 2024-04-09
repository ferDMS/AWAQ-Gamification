
-- Para crear la base de datos y usarla para los siguientes DDLs
-- DROP SCHEMA IF EXISTS sprint2;''
create database if not exists sprint2;
use sprint2;
-- ------------------------------------------------------------------------------------------


-- ------------------------------------------------------------------------------------------
-- Tablas para guardar información de usuarios y admins
-- Para autentificar en el log in y guardar información personal
-- ------------------------------------------------------------------------------------------
-- Información de los biomonitores / usuarios
create table if not exists usuarios (
	user_id int not null auto_increment,
	nombre varchar(30) not null,
	apellido varchar(30) not null,
	genero varchar(10) default null,
	pais varchar(20) not null,
	ciudad varchar(20) not null,
	fechaNacimiento date not null,
	correo varchar(30) not null,
	pass_word varchar(20) not null,
	lastLogin datetime not null,
	primary key(user_id)
);
-- Información de los administradores / moderadores de AWAQ
create table if not exists admin(
	admin_id int not null auto_increment,
	nombre varchar(30) not null,
	apellido varchar(30) not null,
	correo varchar(30) not null,
	pass_word varchar(20) not null,
    primary key (admin_id)
);
-- ------------------------------------------------------------------------------------------


-- ------------------------------------------------------------------------------------------
-- Tablas para guardar información básica para el funcionamiento del juego
-- Esta información se usa dentro de las mecánicas del juego y es vital para su correcto funcionamiento
-- Esta información es independiente del usuario, pues únicamente define cómo es el juego
-- ------------------------------------------------------------------------------------------
-- Herramientas que se pueden usar dentro del juego
create table if not exists herramientas(
	herramienta_id int not null auto_increment,
	nombre_herramienta varchar(50) not null,
	descripcion varchar(300),
	xp_desbloqueo int NOT NULL,
	primary key (herramienta_id)
);
-- Regiones de donde pueden provenir especies del juego
create table if not exists regiones(
	region_id int not null auto_increment,
	nombre_region varchar(50) not null,
	primary key (region_id)
);
-- Tipos de especies existentes dentro del juego (flora y fauna)
CREATE TABLE IF NOT EXISTS tipos_de_especies(
	especie_tipo_id int NOT NULL AUTO_INCREMENT,
	tipo VARCHAR(30) NOT NULL,
	PRIMARY KEY (especie_tipo_id),
	UNIQUE KEY (tipo)
);
-- Lista de especies que pueden aparecer dentro del juego
create table if not exists especies(
	especie_id int not null auto_increment,
	
	-- Información básica de la especie
	nombre_especie varchar(30) not null,
	nombre_cientifico varchar(30) not null,
	region_id int not null,
	especie_tipo_id int NOT NULL,
	
	-- Información necesaria para mecánicas de videojuego
	xp_desbloqueo int not null,
	xp_registro int NOT NULL,
	xp_captura int DEFAULT NULL, 
	herramienta_id int not null,
	
	primary key (especie_id),
	constraint tde_region_id_fk foreign key (region_id) references regiones(region_id),
	CONSTRAINT tde_especie_tipo_id_fk FOREIGN KEY (especie_tipo_id) REFERENCES tipos_de_especies(especie_tipo_id),
	CONSTRAINT tde_herramienta_id_fk FOREIGN KEY (herramienta_id) REFERENCES herramientas(herramienta_id)
);
-- Lista de desafíos que deben ser superados a través del juego
CREATE TABLE IF NOT EXISTS desafios(
	desafio_id int NOT NULL AUTO_INCREMENT,
	xp_desbloqueo int NOT NULL,
	xp_completar int,
	xp_fallar int,
	PRIMARY KEY (desafio_id)
);
-- Los desafíos desbloquean herramientas
-- Debemos detallar cuales desafíos desbloquean cuales herrramientas
CREATE TABLE IF NOT EXISTS desafios_herramientas(
	desafio_id int NOT NULL,
	herramienta_id int NOT NULL,
	PRIMARY KEY (desafio_id, herramienta_id),
	CONSTRAINT dh_desafio_id_fk FOREIGN KEY (desafio_id) REFERENCES desafios(desafio_id),
	CONSTRAINT dh_herramienta_id_fk FOREIGN KEY (herramienta_id) REFERENCES herramientas(herramienta_id)
);
-- ------------------------------------------------------------------------------------------


-- ------------------------------------------------------------------------------------------''
-- Tablas para guardar la información del progreso de los usuarios mientras juegan
-- Estas tablas unifican las dos secciones anteriores de usuarios y mecánicas del juego
-- ------------------------------------------------------------------------------------------
-- Tabla para salvar el progreso del usuario en materia de cada captura / avistamiento de especies
-- Óptimamente ingresaríamos datos a esta table con inserts de múltiples entradas correspondientes a una sesión de juego
CREATE TABLE IF NOT EXISTS progreso_especies (
	progreso_id int NOT NULL AUTO_INCREMENT,
	user_id int NOT NULL,
	especie_id int NOT NULL,
	fecha datetime NOT NULL,
	PRIMARY KEY (progreso_id),
	KEY (fecha),
	CONSTRAINT pe_user_id_fk FOREIGN KEY (user_id) REFERENCES usuarios(user_id),
	CONSTRAINT pe_especie_id_fk FOREIGN KEY (especie_id) REFERENCES especies(especie_id)
);
-- Tabla para salvar el progreso del usuario en materia de desafíos completados
CREATE TABLE IF NOT EXISTS progreso_desafios(
	progreso_id int NOT NULL AUTO_INCREMENT,
	desafio_id int NOT NULL,
	user_id int NOT NULL,
	start_time datetime DEFAULT NULL,
	end_time datetime DEFAULT NULL,
	PRIMARY KEY (progreso_id),
	KEY (start_time),
	KEY	(end_time),
	CONSTRAINT pd_desafio_id_fk FOREIGN KEY (desafio_id) REFERENCES desafios(desafio_id),
	CONSTRAINT pd_user_id_fk FOREIGN KEY (user_id) REFERENCES usuarios(user_id)
);
-- No se requiere una tabla de herramientas a usuarios pues se pueden hacer los siguientes joins:
-- usuarios -> progreso_desafios -> desafios -> desafios_herramientas -> herramientas
-- Sin embargo, si se quisiera romper las reglas de normalización para no hacer todo este join, podríamos definir:
/*
create table if not exists herramientas_usuarios(
	usuario_id int not null,
	herramienta_id int not null,
	primary key(usuario_id, herramienta_id),
	constraint hu_user_id_fk foreign key (usuario_id) references usuarios(user_id),
	constraint hu_herramienta_id_fk foreign key (herramienta_id) references herramientas(herramienta_id)
);
*/
-- ------------------------------------------------------------------------------------------

