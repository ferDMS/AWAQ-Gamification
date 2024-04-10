
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

   
-- ------------------------------------------------------------------------------------------
-- Tablas para guardar información básica para el funcionamiento del juego
-- Esta información se usa dentro de las mecánicas del juego y es vital para su correcto funcionamiento
-- Esta información es independiente del usuario, pues únicamente define cómo es el juego
-- ------------------------------------------------------------------------------------------
  
-- Herramientas que se pueden usar dentro del juego
INSERT INTO herramientas(nombre_herramienta, descripcion, xp_desbloqueo) VALUES
	('Lupa', 'Descripción de uso de lupa', 0),
	('Binoculares', 'Descripción de uso de binoculares', 0),
	('Cámara trampa', 'Descripción de uso de cámara trampa', 7500),
	('Caja trampa', 'Descripción de uso de caja trampa', 7500),
	('Red', 'Descripción de uso de red de nieba', 20000),
	('Linterna', 'Descripción de uso de linterna', 20000);

-- Regiones de donde pueden provenir especies del juego
INSERT INTO regiones(nombre_region) VALUES
	('Cordillera de los Andes'),
	('Bosques nublados'),
	('Bosques húmedos'),
	('Bosques húmedos de tierras bajas'),
	('Selva amazónica'),
	('Selvas tropicales'),
	('Páramos');

-- Tipos de especies existentes dentro del juego (flora y fauna)
INSERT INTO tipos_de_especies(tipo) VALUES ('Flora'), ('Fauna');

-- Lista de especies que pueden aparecer dentro del juego
INSERT INTO especies(
	-- Información básica de la especie
	nombre_especie,
	nombre_cientifico,
	region_id,
	especie_tipo_id,
	-- Información necesaria para mecánicas de videojuego
	xp_desbloqueo,
	xp_registro,
	xp_captura,
	herramienta_id
) 
VALUES
	('Cóndor Andino', 'Vultur Gryphus', 1, 2, 0, 200, 600, 2),
	('Oso de Anteojos', 'Tremarctos Ornatus', 2, 2, 2500, 375, 750, 3),
	('Lagarto Punteado', 'Diploglossus Millepunctatus', 3, 2, 7500, 475, 950, 4),
	('Tucán Pechiblanco', 'Ramphastidae Tucanus', 4, 2, 20000, 500, 1000, 5),
	('Polilla Esfinge Tersa', 'Xylophanes Tersa', 2, 2, 30000, 250, 1500, 6),
	('Tití Ornamentado', 'Callicebus Ornatus', 5, 2, 50000, 675, 2000, 4),
	('Ave del Paraíso', 'Heliconia Latispatha', 6, 1, 0, 500, NULL, 1),
	('Orquídea flor de Mayo', 'Cattleya trianae', 2, 1, 2500, 600, NULL, 1),
	('Arbusto de la mermelada', 'Streptosolen jamesonii', 2, 1, 7500, 750, NULL, 1),
	('Palma de Cera del Quindío', 'Ceroxylon Quindiuense', 2, 1, 20000, 1000, NULL, 1),
	('Frailejones', 'Espeletia Pycnophylla', 7, 1, 30000, 1500, NULL, 1),
	('Arbol de Cacao', 'Theobroma cacao', 5, 1, 50000, 2000, NULL, 1);

-- Lista de desafíos que deben ser superados a través del juego
INSERT INTO desafios(xp_desbloqueo, xp_completar, xp_fallar) VALUES
	(0, 0, 0),
	(6000, 1500, 1500),
	(17000, 3000, 2000),
	(42000, 8000, 7000),
	(85000, 15000, 10000);

-- Los desafíos desbloquean herramientas
-- Debemos detallar cuales desafíos desbloquean cuales herrramientas
INSERT INTO desafios_herramientas(desafio_id, herramienta_id) VALUES
	(1, 1),
	(1, 2),
	(2, 3),
	(2, 4),
	(3, 5),
	(3, 6);
-- ------------------------------------------------------------------------------------------

/*
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
*/