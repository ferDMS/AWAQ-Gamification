using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiPaginaAnimal.Controllers
{
    [Route("species/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        string connectionString = "Server=127.0.0.1;Port=3306;Database=awaqdb;Uid=root;password=  ;";
        // GET: species/<SpeciesController>
        [HttpGet]
        public IEnumerable<Specie> Get()
        {
            MySqlConnection conexion = new MySqlConnection(connectionString);
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conexion;
            cmd.CommandText = "select e.nombre_especie, e.nombre_cientifico, e.descripcion, e.url_img, e.color, e.tamagno, r.nombre_region as \"region\",tde.tipo, h.nombre_herramienta from especies e inner join herramientas h on e.herramienta_id = h.herramienta_id inner join regiones r on e.region_id = r.region_id inner join tipos_de_especies tde on e.especie_tipo_id = tde.especie_tipo_id ";

            Specie specie1 = new Specie();
            List<Specie> listaSpecies = new List<Specie>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    specie1 = new Specie();
                    specie1.nombre = Convert.ToString(reader["nombre_especie"]);
                    specie1.nombre_cientifico = Convert.ToString(reader["nombre_cientifico"]);
                    specie1.descripcion = Convert.ToString(reader["descripcion"]);
                    specie1.url_img = Convert.ToString(reader["url_img"]);
                    specie1.color = Convert.ToString(reader["color"]);
                    specie1.tamagno = Convert.ToString(reader["tamagno"]);
                    specie1.region = Convert.ToString(reader["region"]);
                    specie1.tipo = Convert.ToString(reader["tipo"]);
                    specie1.herramienta = Convert.ToString(reader["nombre_herramienta"]);

                    listaSpecies.Add(specie1);
                }
            }
            conexion.Dispose();

            return listaSpecies;
        }
    }

}
