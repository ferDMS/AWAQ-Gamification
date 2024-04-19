using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace APIQuieroSerBiomonitor.Controllers
{
   
    [Route("QSB")]
    public class AuthController : ControllerBase
    {
        string connectionString = System.IO.File.ReadAllText("connectionString.secret");

        [HttpGet("users")]
        public IEnumerable<User> getUsers()
        {
            List<User> users = new List<User>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("getUsers", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            UserId = reader.GetInt32("user_id"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Genero = reader.GetString("genero"),
                            Pais = reader.GetString("pais"),
                            Ciudad = reader.GetString("ciudad"),
                            Correo = reader.GetString("correo"),
                            Password = reader.GetString("pass_word"),
                            LastLogin = reader.GetDateTime("lastLogin")
                        };
                        users.Add(user);
                    }
                }
                return users;
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest loginRequest)
        {
            string email = loginRequest.Email;
            string password = loginRequest.Password;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Check if the user is an admin
                bool isAdmin = CheckIfExists(connection, "checkAdminExistsByEmail", email);
                if (isAdmin)
                {
                    return VerifyCredentials(connection, "verifyAdminCredentials", email, password, "Admin");
                }

                // Check if the user is a regular user
                bool isUser = CheckIfExists(connection, "checkUserExistsByEmail", email);
                if (isUser)
                {
                    return VerifyCredentials(connection, "verifyUserCredentials", email, password, "User");
                }

                // If not, return user not found
                return NotFound(new { Error = "Usuario no fue encontrado." });
            }
        }

        [HttpPut("users/deactivate/{id}")]
        public void DeleteUser(int id)
        {
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conexion;
                cmd.CommandText = "DeleteUser";
                cmd.Parameters.AddWithValue("@user_id_in", id);
                cmd.ExecuteNonQuery();
            }
        }


        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }


        private bool CheckIfExists(MySqlConnection connection, string procedureName, string email)
        {
            using (var cmd = new MySqlCommand(procedureName, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("email_in", email);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        private ActionResult VerifyCredentials(MySqlConnection connection, string procedureName, string email, string password, string role)
        {
            using (var cmd = new MySqlCommand(procedureName, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("correo_in", email);
                cmd.Parameters.AddWithValue("pass_word_in", password);

                object result = cmd.ExecuteScalar();
                if (result != null && Convert.ToInt32(result) > 0)
                {
                    return Ok(new { UserId = Convert.ToInt32(result), Role = role });
                }
                else
                {
                    return Unauthorized(new { Error = "Usuario o Contraseña son Incorrectos." });
                }
            }
        }
    }
}
