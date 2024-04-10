using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;


public class DashboardAdminModel : AuthorizedPageModel
{
    [BindProperty]
    public string email { get; set; }
    [BindProperty]
    public string pass_word { get; set; }

    public string Mensaje { get; set; }

    public void OnPost()
    {
        Mensaje = ".";
        string connectionString = "Server=127.0.0.1;Port=3306;Database=awaqgame;Uid=root;password=Sofia021204.;";
        MySqlConnection conexion = new MySqlConnection(connectionString);
        conexion.Open();

        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = conexion;
        cmd.CommandText = "Select correo,pass_word from usuarios where correo='" + email + "'limit 1;";

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                if (email == reader["correo"].ToString() && pass_word == reader["pass_word"].ToString())
                {
                    Mensaje = "Acceso Correcto";
                }
            }
        }
        cmd.CommandText = "Select correo,pass_word from admin where correo='" + email + "'limit 1;";
        using (var reader2 = cmd.ExecuteReader())
        {
            while(reader2.Read())
            if (email == reader2["correo"].ToString() && pass_word == reader2["pass_word"].ToString())
            {
                Mensaje = "Acceso Administrador";
            }
        }
        if (Mensaje == ".")
        {
            Mensaje = "Usuario o Contraseña Incorrectos";
        }
        conexion.Dispose();
    }

    public IActionResult OnGet()
    {
        var authResult = CheckUserAuthorization("Admin");
        if (authResult != null)
        {
            return authResult;
        }
        return Page();
    }

}


