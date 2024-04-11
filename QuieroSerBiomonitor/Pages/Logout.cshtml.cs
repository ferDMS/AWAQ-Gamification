using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuieroSerBiomonitor.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            // Clear all session data
            HttpContext.Session.Clear();

            // Redirect to the Index page after logout
            return RedirectToPage("/Index");
        }
    }

}

