
-- ------------------------------------------------------------------------------------------
-- Tablas para guardar la información del progreso de los usuarios mientras juegan
-- Estas tablas unifican las dos secciones anteriores de usuarios y mecánicas del juego
-- ------------------------------------------------------------------------------------------

USE sprint2;

TRUNCATE TABLE progreso_especies;
TRUNCATE TABLE progreso_desafios;

-- Tabla para salvar el progreso del usuario en materia de cada captura / avistamiento de especies
-- Óptimamente ingresaríamos datos a esta table con inserts de múltiples entradas correspondientes a una sesión de juego
INSERT INTO progreso_especies(user_id, especie_id, fecha) VALUES
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

-- Tabla para salvar el progreso del usuario en materia de desafíos completados
INSERT INTO progreso_desafios(desafio_id, user_id, start_time, end_time) VALUES
	(1, 1, '2024-04-01 06:50:26', '2024-04-01 06:57:26'),
	(2, 1, '2024-04-02 18:50:26', '2024-04-02 18:59:26'),
	(1, 2, '2024-04-01 11:00:23', '2024-04-01 11:07:27');

-- ------------------------------------------------------------------------------------------

