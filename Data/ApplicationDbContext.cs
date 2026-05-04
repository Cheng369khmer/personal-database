using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models;

namespace MyPortfolio.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<CoderProfile> CoderProfiles => Set<CoderProfile>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed default profile
        builder.Entity<CoderProfile>().HasData(new CoderProfile
        {
            Id = 1,
            Name = "CHENG IT",
            Title = "Full Stack Developer",
            Company = "Teton Private Limited",
            Location = "Dhaka, Bangladesh",
            Bio = "Passionate full-stack developer with expertise in modern web technologies. I build scalable, high-performance applications with clean code and great user experiences.",
            Repositories = 86,
            Hireable = true,
            GithubUrl = "https://github.com/cheng369khmer",
            LinkedInUrl = "https://linkedin.com/in/cheng369khmer",
            Email = "cheng369khmer@example.com",
            Phone = "+855 96 5218 700",
            AvatarPath = "/images/avatar.png",
            UpdatedAt = new DateTime(2024, 1, 1)
        });

        // Seed skills
        builder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "React", Category = "Frontend", Proficiency = 90, SortOrder = 1 },
            new Skill { Id = 2, Name = "Next.js", Category = "Frontend", Proficiency = 85, SortOrder = 2 },
            new Skill { Id = 3, Name = "Redux", Category = "Frontend", Proficiency = 80, SortOrder = 3 },
            new Skill { Id = 4, Name = "Express.js", Category = "Backend", Proficiency = 85, SortOrder = 4 },
            new Skill { Id = 5, Name = "NestJS", Category = "Backend", Proficiency = 80, SortOrder = 5 },
            new Skill { Id = 6, Name = "MySQL", Category = "Database", Proficiency = 85, SortOrder = 6 },
            new Skill { Id = 7, Name = "MongoDB", Category = "Database", Proficiency = 80, SortOrder = 7 },
            new Skill { Id = 8, Name = "PostgreSQL", Category = "Database", Proficiency = 75, SortOrder = 8 },
            new Skill { Id = 9, Name = "Docker", Category = "DevOps", Proficiency = 75, SortOrder = 9 },
            new Skill { Id = 10, Name = "AWS", Category = "DevOps", Proficiency = 70, SortOrder = 10 }
        );

        // Seed sample projects
        builder.Entity<Project>().HasData(
            new Project
            {
                Id = 1,
                Title = "E-Commerce Platform",
                Description = "Full-stack e-commerce app with React frontend, NestJS backend, and PostgreSQL database. Features include cart, payment integration, and admin panel.",
                Technologies = "React, NestJS, PostgreSQL, Docker",
                GithubUrl = "https://github.com/cheng369khmer/ecommerce",
                LiveUrl = "https://example.com",
                IsFeatured = true,
                CreatedAt = new DateTime(2024, 1, 1)
            },
            new Project
            {
                Id = 2,
                Title = "Task Management App",
                Description = "Real-time collaborative task manager with drag-and-drop boards, team workspaces, and notifications using WebSockets.",
                Technologies = "Next.js, Express, MongoDB, Socket.IO",
                GithubUrl = "https://github.com/cheng369khmer/taskmanager",
                IsFeatured = true,
                CreatedAt = new DateTime(2024, 2, 1)
            },
            new Project
            {
                Id = 3,
                Title = "DevOps Dashboard",
                Description = "AWS infrastructure monitoring dashboard with real-time metrics, alerts, and deployment pipeline visualization.",
                Technologies = "React, Node.js, AWS SDK, Docker",
                GithubUrl = "https://github.com/cheng369khmer/devops-dash",
                IsFeatured = false,
                CreatedAt = new DateTime(2024, 3, 1)
            }
        );
    }
}
