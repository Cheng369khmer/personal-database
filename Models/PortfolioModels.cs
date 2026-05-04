using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models;

public class CoderProfile
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Company { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Bio { get; set; } = string.Empty;

    public int Repositories { get; set; }
    public bool Hireable { get; set; }

    [MaxLength(200)]
    public string GithubUrl { get; set; } = string.Empty;

    [MaxLength(200)]
    public string LinkedInUrl { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    // Avatar image path
    [MaxLength(300)]
    public string AvatarPath { get; set; } = string.Empty;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class Skill
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Category { get; set; } = string.Empty; // Frontend, Backend, Database, DevOps

    public int Proficiency { get; set; } = 80; // 0-100 percent

    [MaxLength(50)]
    public string IconClass { get; set; } = string.Empty; // e.g. devicon class

    public int SortOrder { get; set; }
}

public class Project
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Technologies { get; set; } = string.Empty;

    [MaxLength(300)]
    public string GithubUrl { get; set; } = string.Empty;

    [MaxLength(300)]
    public string LiveUrl { get; set; } = string.Empty;

    [MaxLength(300)]
    public string ImagePath { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class ContactMessage
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string SenderName { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string SenderEmail { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Subject { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}
