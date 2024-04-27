using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuieroSerBiomonitor;
using System.Threading.Tasks;
using System.Collections.Generic;

public class DashboardAdminModel : AuthorizedPageModel
{
    // Lista de usuarios que se despliegan en la vista general
    [BindProperty]
    public List<User> users { get; set; }

    // Obtener los usuarios de la base de datos e inicializar la lista
    public async Task LoadUsersAsync()
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                string url = "https://localhost:7044/QSB/users/";

                HttpResponseMessage response = await httpClient.GetAsync(url); 
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(jsonString);
                }
                else
                {
                    users = new List<User>(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching users: {ex.Message}");
                users = new List<User>();
            }
        }
    }

    // Método llamado al entrar a la página
    public async Task<IActionResult> OnGetAsync()
    {
        var authResult = CheckUserAuthorization("Admin");
        if (authResult != null)
        {
            return authResult;
        }

        await LoadUsersAsync();
        return Page();
    }

    // Método llamado al seleccionar borrar a un usuario
    public async Task<IActionResult> OnPostDelete()
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                string userId_str = Request.Form["userId"];
                string url = "https://localhost:7044/QSB/users/deactivate/" + userId_str;

                HttpResponseMessage response = await httpClient.PutAsync(url, null);

                // Checar si se logró borrar el usuario
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Deletion was successful.");
                }
                else
                {
                    Console.WriteLine($"Deletion failed with status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Al final de la operación, recargar la página para que se muestre el
        // dashboard actualizado, con el borrado del usuario reflejado.
        return RedirectToPage("/DashboardAdmin");
    }
}
