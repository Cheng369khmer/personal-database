using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin;

[Authorize(Roles = "Admin")]
public class UsersModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UsersModel(UserManager<ApplicationUser> um) => _userManager = um;

    public List<UserWithRole> Users { get; set; } = new();

    public async Task OnGetAsync()
    {
        var allUsers = await _userManager.Users.ToListAsync();
        foreach (var u in allUsers)
        {
            var roles = await _userManager.GetRolesAsync(u);
            Users.Add(new UserWithRole { User = u, Role = roles.FirstOrDefault() ?? "User" });
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null && user.Email != User.Identity?.Name)
        {
            await _userManager.DeleteAsync(user);
            TempData["Success"] = "User deleted.";
        }
        return RedirectToPage();
    }
}

public class UserWithRole
{
    public ApplicationUser User { get; set; } = default!;
    public string Role { get; set; } = "User";
}
