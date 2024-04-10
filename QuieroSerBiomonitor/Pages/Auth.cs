using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AuthorizedPageModel : PageModel
{
    public IActionResult CheckUserAuthorization(string requiredRole)
    {
        var userRole = HttpContext.Session.GetString("UserRole");

        if (string.IsNullOrEmpty(userRole) || userRole != requiredRole)
        {
            return RedirectToPage("/Index");
        }
        return null;
    }
}
