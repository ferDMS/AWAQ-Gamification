using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace MostrarApiAnimal.Pages
{
    public class IndexModel : PageModel
    { 
        // List of species that will be filled from the API call
        public List<Especie>? listaEspecies { get; set; }

        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                // Try to get something from the API
                try
                {
                    string url = "https://localhost:7044/QSBWeb/especies";
                    var response = await httpClient.GetAsync(url);
                    // If we are succesfull and obtain data from the API
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the JSON content from the response and match it with our species model
                        // The parsing is actually done into multiple models but the deseralization is very smart about it (apparently)
                        var jsonString = await response.Content.ReadAsStringAsync();
                        listaEspecies = JsonConvert.DeserializeObject<List<Especie>>(jsonString);
                    }
                }

                // If there's an error with the HTTP request, then create a default, empty, list of species
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    listaEspecies = null;
                }
            }
        }
    }
}
