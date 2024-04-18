using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace MostrarApiAnimal.Pages
{
    public class IndexModel : PageModel
    { 
        public List<Specie> listaSpecies { get; set; }

        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string url = "https://localhost:7100/species/Species";
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    listaSpecies = JsonConvert.DeserializeObject<List<Specie>>(jsonString);
                }
            }
        }
    }
}
