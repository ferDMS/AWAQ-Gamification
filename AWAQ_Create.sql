
-- Para crear la base de datos y usarla para los siguientes DDLs
DROP SCHEMA IF EXISTS awaqDB;
create database if not exists awaqDB;
use awaqDB;
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
	lastLogin datetime default null,
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
-- Tipos de progreso donde puedes obtener XP (desafio ó captura)
CREATE TABLE IF NOT EXISTS tipos_de_fuente(
	fuente_tipo_id INT NOT NULL AUTO_INCREMENT,
	tipo VARCHAR(30) NOT NULL,
	PRIMARY KEY (fuente_tipo_id),
	CONSTRAINT fuente_tipo_check CHECK (tipo IN ('Desafío','Captura'))
);
-- Tabla de todas las fuentes de progreso en el juego (xp), que incluye desafíos y capturas
CREATE TABLE IF NOT EXISTS fuentes_xp(
	fuente_id INT NOT NULL AUTO_INCREMENT,
	
	-- Declaración explícita de tipo de fuente de xp
	fuente_tipo_id INT NOT NULL,
	
	-- Atributos generales de todas las fuentes de xp
	xp_desbloqueo INT NOT NULL,
	xp_exito INT NOT NULL,
	
	
	PRIMARY KEY (fuente_id),
	CONSTRAINT fxp_fuente_tipo_id FOREIGN KEY (fuente_tipo_id) REFERENCES tipos_de_fuente(fuente_tipo_id)
);
-- Tipos de especies existentes dentro del juego (flora y fauna)
CREATE TABLE IF NOT EXISTS tipos_de_especies(
	especie_tipo_id int NOT NULL AUTO_INCREMENT,
	tipo VARCHAR(30) NOT NULL,
	PRIMARY KEY (especie_tipo_id),
	UNIQUE KEY (tipo),
	CONSTRAINT especie_tipo_check CHECK (tipo IN ('Flora','Fauna'))
);
-- Lista de especies que pueden aparecer dentro del juego
-- El id es una foreign key que viene de la tabla de progresos
create table if not exists especies(
	especie_id INT NOT NULL AUTO_INCREMENT,
	fuente_id int not null,
	
	-- Atributos específicos de fuente de xp tipo especie
	nombre_especie varchar(30) not null,
	nombre_cientifico varchar(30) not null,
	region_id int not null,
	especie_tipo_id int NOT NULL,
	herramienta_id int not null,
	
	primary key (especie_id),
	CONSTRAINT e_fuente_id FOREIGN KEY (fuente_id) REFERENCES fuentes_xp(fuente_id),
	constraint tde_region_id_fk foreign key (region_id) references regiones(region_id),
	CONSTRAINT tde_especie_tipo_id_fk FOREIGN KEY (especie_tipo_id) REFERENCES tipos_de_especies(especie_tipo_id),
	CONSTRAINT tde_herramienta_id_fk FOREIGN KEY (herramienta_id) REFERENCES herramientas(herramienta_id)
	
);
-- Lista de desafíos que deben ser superados a través del juego
CREATE TABLE IF NOT EXISTS desafios(
	desafio_id int NOT NULL AUTO_INCREMENT,
	fuente_id int NOT NULL,
	
	-- Atributo específico de fuente de xp tipo desafío
	xp_fallar int,
	
	PRIMARY KEY (desafio_id),
	CONSTRAINT d_fuente_id FOREIGN KEY (fuente_id) REFERENCES fuentes_xp(fuente_id)
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
-- Tabla para guardar sesiones de juego de diferentes usuarios
CREATE TABLE IF NOT EXISTS sesiones(
	sesion_id INT NOT NULL AUTO_INCREMENT,
	user_id INT NOT NULL,
	start_time datetime NOT NULL,
	end_time datetime DEFAULT NULL,
	PRIMARY KEY (sesion_id),
	CONSTRAINT s_user_id FOREIGN KEY (user_id) REFERENCES usuarios(user_id)
);
-- Tabla para salvar el progreso del usuario en materia de cada captura o desafíos
-- Cada insert en esta tabla representa un nuevo registro o un nuevo desafío completado
-- Tabla para guardar las sesiones de juego de los usuarios
CREATE TABLE IF NOT EXISTS progreso(
	progreso_id INT NOT NULL AUTO_INCREMENT,
	user_id INT NOT NULL,
	fuente_id INT NOT NULL,
	fecha datetime NOT NULL,
	PRIMARY KEY (progreso_id),
	KEY (fecha),
	CONSTRAINT p_user_id FOREIGN KEY (user_id) REFERENCES usuarios(user_id),
	CONSTRAINT p_fuente_id FOREIGN KEY (fuente_id) REFERENCES fuentes_xp(fuente_id)
);

-- ------------------------------------------------------------------------------------------

