using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuieroSerBiomonitor;
using System.Threading.Tasks;
using System.Collections.Generic;

public class DashboardAdminModel : AuthorizedPageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DashboardAdminModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public List<User> users { get; set; }

    public async Task LoadUsersAsync()
    {
        var client = _httpClientFactory.CreateClient("BypassSSLClient");
        try
        {
            HttpResponseMessage response = await client.GetAsync("QSB/users"); 
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
}
