USE sprint2;

DELIMITER //

DROP PROCEDURE IF EXISTS `allTimePerDay`//
CREATE PROCEDURE `admin_hoursPerDay`()
BEGIN
    SELECT
    	SEC_TO_TIME(SUM(TIMESTAMPDIFF(SECOND, s.start_time, s.end_time))) AS `Tiempo`, 
    	DATE(s.end_time) AS `Fecha`
    FROM
    	sesiones s
    GROUP BY
    	DATE(s.end_time);
END//

DROP PROCEDURE IF EXISTS `allXpPerDay`//
CREATE PROCEDURE `admin_xpPerDay`()
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

DELIMITER ;

CALL `admin_hoursPerDay`();
CALL `admin_xpPerDay`();
