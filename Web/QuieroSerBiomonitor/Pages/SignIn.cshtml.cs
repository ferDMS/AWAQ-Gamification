using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class SignInModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SignInModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string Message { get; set; }

    [BindProperty]
    public int UserId { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        HttpClient client = _httpClientFactory.CreateClient("BypassSSLClient");
        var postData = new
        {
            email = Email,
            password = Password
        };

        var content = new StringContent(JsonConvert.SerializeObject(postData), System.Text.Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync("https://172.20.10.8:7044/QSB/login", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var loginResult = JsonConvert.DeserializeObject<LoginResult>(result);

                if (loginResult.UserId > 0)
                {
                    UserId = loginResult.UserId;
                    HttpContext.Session.SetString("UserEmail", Email);
                    HttpContext.Session.SetInt32("UserId", loginResult.UserId);
                    HttpContext.Session.SetString("UserRole", loginResult.Role);
                    return RedirectToPage(loginResult.Role == "Admin" ? "DashboardAdmin" : "DashboardUser");

                    
                }
            }
            else
            {
                var errorResult = JsonConvert.DeserializeObject<ErrorResult>(await response.Content.ReadAsStringAsync());
                Message = errorResult.Error;
            }
        }
        catch (Exception ex)
        {
            Message = $"An error occurred: {ex.Message}";
        }

        return Page();
    }

    public class LoginResult
    {
        public int UserId { get; set; }
        public string Role { get; set; }
    }

    public class ErrorResult
    {
        public string Error { get; set; }
    }
}
