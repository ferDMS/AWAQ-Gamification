/*
SELECT * FROM progreso WHERE user_id = 29;
SELECT * FROM usuarios u;

-- Obtener XP de un usuario hasta cierto progreso
SELECT 
	p.user_id,
	SUM(CASE WHEN p.isSuccessful = 1 THEN fxp.xp_exito ELSE -1 * fxp.xp_fallar END) AS total_xp
FROM progreso p
INNER JOIN fuentes_xp fxp ON p.fuente_id = fxp.fuente_id
WHERE p.user_id = 3 AND progreso_id < 174
GROUP BY p.user_id
LIMIT 1;

-- Insert usuarios demo de prueba
INSERT INTO usuarios (nombre, apellido, genero, pais, ciudad, fechaNacimiento, correo, pass_word, lastLogin) VALUES
	('Demo', '1', 'H', 'México', 'Monterrey', '2024-05-01', 'demo1', '1', '2024-05-01 00:00:01'),
	('Demo', '2', 'H', 'México', 'Monterrey', '2024-05-01', 'demo2', '2', '2024-05-01 00:00:03'),
	('Demo', '3', 'H', 'México', 'Monterrey', '2024-05-01', 'demo3', '3', '2024-05-01 00:00:03');
*/

START TRANSACTION;

SELECT * FROM usuarios u WHERE user_id > 27 AND user_id < 31 ORDER BY user_id DESC LIMIT 3;

DELETE FROM progreso WHERE user_id = 28;
DELETE FROM progreso WHERE user_id = 29;
DELETE FROM progreso WHERE user_id = 30;

-- Insertar progreso copiandolo de un usuario 3 hacia otro usuario 29, nada más cierta cantidad de entradas máximo
-- Este específico insert para el estado de la base de datos actual
-- Este es para a punto de llegar al desafío que desbloquea el transecto
INSERT INTO progreso (user_id, fuente_id, fecha, isSuccessful)
SELECT 
	29 AS user_id,
    fuente_id, 
    fecha, 
    isSuccessful 
FROM 
    progreso 
WHERE 
    user_id = 3 AND progreso_id < 115;
   
-- Insertar progreso copiandolo de un usuario 3 hacia otro usuario 30, nada más cierta cantidad de entradas máximo
-- Este específico insert para el estado de la base de datos actual
-- Este es para a punto de llegar al desafío final
INSERT INTO progreso (user_id, fuente_id, fecha, isSuccessful)
SELECT 
    30 AS user_id, 
    fuente_id, 
    fecha, 
    isSuccessful 
FROM 
    progreso 
WHERE 
    user_id = 3 AND progreso_id < 174;
   
CALL GetTotalXP(28);
CALL GetTotalXP(29);
CALL GetTotalXP(30);

COMMIT;

-- SELECT * FROM progreso WHERE user_id = 28;