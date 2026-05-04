using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin;

[Authorize(Roles = "Admin")]
public class ProfileModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public ProfileModel(ApplicationDbContext db) => _db = db;

    [BindProperty]
    public CoderProfile Profile { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Profile = await _db.CoderProfiles.FirstOrDefaultAsync()
            ?? new CoderProfile { Name = "CHENG-IT" };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var existing = await _db.CoderProfiles.FirstOrDefaultAsync();
        if (existing == null)
        {
            Profile.UpdatedAt = DateTime.UtcNow;
            _db.CoderProfiles.Add(Profile);
        }
        else
        {
            existing.Name        = Profile.Name;
            existing.Title       = Profile.Title;
            existing.Company     = Profile.Company;
            existing.Location    = Profile.Location;
            existing.Bio         = Profile.Bio;
            existing.Email       = Profile.Email;
            existing.Phone       = Profile.Phone;
            existing.GithubUrl   = Profile.GithubUrl;
            existing.LinkedInUrl = Profile.LinkedInUrl;
            existing.Repositories = Profile.Repositories;
            existing.Hireable    = Profile.Hireable;
            existing.UpdatedAt   = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();
        TempData["Success"] = "Profile updated successfully!";
        return RedirectToPage();
    }
}
