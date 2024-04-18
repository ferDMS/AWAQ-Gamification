
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
    (2, 4, '2024-04-08 12:55:55'),
    (1, 13, '2024-04-01 06:50:26'),
	(2, 13, '2024-04-02 18:50:26'),
	(1, 14, '2024-04-01 11:00:23');
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
