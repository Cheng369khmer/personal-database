using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public IndexModel(ApplicationDbContext db) => _db = db;

    public int ProjectCount { get; set; }
    public int SkillCount { get; set; }
    public int MessageCount { get; set; }
    public int UserCount { get; set; }
    public List<ContactMessage> RecentMessages { get; set; } = new();

    public async Task OnGetAsync()
    {
        ProjectCount  = await _db.Projects.CountAsync();
        SkillCount    = await _db.Skills.CountAsync();
        MessageCount  = await _db.ContactMessages.CountAsync();
        UserCount     = await _db.Users.CountAsync();
        RecentMessages = await _db.ContactMessages
            .OrderByDescending(m => m.SentAt)
            .Take(5)
            .ToListAsync();
    }
}
