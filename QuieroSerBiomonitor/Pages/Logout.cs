using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LogoutModel : PageModel
{
    public IActionResult OnPost()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("/Index");
    }
}
