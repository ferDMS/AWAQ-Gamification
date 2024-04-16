using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel.DataAnnotations;

public class RegModel : AuthorizedPageModel
{
    [BindProperty] public string nombre { get; set; }
    [BindProperty] public string apellido { get; set; }
    [BindProperty] public string genero { get; set; }
    [BindProperty] public string pais { get; set; }
    [BindProperty] public string ciudad { get; set; }
    [BindProperty] public DateTime fechaNacimiento { get; set; }
    [BindProperty] public string correo { get; set; }
    [BindProperty] public string pass_word { get; set; }

    public string Message { get; set; }

    public void OnPost()
    {
        string connectionString = System.IO.File.ReadAllText("connectionString.secret");
        MySqlConnection conexion = new MySqlConnection(connectionString);
        
        conexion.Open();
        var lastLogin = DateTime.Now; 
           
        var cmd = new MySqlCommand("INSERT INTO usuarios (nombre, apellido, genero, pais, ciudad, fechaNacimiento, correo, pass_word, lastLogin) VALUES (@nombre, @apellido, @genero, @pais, @ciudad, @fechaNacimiento, @correo, @pass_word, @lastLogin)", conexion);
        cmd.Parameters.AddWithValue("@nombre", nombre);
        cmd.Parameters.AddWithValue("@apellido", apellido);
        cmd.Parameters.AddWithValue("@genero", genero);
        cmd.Parameters.AddWithValue("@pais", pais);
        cmd.Parameters.AddWithValue("@ciudad", ciudad);
        cmd.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
        cmd.Parameters.AddWithValue("@correo", correo);
        cmd.Parameters.AddWithValue("@pass_word", pass_word);
        cmd.Parameters.AddWithValue("@lastLogin", lastLogin);

        try
        {
            cmd.ExecuteNonQuery();
            Message = "Registro exitoso. Por favor, inidique al usuario.";
                
        }
        catch (MySqlException ex)
        {
            Message = $"Error al registrar el usuario. Detalle del error: {ex.Message}";
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
