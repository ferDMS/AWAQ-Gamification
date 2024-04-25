using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using APIQuieroSerBiomonitor;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using static APIQuieroSerBiomonitor.Controllers.AuthController;

namespace APIQuieroSerBiomonitor.Controllers
{

    [Route("QSBGame")]
    public class GameController : Controller
    {
        string connectionString = "Server=mysql-351e1a24-tec-965e.a.aivencloud.com;Port=26933;Database=awaqDB;Uid=avnadmin;Password=AVNS_UOl5EfEjlKFL5V5IVyl;SslMode=Required;CertificateFile=ca.cer;";

        [HttpGet("especies")]
        public IEnumerable<Especie> GetAllEspecies()
        {
            List<Especie> especies = new List<Especie>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("GetAllEspecies", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tipoEspecieString = reader.GetString("tipo_especie");
                       

                        if (Enum.TryParse(tipoEspecieString, true, out TipoEspecie tipoEspecieEnum))
                        {
                            especies.Add(new Especie
                            {
                                EspecieId = reader.GetInt32("especie_id"),
                                NombreEspecie = reader.GetString("nombre_especie"),
                                NombreCientifico = reader.GetString("nombre_cientifico"),
                                TipoEspecie = tipoEspecieEnum,
                                Herramienta = new Herramienta
                                {
                                    HerramientaId = reader.GetInt32("herramienta_id"),
                                    XpDesbloqueo = reader.GetInt32("xp_desbloqueo_h"),
                                    NombreHerramienta = reader.GetString("nombre_herramienta"),
                                    Descripcion = reader.GetString("descripcion")
                                },
                                FuenteId = reader.GetInt32("fuente_id"),
                                XpDesbloqueo = reader.GetInt32("xp_desbloqueo_e"),
                                XpExito = reader.GetInt32("xp_exito")
                            });
                        }
                        else
                        {
                            throw new ArgumentException("Invalid enum conversion for especie or fuente types");
                        }
                    }
                }
            }

            return especies;
        }

        [HttpGet("desafios")]
        public ActionResult<List<Desafio>> GetAllDesafios()
        {
            List<Desafio> desafios = new List<Desafio>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("GetAllDesafios", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var herramientaIds = reader["herramienta_ids"] as string;
                        var desafio = new Desafio
                        {
                            DesafioId = reader.GetInt32("desafio_id"),
                            XpDesbloqueo = reader.GetInt32("xp_desbloqueo"),
                            XpExito = reader.GetInt32("xp_exito"),
                            XpFallar = reader.GetInt32("xp_fallar"),
                            HerramientaIds = !string.IsNullOrEmpty(herramientaIds)
                                             ? herramientaIds.Split(',').Select(int.Parse).ToList()
                                             : new List<int>(),
                            FuenteId = reader.GetInt32("fuente_id")
                        };
                        desafios.Add(desafio);
                    }
                }
            }
            return Ok(desafios);
        }

        [HttpGet("herramienta/{id}")]
        public ActionResult<Herramienta> GetHerramientasInfo(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("GetHerramientasInfo", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@HerramientaID", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var herramienta = new Herramienta
                        {
                            HerramientaId = reader.GetInt32("herramienta_id"),
                            NombreHerramienta = reader.GetString("nombre_herramienta"),
                            Descripcion = reader.IsDBNull("descripcion") ? null : reader.GetString("descripcion"),
                            XpDesbloqueo = reader.GetInt32(reader.GetOrdinal("xp_desbloqueo"))
                        };
                        return Ok(herramienta);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        [HttpGet("totalXP/{id}")]
        public ActionResult<int> GetTotalXP(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("GetTotalXP", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@user_id_in", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var XP = reader.GetInt32("total_xp");
                        return Ok(XP);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }


        [HttpGet("biomonitorXP")]
        public ActionResult<int> GetXPBiomonitor()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("GetBiomonitorXP", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var XP = reader.GetInt32("XPValue");
                        return Ok(XP);
                    }
                    else
                    {
                        return NotFound("No XP value found for Biomonitor Achievement.");
                    }
 
                }
            }
        }

        [HttpGet("capturas/{id}")]
        public ActionResult<List<Captura>> GetCapturasByUserID(int id)
        {
            List<Captura> capturas = new List<Captura>();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("GetCapturasByUserID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@user_id_in", id);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        capturas.Add(new Captura
                        {
                            EspecieId = reader.GetInt32(reader.GetOrdinal("especie_id")),
                            NombreEspecie = reader.GetString(reader.GetOrdinal("nombre_especie")),
                            NombreCientifico = reader.GetString(reader.GetOrdinal("nombre_cientifico")),
                            EspecieDescripcion = reader.GetString(reader.GetOrdinal("especie_descripcion")),
                            CaptureCount = reader.GetInt32(reader.GetOrdinal("capture_count"))
                        });
                    }
                }
            }

            return Ok(capturas);
        }

        [HttpPost("PostXPEvent")]
        public IActionResult PostXpEvent([FromBody] XPEvent xpevent)
        {
            int userId = xpevent.UserId;
            int fuenteId = xpevent.FuenteId;
            DateTime fecha = xpevent.Fecha;
            bool isSuccessful = xpevent.IsSuccessful;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand("PostXpEvent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@user_id_in", userId);
                        command.Parameters.AddWithValue("@fuente_id_in", fuenteId);
                        command.Parameters.AddWithValue("@fecha_in", fecha);
                        command.Parameters.AddWithValue("@isSuccessful_in", isSuccessful);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return Ok("XP event posted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

