
USE sprint2;


-- ------------------------------------------------------------------------------------------
-- Tablas para guardar información de usuarios y admins
-- Para autentificar en el log in y guardar información personal
-- ------------------------------------------------------------------------------------------
-- Información de los biomonitores / usuarios
INSERT INTO usuarios(nombre, apellido, genero, pais, ciudad, fechaNacimiento, correo, pass_word, lastLogin) VALUES
	('Fernando', 'Monroy', 'H', 'México', 'Monterrey', '2004-01-15', 'fernando@awaq.org', 'monroy', '2024-03-13 14:04:00'),
    ('Nicolás', 'Mendoza', 'H', 'México', 'Monterrey', '2003-01-01', 'nicolas@awaq.org', 'mendoza', '2024-03-12 08:01:00'),
    ('Sofía', 'Sandoval', 'M', 'México', 'Monterrey', '2003-05-01', 'sofia@awaq.org', 'sandoval', '2024-03-11 17:45:00'),
    ('Regina', 'Cavazos', 'M', 'México', 'Monterrey', '2003-08-01', 'regina@awaq.org', 'cavazos', '2024-03-12 20:25:00'),
    ('Rodrigo', 'López', 'H', 'México', 'Monterrey', '2003-09-01', 'rodrigo@awaq.org', 'lopez', '2024-03-13 09:30:00');

   -- Información de los administradores / moderadores de AWAQ
INSERT INTO admin(nombre, apellido, correo, pass_word) VALUES
	('Angélica', 'Lozano', 'angelica@awaq.org', 'lozano'),
    ('José', 'Serna', 'jose@awaq.org', 'serna');
-- ------------------------------------------------------------------------------------------

/*
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
*/