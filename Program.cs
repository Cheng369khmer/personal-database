using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Data;
using MyPortfolio.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. ── Database Connection ──
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. ── Identity Setup ──
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 3. ── Session & Cache ──
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 4. ── Razor Pages & Auth ──
builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// 5. ── Middleware Pipeline ──
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapRazorPages();

app.MapGet("/", context => {
    context.Response.Redirect("/Index");
    return Task.CompletedTask;
});

// 6. ── Seed Database (single block, before app.Run) ──
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();

        // Test connection before migrating
        if (!db.Database.CanConnect())
        {
            Console.WriteLine(">>> ERROR: Cannot connect to MySQL! Is XAMPP running?");
        }
        else
        {
            db.Database.Migrate();
            await DbSeeder.SeedRolesAndAdminAsync(services);
            Console.WriteLine(">>> Database setup and seeding completed successfully!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(">>> Seeding Error: " + ex.Message);
        Console.WriteLine(">>> Inner:  " + ex.InnerException?.Message);
    }
}

app.Run(); // ← app.Run() is always LAST