USE awaqDB;

START TRANSACTION;

DELIMITER //

-- ------------------------------------------------------------------------------------------
-- Procedures para los dashboards desarrollados en Looker Studio
-- ESTOS PROCEDURES NO SE LLAMAN, únicamente se utilizan los querys como data source
-- ------------------------------------------------------------------------------------------
-- Para gráficas de horas por día
DROP PROCEDURE IF EXISTS `allTimePerDay`//
CREATE PROCEDURE `allTimePerDay`()
BEGIN
    SELECT
    	SEC_TO_TIME(SUM(TIMESTAMPDIFF(SECOND, s.start_time, s.end_time))) AS `Tiempo`, 
    	DATE(s.end_time) AS `Fecha`
    FROM
    	sesiones s
    GROUP BY
    	DATE(s.end_time);
END//
-- Para gráficas de xp por fecha (días, horas, etc.)
DROP PROCEDURE IF EXISTS `allXpPerDay`//
CREATE PROCEDURE `allXpPerDay`()
BEGIN
	SELECT
	    SUM(f.xp_exito) AS `XP Total`,
	    DATE(fecha) AS `Fecha`
	FROM
	    progreso p
	LEFT JOIN
	    fuentes_xp f ON p.fuente_id = f.fuente_id
	GROUP BY
	    DATE(fecha);
END//
-- ------------------------------------------------------------------------------------------

DELIMITER ;

COMMIT;