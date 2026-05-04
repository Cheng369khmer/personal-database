using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin;

[Authorize(Roles = "Admin")]
public class ProjectsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public ProjectsModel(ApplicationDbContext db) => _db = db;

    public List<Project> Projects { get; set; } = new();

    public async Task OnGetAsync()
    {
        Projects = await _db.Projects.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync(
        string Title, string Description, string Technologies,
        string? GithubUrl, string? LiveUrl, bool IsFeatured)
    {
        if (!string.IsNullOrWhiteSpace(Title))
        {
            _db.Projects.Add(new Project
            {
                Title = Title,
                Description = Description ?? "",
                Technologies = Technologies ?? "",
                GithubUrl = GithubUrl ?? "",
                LiveUrl = LiveUrl ?? "",
                IsFeatured = IsFeatured,
                CreatedAt = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
            TempData["Success"] = "Project added successfully!";
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var project = await _db.Projects.FindAsync(id);
        if (project != null)
        {
            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Project deleted.";
        }
        return RedirectToPage();
    }
}
