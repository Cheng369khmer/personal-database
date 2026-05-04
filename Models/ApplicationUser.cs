namespace MyPortfolio.Models;

public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "User"; // "Admin" or "User"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
