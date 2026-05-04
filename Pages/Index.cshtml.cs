using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public IndexModel(ApplicationDbContext db) => _db = db;

    public CoderProfile Profile { get; set; } = default!;
    public List<Skill> Skills { get; set; } = new();
    public List<Project> Projects { get; set; } = new();

    public async Task OnGetAsync()
    {
        Profile = await _db.CoderProfiles.FirstOrDefaultAsync()
            ?? new CoderProfile { Name = "CHENG-IT", Title = "Full Stack Developer" };

        Skills = await _db.Skills.OrderBy(s => s.SortOrder).ToListAsync();
        Projects = await _db.Projects.OrderByDescending(p => p.IsFeatured)
                                     .ThenByDescending(p => p.CreatedAt)
                                     .ToListAsync();
    }

    public async Task<IActionResult> OnPostContactAsync(
        string SenderName, string SenderEmail, string Subject, string Message)
    {
        if (string.IsNullOrWhiteSpace(SenderName) || string.IsNullOrWhiteSpace(SenderEmail)
            || string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(Message))
        {
            TempData["Error"] = "All fields are required.";
            return RedirectToPage();
        }

        var msg = new ContactMessage
        {
            SenderName  = SenderName,
            SenderEmail = SenderEmail,
            Subject     = Subject,
            Message     = Message,
            SentAt      = DateTime.UtcNow
        };

        _db.ContactMessages.Add(msg);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Your message has been sent! I'll get back to you soon.";
        return RedirectToPage();
    }
}
