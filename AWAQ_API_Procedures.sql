USE awaqDB;

START TRANSACTION;

DELIMITER //


-- ------------------------------------------------------------------------------------------
-- Procedures relacionados con Usuarios
-- ------------------------------------------------------------------------------------------

-- Seleccionar todos los usuarios
DROP PROCEDURE IF EXISTS `getUsers`//
CREATE PROCEDURE `getUsers` ()
BEGIN
	SELECT * FROM usuarios;
END//

-- Seleccionar a un único usuario según su ID
DROP PROCEDURE IF EXISTS `getUser`//
CREATE PROCEDURE `getUser` (IN user_id_in INT)
BEGIN
	SELECT * FROM usuarios WHERE user_id = user_id_in LIMIT 1;
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

-- Borrar toda la información de un único usuario
DROP PROCEDURE IF EXISTS `deleteUser`//
CREATE PROCEDURE `deleteUser`(IN user_id_in INT)
BEGIN
	DELETE FROM usuarios WHERE user_id = user_id_in;
END//

-- Modificar contraseña antigua a nueva contraseña provista en el procedure
-- El sistema provee el user_id desde la session, el usuario provee la contraseña nueva
DROP PROCEDURE IF EXISTS `updateUserPassword`//
CREATE PROCEDURE `updateUserPassword`(IN user_id_in INT, IN new_pass_word VARCHAR(20))
BEGIN
	UPDATE usuarios SET pass_word = new_pass_word WHERE user_id = user_id_in;
END//
-- ------------------------------------------------------------------------------------------



-- ------------------------------------------------------------------------------------------
-- Verificaciones de datos
-- ------------------------------------------------------------------------------------------

-- Verificar credenciales de usuario desde su correo
	-- Correctas: regresar user_id (para guardar en la session de WEB)
	-- Incorrectas: No regresar nada
DROP PROCEDURE IF EXISTS `verifyUserLogin`//
CREATE PROCEDURE `verifyUserLogin` (IN correo_in VARCHAR(30), IN pass_word_in VARCHAR(20))
BEGIN
	SELECT user_id FROM usuarios WHERE correo = correo_in AND CAST(pass_word AS BINARY) = CAST(pass_word_in AS BINARY) LIMIT 1;
END//

-- Verificar credenciales de usuario desde su ID
DROP PROCEDURE IF EXISTS `verifyUserPassword`//
CREATE PROCEDURE `verifyUserPassword`(IN user_id_in INT, IN pass_word_in VARCHAR(20), OUT user_id_out INT)
BEGIN
	DECLARE correo VARCHAR(30);
	SELECT u.correo INTO correo FROM usuarios u WHERE u.user_id = user_id_in LIMIT 1;
	SELECT `verifyUserLogin`(correo, pass_word_in) INTO user_id_out;
END//

-- Verificar credenciales de admin desde su correo
	-- Correctas: regresar user_id (para guardar en la session de WEB)
	-- Incorrectas: No regresar nada
DROP PROCEDURE IF EXISTS `verifyAdminLogin`//
CREATE PROCEDURE `verifyAdminLogin` (IN correo_in VARCHAR(30), IN pass_word_in VARCHAR(20))
BEGIN
	SELECT admin_in FROM admin WHERE correo = correo_in AND CAST(pass_word AS BINARY) = CAST(pass_word_in AS BINARY) LIMIT 1;
END//
-- ------------------------------------------------------------------------------------------


DELIMITER ;

COMMIT;

-- CALL `allXpPerDay`();
-- CALL `allTimePerDay`();

CALL verifyUserLogin('fernando@awaq.org', 'monroy');