using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

public class ChangePasswordModel : AuthorizedPageModel
{
    [BindProperty]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña actual")]
    public string old_password { get; set; }

    [BindProperty]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña nueva")]
    public string new_password { get; set; }

    [BindProperty]
    [DataType(DataType.Password)]
    [Display(Name = "Confirma contraseña nueva")]
    public string newC_password { get; set; }

    public string Message { get; set; }

    string connectionString = System.IO.File.ReadAllText("connectionString.secret");

    public void OnPost()
    {
        if (new_password != newC_password)
        {
            Message = "La nueva contraseña y la confirmación no coinciden.";
            return;
        }

        var userEmail = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(userEmail))
        {
            Message = "No estás logueado.";
            return;
        }

        using (var conexion = new MySqlConnection(connectionString))
        {
            try
            {
                conexion.Open();
                var cmd = new MySqlCommand("SELECT pass_word FROM usuarios WHERE correo = @correo LIMIT 1", conexion);
                cmd.Parameters.AddWithValue("@correo", userEmail);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var currentPassword = reader["pass_word"].ToString();
                        if (old_password != currentPassword)
                        {
                            Message = "La contraseña actual es incorrecta.";
                            return;
                        }
                    }
                    else
                    {
                        Message = "Usuario no encontrado.";
                        return;
                    }
                }

                cmd = new MySqlCommand("UPDATE usuarios SET pass_word = @newPassword WHERE correo = @correo", conexion);
                cmd.Parameters.AddWithValue("@newPassword", new_password);
                cmd.Parameters.AddWithValue("@correo", userEmail);

                cmd.ExecuteNonQuery(); 
                Message = "Contraseña actualizada correctamente.";
            }
            catch (MySqlException ex)
            {
                Message = "Error al conectar con la base de datos.";
            }
        }
    }

    public IActionResult OnGet()
    {
        var authResult = CheckUserAuthorization("User");
        if (authResult != null)
        {
            return authResult;
        }
        return Page();
    }


}
