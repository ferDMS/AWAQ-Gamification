﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;


public class DashboardUserModel : AuthorizedPageModel
{
    public int UserId { get; set; }
    public IActionResult OnGet()
    {

        var authResult = CheckUserAuthorization("User");
        if (authResult != null)
        {
            return authResult;
        }

        UserId = (int)HttpContext.Session.GetInt32("UserId");
        return Page();

    }

     

}

