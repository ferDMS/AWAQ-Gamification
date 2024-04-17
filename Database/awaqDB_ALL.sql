
-- AWAQ_Create.sql
-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------

-- Para crear la base de datos y usarla para los siguientes DDLs
DROP SCHEMA IF EXISTS awaqDB;
create database if not exists awaqDB;
use awaqDB;

-- --------------------------------------------------------
-- Tablas para guardar información de usuarios y admins
-- Para autentificar en el log in y guardar información personal
-- --------------------------------------------------------
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
	-- Es escencial que isActive sea por default 1
	-- Pues en los procedures de insert, etc., no es explícito
	isActive tinyint(1) DEFAULT 1,
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
-- --------------------------------------------------------

-- --------------------------------------------------------
-- Tablas para guardar información básica para el funcionamiento del juego
-- Esta información se usa dentro de las mecánicas del juego y es vital para su correcto funcionamiento
-- Esta información es independiente del usuario, pues únicamente define cómo es el juego
-- --------------------------------------------------------
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
	xp_fallar INT DEFAULT 0,	
	
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
	descripcion varchar(250) NOT NULL,
	url_img varchar(250) NOT NULL,
	color varchar(30) NOT NULL,
	tamagno varchar(30) NOT NULL,
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
-- --------------------------------------------------------


-- --------------------------------------------------------
-- Tablas para guardar la información del progreso de los usuarios mientras juegan
-- Estas tablas unifican las dos secciones anteriores de usuarios y mecánicas del juego
-- --------------------------------------------------------
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
	isSuccessful TINYINT(1) DEFAULT 1,
	PRIMARY KEY (progreso_id),
	KEY (fecha),
	CONSTRAINT p_user_id FOREIGN KEY (user_id) REFERENCES usuarios(user_id),
	CONSTRAINT p_fuente_id FOREIGN KEY (fuente_id) REFERENCES fuentes_xp(fuente_id)
);
-- --------------------------------------------------------

-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------
-- AWAQ_Create.sql






-- AWAQ_GameInserts
-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------

USE awaqDB;

-- --------------------------------------------------------
-- Tablas para guardar información de admins
-- Para autentificar en el log in y guardar información personal
-- --------------------------------------------------------
-- Información de los administradores / moderadores de AWAQ
INSERT INTO admin(nombre, apellido, correo, pass_word) VALUES
	('Angélica', 'Lozano', 'angelica@awaq.org', 'lozano'),
    ('José', 'Serna', 'jose@awaq.org', 'serna');
-- --------------------------------------------------------

-- --------------------------------------------------------
-- Tablas para guardar información básica para el funcionamiento del juego
-- Esta información se usa dentro de las mecánicas del juego y es vital para su correcto funcionamiento
-- Esta información es independiente del usuario, pues únicamente define cómo es el juego
-- --------------------------------------------------------
   
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
INSERT INTO fuentes_xp (fuente_tipo_id, xp_desbloqueo, xp_exito, xp_fallar) VALUES
	(1, 0, 200, 0),
	(1, 2500, 375, 0),
	(1, 7500, 475, 0),
	(1, 20000, 500, 0),
	(1, 30000, 250, 0),
	(1, 50000, 675, 0),
	(1, 0, 500, 0),
	(1, 2500, 600, 0),
	(1, 7500, 750, 0),
	(1, 20000, 1000, 0),
	(1, 30000, 1500, 0),
	(1, 50000, 2000, 0),
	(2, 0, 0, 0),
	(2, 6000, 1500, 1500),
	(2, 17000, 3000, 2000),
	(2, 42000, 8000, 7000),
	(2, 85000, 15000, 10000);

-- Tipos de especies existentes dentro del juego (flora y fauna)
INSERT INTO tipos_de_especies(tipo) VALUES ('Flora'), ('Fauna');

