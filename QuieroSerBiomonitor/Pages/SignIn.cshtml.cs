using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

public class SignInModel : PageModel
{
    [BindProperty]
    public string email { get; set; }
    [BindProperty]
    public string pass_word { get; set; }

    public string message { get; set; }

    public void OnPost()
    {
        message = ".";
        string connectionString = "Server=-----;Port=---;Database=awaqgame;Uid=root;password=-----.;";
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();

        // Use parameterized query to avoid SQL injection
        MySqlCommand cmd = new MySqlCommand("Select correo, pass_word from usuarios where correo = @correo limit 1", conexion);
        cmd.Parameters.AddWithValue("@correo", email);

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                if (email == reader["correo"].ToString() && pass_word == reader["pass_word"].ToString())
                {
                    message = "Acceso Correcto";
                    // Set session variables here
                    HttpContext.Session.SetString("UserEmail", email);
                    HttpContext.Session.SetString("UserRole", "User");
                    Response.Redirect("DashboardUser");
                    return; // Exit method to prevent further execution
                }
            }
        }

        // Adjusted for parameterized query
        cmd.CommandText = "Select correo, pass_word from admin where correo = @correo limit 1";
        // cmd.Parameters already contains the needed parameter

        using (var reader2 = cmd.ExecuteReader())
        {
            while (reader2.Read())
                if (email == reader2["correo"].ToString() && pass_word == reader2["pass_word"].ToString())
                {
                    message = "Acceso Administrador";
                    // Set different session variables for admin
                    HttpContext.Session.SetString("UserEmail", email);
                    HttpContext.Session.SetString("UserRole", "Admin");
                    Response.Redirect("DashboardAdmin");
                    return; // Exit method to prevent further execution
                }
        }
        if (message == ".")
        {
            message = "Usuario o Contraseña Incorrectos";
        }
        conexion.Dispose();
    }
}
