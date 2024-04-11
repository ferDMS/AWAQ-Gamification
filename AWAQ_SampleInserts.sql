
-- ------------------------------------------------------------------------------------------
-- Tablas para guardar la información del progreso de los usuarios mientras juegan
-- Estas tablas unifican las dos secciones anteriores de usuarios y mecánicas del juego
-- ------------------------------------------------------------------------------------------

USE sprint2;

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

-- ------------------------------------------------------------------------------------------

