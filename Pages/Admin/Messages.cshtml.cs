using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;

namespace MyPortfolio.Pages.Admin;

[Authorize(Roles = "Admin")]
public class MessagesModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public MessagesModel(ApplicationDbContext db) => _db = db;

    public List<ContactMessage> Messages { get; set; } = new();

    public async Task OnGetAsync()
    {
        Messages = await _db.ContactMessages
            .OrderByDescending(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostMarkReadAsync(int id)
    {
        var msg = await _db.ContactMessages.FindAsync(id);
        if (msg != null) { msg.IsRead = true; await _db.SaveChangesAsync(); }
        TempData["Success"] = "Message marked as read.";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var msg = await _db.ContactMessages.FindAsync(id);
        if (msg != null) { _db.ContactMessages.Remove(msg); await _db.SaveChangesAsync(); }
        TempData["Success"] = "Message deleted.";
        return RedirectToPage();
    }
}
