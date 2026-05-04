using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyPortfolio.Models;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    public LoginModel(SignInManager<ApplicationUser> signInManager)
        => _signInManager = signInManager;

    [BindProperty, Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [BindProperty, Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public bool RememberMe { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid) return Page();

        var result = await _signInManager.PasswordSignInAsync(
            Email, Password, RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            returnUrl ??= "/";
            return LocalRedirect(returnUrl);
        }

        TempData["Error"] = "Invalid email or password. Please try again.";
        return Page();
    }
}
