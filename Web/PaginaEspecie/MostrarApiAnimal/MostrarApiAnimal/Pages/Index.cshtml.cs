using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace MostrarApiAnimal.Pages
{
    public class IndexModel : PageModel
    { 
        public List<Especie> listaEspecies { get; set; }

        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                string url = "https://localhost:7044/QSBWeb/especies";
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    listaEspecies = JsonConvert.DeserializeObject<List<Especie>>(jsonString);
                }
            }
        }
    }
}
