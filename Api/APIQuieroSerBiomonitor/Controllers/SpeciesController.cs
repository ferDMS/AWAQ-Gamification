using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace APIQuieroSerBiomonitor.Controllers
{
    [Route("QSBWeb")]
    public class SpeciesController : ControllerBase
    {
        string connectionString = System.IO.File.ReadAllText("connectionString.secret");

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
                                Descripcion = reader.GetString("descripcion"),
                                Url_img = reader.GetString("url_img"),
                                Color = reader.GetString("color"),
                                Tamagno = reader.GetString("tamagno"),
                                Region = new Region
                                {
                                    RegionId = reader.GetInt32("region_id"),
                                    NombreRegion = reader.GetString("nombre_region")
                                },
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
                                XpExito = reader.GetInt32("xp_exito"),
                                XpFallar = reader.GetInt32("xp_fallar")
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
    }

}
