
USE awaqDB;


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

-- Tipos de progreso donde puedes obtener XP (desafio ó captura)
INSERT INTO tipos_de_fuente(tipo) VALUES ('Desafío'), ('Captura');

-- Tabla de todas las fuentes de progreso en el juego (xp), que incluye desafíos y capturas
INSERT INTO fuentes_xp (fuente_tipo_id, xp_desbloqueo, xp_exito) VALUES
	(1, 0, 200),
	(1, 2500, 375),
	(1, 7500, 475),
	(1, 20000, 500),
	(1, 30000, 250),
	(1, 50000, 675),
	(1, 0, 500),
	(1, 2500, 600),
	(1, 7500, 750),
	(1, 20000, 1000),
	(1, 30000, 1500),
	(1, 50000, 2000),
	(2, 0, 0),
	(2, 6000, 1500),
	(2, 17000, 3000),
	(2, 42000, 8000),
	(2, 85000, 15000);

-- Tipos de especies existentes dentro del juego (flora y fauna)
INSERT INTO tipos_de_especies(tipo) VALUES ('Flora'), ('Fauna');

-- Lista de especies que pueden aparecer dentro del juego
INSERT INTO especies(
	-- Información específica de las fuentes de xp tipo especie
	fuente_id,
	nombre_especie,
	nombre_cientifico,
	region_id,
	especie_tipo_id,
	herramienta_id
) 
VALUES
	(1, 'Cóndor Andino', 'Vultur Gryphus', 1, 2, 2),
	(2, 'Oso de Anteojos', 'Tremarctos Ornatus', 2, 2, 3),
	(3, 'Lagarto Punteado', 'Diploglossus Millepunctatus', 3, 2, 4),
	(4, 'Tucán Pechiblanco', 'Ramphastidae Tucanus', 4, 2, 5),
	(5, 'Polilla Esfinge Tersa', 'Xylophanes Tersa', 2, 2, 6),
	(6, 'Tití Ornamentado', 'Callicebus Ornatus', 5, 2, 4),
	(7, 'Ave del Paraíso', 'Heliconia Latispatha', 6, 1, 1),
	(8, 'Orquídea flor de Mayo', 'Cattleya trianae', 2, 1, 1),
	(9, 'Arbusto de la mermelada', 'Streptosolen jamesonii', 2, 1, 1),
	(10, 'Palma de Cera del Quindío', 'Ceroxylon Quindiuense', 2, 1, 1),
	(11, 'Frailejones', 'Espeletia Pycnophylla', 7, 1, 1),
	(12, 'Arbol de Cacao', 'Theobroma cacao', 5, 1, 1);

-- Lista de desafíos que deben ser superados a través del juego
INSERT INTO desafios(fuente_id, xp_fallar) VALUES
	(13, 0),
	(14, 1500),
	(15, 2000),
	(16, 7000),
	(17, 10000);

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