-- Lista de especies que pueden aparecer dentro del juego
INSERT INTO especies(
	fuente_id,
	nombre_especie,
	nombre_cientifico,
	descripcion,
	url_img,
	color,
	tamagno,
	region_id,
	especie_tipo_id,
	herramienta_id
) VALUES
	(1, 'Cóndor Andino', 'Vultur Gryphus', 'El Cóndor Andino es una de las aves voladoras más grandes del mundo, reconocible por su majestuoso vuelo sobre las montañas de los Andes.', 'https://i.pinimg.com/564x/82/fb/b6/82fbb6af33d8b552bd67dae441698653.jpg', 'Negro y blanco', '100 - 130 cm', 1, 2, 2),
	(2, 'Oso de Anteojos', 'Tremarctos Ornatus', 'El Oso de Anteojos, también conocido como el oso andino, es el único oso nativo de Sudamérica y se caracteriza por sus distintivas marcas faciales que recuerdan a anteojos.', 'https://static.nationalgeographicla.com/files/styles/image_3200/public/01-andean-bear-minden_00910199.jpg?w=400&h=400&q=75', 'Marrón', '1.2-2 m', 2, 2, 3),
	(3, 'Lagarto Punteado', 'Diploglossus Millepunctatus', 'Este lagarto, nativo de regiones tropicales, posee una piel brillantemente moteada que utiliza como camuflaje entre la hojarasca.', 'https://static.wikia.nocookie.net/colombia/images/6/69/Lagarto_punteado.jpg/revision/latest?cb=20130120201316&path-prefix=es', 'Verde oscuro', '25-40 cm', 3, 2, 4),
	(4, 'Tucán Pechiblanco', 'Ramphastidae Tucanus', 'Conocido por su llamativo pico colorido y grande, el Tucán Pechiblanco es un ícono de los bosques tropicales de América Latina.', 'https://i0.wp.com/birdscolombia.com/wp-content/uploads/2020/07/Ramphastos-tucanus-1.png?resize=590%2C590&ssl=1', 'Negro con blanco', '50-65 cm', 4, 2, 5),
	(5, 'Polilla Esfinge Tersa', 'Xylophanes Tersa', 'La Polilla Esfinge Tersa es conocida por su habilidad de vuelo rápida y precisa, a menudo confundida con un colibrí debido a su manera de alimentarse mientras vuela.', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTC6azYkRJ0zPx_wxqeA0jmru7yoCaHrcsAQZ3crB7FBA&s', 'Verde y marrón', '7-12 cm', 2, 2, 6),
	(6, 'Tití Ornamentado', 'Callicebus Ornatus', 'Este pequeño primate se destaca por su pelaje suave y los dibujos contrastantes en su cuerpo, habitando principalmente en los bosques húmedos del Amazonas.', 'https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/Callicebus_ornatus_mg.jpg/1200px-Callicebus_ornatus_mg.jpg', 'Gris y marrón', '30-45 cm', 5, 2, 4),
	(7, 'Ave del Paraíso', 'Heliconia Latispatha', 'La Ave del Paraíso no es un ave sino una planta tropical conocida por sus espectaculares flores que se asemejan a un plumaje colorido.', 'https://t1.uc.ltmcdn.com/es/posts/0/2/0/como_cuidar_la_planta_ave_del_paraiso_51020_600_square.jpg', 'Rojo y naranja', '1-1.5 m', 6, 1, 1),
	(8, 'Orquídea flor de Mayo', 'Cattleya trianae', 'Esta orquídea, emblema de belleza y delicadeza, es famosa por sus grandes flores vibrantes y es considerada la flor nacional de Colombia.', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ_rnbO0El0jxwzUBANbd4IZbplm4LD_TejF1kPH2laBA&s', 'Violeta y blanco', '15-40 cm', 2, 1, 1),
	(9, 'Arbusto de la mermelada', 'Streptosolen jamesonii', 'El Arbusto de la mermelada atrae numerosos colibríes gracias a sus flores tubulares de colores cálidos que florecen casi todo el año.', 'https://inaturalist-open-data.s3.amazonaws.com/photos/40085519/large.jpg', 'Naranja y amarillo', '50-150 cm', 2, 1, 1),
	(10, 'Palma de Cera del Quindío', 'Ceroxylon Quindiuense', 'Esta palma es conocida como el árbol nacional de Colombia, alcanzando alturas impresionantes y es vital para la conservación del loro orejiamarillo.', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR5tABZecpnTZruqUIVXOK9KxsvVlNeX585gEXsg_hhSw&s', 'Verde', '15-45 m', 2, 1, 1),
	(11, 'Frailejones', 'Espeletia Pycnophylla', 'Los Frailejones son plantas emblemáticas de los páramos andinos, conocidos por su resistencia al frío y su capacidad para retener agua en sus rosetas densas.', 'https://www.elpais.com.co/resizer/a6iLmyg_OcsoBqcSN4scGImbnyY=/1280x1280/smart/filters:format(jpg):quality(80)/cloudfront-us-east-1.images.arcpublishing.com/semana/2BBMTF2C6FAKRAYKOY6ZEKGNUU.jpg', 'Verde y amarillo', '1-2 m', 7, 1, 1),
	(12, 'Arbol de Cacao', 'Theobroma cacao', 'El Árbol de Cacao es esencial para la producción de chocolate, cultivado en las regiones tropicales húmedas con una rica historia en la cultura precolombina.', 'https://thumbs.dreamstime.com/b/teobroma-de-cacao-en-un-%C3%A1rbol-rojo-su-crudo-frijol-bacalao-el-205418065.jpg', 'Marrón', '4-8 m', 5, 1, 1);


-- Lista de desafíos que deben ser superados a través del juego
INSERT INTO desafios(fuente_id) VALUES
	(13),
	(14),
	(15),
	(16),
	(17);

-- Los desafíos desbloquean herramientas
-- Debemos detallar cuales desafíos desbloquean cuales herrramientas
INSERT INTO desafios_herramientas(desafio_id, herramienta_id) VALUES
	(1, 1),
	(1, 2),
	(2, 3),
	(2, 4),
	(3, 5),
	(3, 6);
-- --------------------------------------------------------

-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------
-- AWAQ_GameInserts.sql






-- AWAQ_SampleInserts.sql
-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------

-- --------------------------------------------------------
-- Tablas para guardar la información del progreso de los usuarios mientras juegan
-- Estas tablas unifican las dos secciones anteriores de usuarios y mecánicas del juego
-- --------------------------------------------------------
USE awaqDB;

TRUNCATE TABLE sesiones;
TRUNCATE TABLE progreso;
DELETE FROM usuarios;
ALTER TABLE usuarios AUTO_INCREMENT = 1;

-- Información de los biomonitores / usuarios de ejemplo
INSERT INTO usuarios(nombre, apellido, genero, pais, ciudad, fechaNacimiento, correo, pass_word, lastLogin) VALUES
	('Fernando', 'Monroy', 'H', 'México', 'Monterrey', '2004-01-15', 'fernando@awaq.org', 'monroy', '2024-03-13 14:04:00'),
    ('Nicolás', 'Mendoza', 'H', 'México', 'Monterrey', '2003-01-01', 'nicolas@awaq.org', 'mendoza', '2024-03-12 08:01:00'),
    ('Sofía', 'Sandoval', 'M', 'México', 'Monterrey', '2003-05-01', 'sofia@awaq.org', 'sandoval', '2024-03-11 17:45:00'),
    ('Regina', 'Cavazos', 'M', 'México', 'Monterrey', '2003-08-01', 'regina@awaq.org', 'cavazos', '2024-03-12 20:25:00'),
    ('Rodrigo', 'López', 'H', 'México', 'Monterrey', '2003-09-01', 'rodrigo@awaq.org', 'lopez', '2024-03-13 09:30:00');

-- Tabla para guardar sesiones de juego de diferentes usuarios
INSERT INTO sesiones(user_id, start_time, end_time) VALUES
	(1, '2024-04-01 08:00:00', '2024-04-01 08:10:00'),
    (1, '2024-04-01 04:20:00', '2024-04-01 04:30:00'),
    (2, '2024-04-01 09:45:00', '2024-04-01 09:55:00'),
    (2, '2024-04-03 11:30:00', '2024-04-03 11:40:00'),
    (1, '2024-04-05 15:20:00', '2024-04-05 15:30:00'),
    (1, '2024-04-04 02:10:00', '2024-04-04 02:20:00'),
    (2, '2024-04-07 19:05:00', '2024-04-07 19:15:00'),
    (1, '2024-04-02 14:00:00', '2024-04-02 14:10:00'),
    (2, '2024-04-02 20:40:00', '2024-04-02 20:50:00'),
    (2, '2024-04-06 13:55:00', '2024-04-06 14:05:00'),
    (1, '2024-04-01 06:30:00', '2024-04-01 06:40:00'),
    (1, '2024-04-03 22:15:00', '2024-04-03 22:25:00'),
    (2, '2024-04-05 11:00:00', '2024-04-05 11:10:00'),
    (2, '2024-04-04 04:45:00', '2024-04-04 04:55:00'),
    (1, '2024-04-07 17:50:00', '2024-04-07 18:00:00'),
    (2, '2024-04-02 07:25:00', '2024-04-02 07:35:00'),
    (1, '2024-04-08 09:40:00', '2024-04-08 09:50:00'),
    (2, '2024-04-06 01:30:00', '2024-04-06 01:40:00'),
    (1, '2024-04-05 00:05:00', '2024-04-05 00:15:00'),
    (1, '2024-04-02 18:20:00', '2024-04-02 18:30:00');

-- Salvar el progreso del usuario en materia de cada captura / avistamiento de especies
INSERT INTO progreso(user_id, fuente_id, fecha) VALUES
    (1, 2, '2024-04-01 09:32:15'),
    (2, 3, '2024-04-01 14:20:45'),
    (1, 4, '2024-04-01 18:05:23'),
    (2, 1, '2024-04-01 22:10:32'),
    (1, 3, '2024-04-02 07:45:18'),
    (2, 4, '2024-04-02 10:15:09'),
    (1, 1, '2024-04-02 12:40:55'),
    (2, 2, '2024-04-02 16:25:33'),
    (1, 4, '2024-04-03 08:30:21'),
    (2, 3, '2024-04-03 13:55:47'),
    (1, 2, '2024-04-03 17:10:36'),
    (2, 1, '2024-04-03 21:20:58'),
    (1, 3, '2024-04-04 10:45:12'),
    (2, 4, '2024-04-04 14:30:26'),
    (1, 1, '2024-04-04 19:05:39'),
    (2, 2, '2024-04-04 23:40:54'),
    (1, 4, '2024-04-05 09:15:07'),
    (2, 3, '2024-04-05 12:50:29'),
    (1, 2, '2024-04-05 15:20:43'),
    (2, 1, '2024-04-05 18:35:57'),
    (1, 3, '2024-04-06 07:10:15'),
    (2, 4, '2024-04-06 11:25:27'),
    (1, 1, '2024-04-06 14:40:38'),
    (2, 2, '2024-04-06 18:55:49'),
    (1, 4, '2024-04-07 09:20:04'),
    (2, 3, '2024-04-07 13:35:18'),
    (1, 2, '2024-04-07 16:50:26'),
    (2, 1, '2024-04-07 20:15:34'),
    (1, 3, '2024-04-08 08:40:47'),
    (2, 4, '2024-04-08 12:55:55');

-- Salvar el progreso del usuario en materia de desafíos
INSERT INTO progreso(user_id, fuente_id, fecha, isSuccessful) VALUES
    (1, 13, '2024-04-01 06:50:26', 0),
	(2, 13, '2024-04-02 18:50:26', 1),
	(1, 14, '2024-04-01 11:00:23', 1);
-- --------------------------------------------------------

-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------
-- AWAQ_SampleInserts.sql





-- AWAQ_API_Procedures.sql
-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------

USE awaqDB;

START TRANSACTION;

DELIMITER //

-- --------------------------------------------------------
-- Procedures relacionados con Usuarios
-- --------------------------------------------------------
-- Seleccionar todos los usuarios
DROP PROCEDURE IF EXISTS `getUsers`//
CREATE PROCEDURE `getUsers` ()
BEGIN
	SELECT * FROM usuarios WHERE isActive = 1;
END//

-- Seleccionar a un único usuario según su ID
DROP PROCEDURE IF EXISTS `getUser`//
CREATE PROCEDURE `getUser` (IN user_id_in INT)
BEGIN
	SELECT * FROM usuarios WHERE user_id = user_id_in AND isActive = 1 LIMIT 1;
END//

-- Actualizar la información personal de un único usuario
DROP PROCEDURE IF EXISTS `updateUser`//
CREATE PROCEDURE `updateUser` (
    IN user_id_in INT,
    IN nombre_in VARCHAR(30),
    IN apellido_in VARCHAR(30),
    IN genero_in VARCHAR(10),
    IN pais_in VARCHAR(20),
    IN ciudad_in VARCHAR(20),
    IN fechaNacimiento_in DATE,
    IN correo_in VARCHAR(30),
    IN pass_word_in VARCHAR(20)
)
BEGIN
    UPDATE usuarios 
    SET
        nombre = nombre_in,
        apellido = apellido_in,
        genero = genero_in,
        pais = pais_in,
        ciudad = ciudad_in,
        fechaNacimiento = fechaNacimiento_in,
        correo = correo_in,
        pass_word = pass_word_in
    WHERE
        user_id = user_id_in
    LIMIT 1;
END//

-- Insertar un nuevo usuario con todos sus datos
DROP PROCEDURE IF EXISTS `insertUser`//
CREATE PROCEDURE `insertUser`(
	IN nombre_in VARCHAR(30),
    IN apellido_in VARCHAR(30),
    IN genero_in VARCHAR(10),
    IN pais_in VARCHAR(20),
    IN ciudad_in VARCHAR(20),
    IN fechaNacimiento_in DATE,
    IN correo_in VARCHAR(30),
    IN pass_word_in VARCHAR(20)
)
BEGIN
    INSERT INTO usuarios (nombre, apellido, genero, pais, ciudad, fechaNacimiento, correo, pass_word)
    VALUES (nombre_in, apellido_in, genero_in, pais_in, ciudad_in, fechaNacimiento_in, correo_in, pass_word_in);
END //

-- Borrar de manera lógica la información de un único usuario
DROP PROCEDURE IF EXISTS `deleteUser`//
CREATE PROCEDURE `deleteUser`(IN user_id_in INT)
BEGIN
	UPDATE usuarios SET isActive = 0 WHERE user_id = user_id_in;
END//

-- Reactivar de manera lógica a un usuario que previamente fue borrado (desactivado)
DROP PROCEDURE IF EXISTS `undoDeleteUser`//
CREATE PROCEDURE `undoDeleteUser`(IN user_id_in INT)
BEGIN
	UPDATE usuarios SET isActive = 1 WHERE user_id = user_id_in;
END//

-- Modificar contraseña antigua a nueva contraseña provista en el procedure
-- El sistema provee el user_id desde la session, el usuario provee la contraseña nueva
DROP PROCEDURE IF EXISTS `updateUserPassword`//
CREATE PROCEDURE `updateUserPassword`(IN user_id_in INT, IN new_pass_word VARCHAR(20))
BEGIN
	UPDATE usuarios SET pass_word = new_pass_word WHERE user_id = user_id_in;
END//
-- -- --------------------------------------------------------

-- -- --------------------------------------------------------
-- Verificaciones de datos
-- -- --------------------------------------------------------
-- Verificar credenciales de usuario desde su correo
	-- Correctas: regresar user_id (para guardar en la session de WEB)
	-- Incorrectas: No regresar nada
DROP PROCEDURE IF EXISTS `verifyUserCredentials`//
CREATE PROCEDURE `verifyUserCredentials` (IN correo_in VARCHAR(30), IN pass_word_in VARCHAR(20))
BEGIN
	SELECT user_id FROM usuarios WHERE correo = correo_in AND CAST(pass_word AS BINARY) = CAST(pass_word_in AS BINARY) LIMIT 1;
END//

-- Verificar credenciales de usuario desde su ID
DROP PROCEDURE IF EXISTS `verifyUserPassword`//
CREATE PROCEDURE `verifyUserPassword`(IN user_id_in INT, IN pass_word_in VARCHAR(20))
BEGIN
	DECLARE correo VARCHAR(30);
	SELECT u.correo INTO correo FROM usuarios u WHERE u.user_id = user_id_in LIMIT 1;
	CALL `verifyUserCredentials`(correo, pass_word_in);
END//

-- Verificar credenciales de admin desde su correo
	-- Correctas: regresar user_id (para guardar en la session de WEB)
	-- Incorrectas: No regresar nada
DROP PROCEDURE IF EXISTS `verifyAdminCredentials`//
CREATE PROCEDURE `verifyAdminCredentials` (IN correo_in VARCHAR(30), IN pass_word_in VARCHAR(20))
BEGIN
	SELECT admin_id FROM admin WHERE correo = correo_in AND CAST(pass_word AS BINARY) = CAST(pass_word_in AS BINARY) LIMIT 1;
END//
-- Verificar que un usuario existe a partir de su correo
-- El usuario también debe estar activo (sin borrado lógico)
DROP PROCEDURE IF EXISTS `checkUserExistsByEmail`//
CREATE PROCEDURE `checkUserExistsByEmail` (IN email_in VARCHAR(30))
BEGIN
    SELECT EXISTS(SELECT 1 FROM usuarios WHERE correo = email_in AND isActive = 1 LIMIT 1);
END//
-- Verificar que un admin existe a partir de su correo
DROP PROCEDURE IF EXISTS `checkAdminExistsByEmail`//
CREATE PROCEDURE `checkAdminExistsByEmail` (IN email_in VARCHAR(30))
BEGIN
    SELECT EXISTS(SELECT 1 FROM admin WHERE correo = email_in);
END//
-- --------------------------------------------------------

DELIMITER ;

COMMIT;

-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------
-- AWAQ_API_Procedures.sql





-- AWAQ_Game_API_Procedures.sql
-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------

USE awaqDB;

START TRANSACTION;

DELIMITER //

-- --------------------------------------------------------
-- Procedures para llamadas tipo GET / Select
-- --------------------------------------------------------
-- Regresa todas las especies con sus datos correspondientes
-- TODO: Obtener valores agregados por Rocco también
DROP PROCEDURE IF EXISTS `GetAllEspecies`//
CREATE PROCEDURE `GetAllEspecies`()
BEGIN
    SELECT 
        e.especie_id, 
        e.nombre_especie, 
        e.nombre_cientifico, 
        te.tipo AS tipo_especie, 
        h.herramienta_id,
        h.xp_desbloqueo AS xp_desbloqueo_h,
        h.nombre_herramienta, 
        h.descripcion,
        fxp.fuente_id,
        fxp.xp_desbloqueo AS xp_desbloqueo_e, 
        fxp.xp_exito
    FROM especies e
    INNER JOIN regiones r ON e.region_id = r.region_id
    INNER JOIN tipos_de_especies te ON e.especie_tipo_id = te.especie_tipo_id
    INNER JOIN herramientas h ON e.herramienta_id = h.herramienta_id
    INNER JOIN fuentes_xp fxp ON e.fuente_id = fxp.fuente_id;
END//

-- Regresa todos los desafios con sus datos correspondientes
DROP PROCEDURE IF EXISTS `GetAllDesafios`//
CREATE PROCEDURE `GetAllDesafios`()
BEGIN
    SELECT 
        d.desafio_id, 
        fxp.fuente_id,
        fxp.xp_desbloqueo, 
        fxp.xp_exito, 
        fxp.xp_fallar,
        GROUP_CONCAT(dh.herramienta_id) AS herramienta_ids
    FROM desafios d
    INNER JOIN fuentes_xp fxp ON d.fuente_id = fxp.fuente_id
    LEFT JOIN desafios_herramientas dh ON d.desafio_id = dh.desafio_id
    GROUP BY d.desafio_id;
END //

-- Regresa todos los datos de una herramienta en especifico
DROP PROCEDURE IF EXISTS `GetHerramientasInfo`//
CREATE PROCEDURE `GetHerramientasInfo`(IN HerramientaID INT)
BEGIN
    SELECT 
        herramienta_id,
        nombre_herramienta,
        descripcion,
        xp_desbloqueo
    FROM herramientas
    WHERE herramienta_id = HerramientaID;
END//

-- Regresa una Fuente de xp con sus datos correspondientes
-- Los datos incluyen si es flora o fauna y los puntos que da
DROP PROCEDURE IF EXISTS `GetFuente`//
CREATE PROCEDURE `GetFuente`(IN FuenteID INT)
BEGIN
    SELECT 
        fx.fuente_id,
        fx.xp_desbloqueo,
        fx.xp_exito,
        te.tipo AS especie_tipo,  -- Assuming this still indicates whether it's flora or fauna
        e.especie_id,  -- This will be NULL if no especie is associated
        d.desafio_id   -- This will be NULL if no desafio is associated
    FROM fuentes_xp fx
    LEFT JOIN especies e ON fx.fuente_id = e.fuente_id  -- Linking especies if applicable
    LEFT JOIN desafios d ON fx.fuente_id = d.fuente_id  -- Linking desafios if applicable
    LEFT JOIN tipos_de_especies te ON e.especie_tipo_id = te.especie_tipo_id
    WHERE fx.fuente_id = FuenteID;
END//

-- Regresa XP Total de un usuario
DROP PROCEDURE IF EXISTS `GetTotalXP`//
CREATE PROCEDURE `GetTotalXP`(IN user_id_in INT)
BEGIN
	SELECT 
		p.user_id,
		SUM(CASE WHEN p.isSuccessful = 1 THEN fxp.xp_exito ELSE -1 * fxp.xp_fallar END) AS total_xp
	FROM progreso p
	INNER JOIN fuentes_xp fxp ON p.fuente_id = fxp.fuente_id
	WHERE p.user_id = user_id_in
	GROUP BY p.user_id
	LIMIT 1;
END//

-- Regresa el nombre, descripcion y numero de capturas de flora y fauna por usuario
DROP PROCEDURE IF EXISTS `GetCapturasByUserID`//
CREATE PROCEDURE `GetCapturasByUserID`(IN user_id_in INT)
BEGIN
    SELECT 
        e.especie_id,
        e.nombre_especie,
        e.nombre_cientifico,
        -- CAMBIAR ESTO DESPUÉS, PORQUE ROCCO AÑADIÓ YA UNA DESCRIPCIÓN A LA BASE DE DATOS
        -- PERO POR AHORA ESTÁ BIEN, TAMBIÉN AÑADIÓ TAMAÑO, COLOR, ETC., PARA EL DESAFÍO
        CONCAT( 
            e.nombre_especie, ' también conocido como ', e.nombre_cientifico,
            ' se puede encontrar en ', r.nombre_region, 
            '  y su herramienta de conteo es ', h.nombre_herramienta
        ) AS especie_descripcion,
        COUNT(*) AS capture_count
    FROM progreso p
    INNER JOIN especies e ON p.fuente_id = e.fuente_id
    INNER JOIN regiones r ON e.region_id = r.region_id  -- Assumes each especie is linked to a region
    INNER JOIN herramientas h ON e.herramienta_id = h.herramienta_id  -- Assumes especies link to herramientas
    WHERE p.user_id = user_id_in
    GROUP BY e.especie_id, e.nombre_especie, e.nombre_cientifico, r.nombre_region, h.nombre_herramienta;
END//

-- Regresa los IDs de los desafíos que el usuario a completado exitosamente
DROP PROCEDURE IF EXISTS `GetDesafiosByUserID`//
CREATE PROCEDURE `GetDesafiosByUserID`(IN user_id_in INT)
BEGIN
	SELECT
		d.desafio_id
	FROM
		progreso p
		INNER JOIN desafios d ON p.fuente_id = d.fuente_id 
	WHERE
		user_id = user_id_in
		AND p.isSuccessful = 1;
END//

-- Obtener segundos desde la última sesión de un usuario específico
DROP PROCEDURE IF EXISTS `GetTimeAwayByUserID`//
CREATE PROCEDURE `GetTimeAwayByUserID`(IN user_id_in INT)
BEGIN
	-- Returns an integer of the seconds AFK
	-- TODO	
END//
-- --------------------------------------------------------


-- --------------------------------------------------------
-- Procedures para llamadas tipo POST / Insert / Update
-- --------------------------------------------------------
-- Añade un nuevo registro positivo (suma XP) ó negativo (resta XP) 
-- de una fuente de XP, aplicado a cierto usuario específico.
DROP PROCEDURE IF EXISTS `PostXpEvent`//
CREATE PROCEDURE `PostXpEvent`(IN user_id_in INT, IN fuente_id_in INT, IN fecha_in DATETIME, IN isSuccessful_in TINYINT)
BEGIN
    INSERT INTO progreso(user_id, fuente_id, fecha, isSuccessful)
    VALUES (user_id_in, fuente_id_in, fecha_in, isSuccessful_in);
END//

-- Crear una nueva sesión de un usuario específico, empezando con la hora actual de creación
-- Can be called by something like POST OR PUT
DROP PROCEDURE IF EXISTS `InsertNewSession`//
CREATE PROCEDURE `InsertNewSession`(IN user_id_in INT)
BEGIN
	-- Returns the new session's ID
	-- TODO
END//

-- Actualizar la última sesión de un usuario específico como activa a la hora actual
-- Can be called by something like POST OR PUT
DROP PROCEDURE IF EXISTS `UpdateLastSession`//
CREATE PROCEDURE `UpdateLastSession`(IN user_id_in INT)
BEGIN
	-- Doesn't return anything
	-- TODO
END//
-- --------------------------------------------------------

DELIMITER ;

COMMIT;

-- ------------------------------------------------------------------------------------------
-- ------------------------------------------------------------------------------------------
-- AWAQ_API_Procedures.sql