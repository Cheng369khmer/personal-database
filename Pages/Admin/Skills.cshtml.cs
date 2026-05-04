using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin;

[Authorize(Roles = "Admin")]
public class SkillsModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public SkillsModel(ApplicationDbContext db) => _db = db;

    public List<Skill> Skills { get; set; } = new();

    public async Task OnGetAsync()
    {
        Skills = await _db.Skills.OrderBy(s => s.Category).ThenBy(s => s.SortOrder).ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync(string Name, string Category, int Proficiency)
    {
        if (!string.IsNullOrWhiteSpace(Name))
        {
            _db.Skills.Add(new Skill
            {
                Name = Name,
                Category = Category,
                Proficiency = Math.Clamp(Proficiency, 10, 100),
                SortOrder = await _db.Skills.CountAsync() + 1
            });
            await _db.SaveChangesAsync();
            TempData["Success"] = "Skill added!";
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var skill = await _db.Skills.FindAsync(id);
        if (skill != null) { _db.Skills.Remove(skill); await _db.SaveChangesAsync(); }
        TempData["Success"] = "Skill deleted.";
        return RedirectToPage();
    }
}
