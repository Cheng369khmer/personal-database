# 🚀 MyPortfolio — ABU SAID
**ASP.NET Core 8 + MySQL (XAMPP) + Admin & User Roles**

---

## ✅ Features

| Feature | Description |
|---|---|
| 🎨 Portfolio Homepage | Hero, Skills, Projects, Contact sections |
| 🔐 Authentication | Login, Register, Logout with cookie auth |
| 👑 Admin Role | Full dashboard: edit profile, manage projects/skills/messages/users |
| 👤 User Role | Can view portfolio and send contact messages |
| 🗄️ MySQL Database | Powered by XAMPP MySQL via Entity Framework Core |
| 📱 Responsive | Mobile-friendly dark theme UI |

---

## 🛠️ Setup Instructions

### 1. Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [XAMPP](https://www.apachefriends.org/) (MySQL running)
- Visual Studio 2022 or VS Code

### 2. Start XAMPP MySQL
1. Open XAMPP Control Panel
2. Click **Start** next to **MySQL**
3. Open **phpMyAdmin** → http://localhost/phpmyadmin
4. Create database: click **New** → name it `myportfolio_db` → click **Create**

   *Or run the included SQL:*
   ```sql
   CREATE DATABASE myportfolio_db CHARACTER SET utf8mb4;
   ```

### 3. Configure Connection String
Open `appsettings.json` and update if your MySQL has a password:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=myportfolio_db;User=root;Password=YOUR_PASSWORD;"
  }
}
```
*(Default XAMPP MySQL has no password — leave `Password=;` empty)*

### 4. Install EF Core Tools & Run Migrations
```bash
# Install EF tools (once)
dotnet tool install --global dotnet-ef

# Create migration
dotnet ef migrations add InitialCreate

# Apply to database (creates all tables + seeds data)
dotnet ef database update
```

### 5. Run the Project
```bash
dotnet run
```
Visit: **http://localhost:5103**

---

## 🔑 Default Login Accounts

| Role | Email | Password |
|------|-------|----------|
| **Admin** | admin@myportfolio.com | Admin@123456 |
| **User** | user@myportfolio.com | User@123456 |

---

## 📁 Project Structure

```
MyPortfolio/
├── Data/
│   ├── ApplicationDbContext.cs   ← EF Core + MySQL context
│   └── DbSeeder.cs               ← Seeds Admin/User accounts
├── Models/
│   ├── ApplicationUser.cs        ← Identity user model
│   └── PortfolioModels.cs        ← Profile, Skill, Project, Message
├── Pages/
│   ├── Index.cshtml              ← 🏠 Portfolio homepage
│   ├── Account/
│   │   ├── Login.cshtml          ← 🔐 Login page
│   │   ├── Register.cshtml       ← ✍️ Register page
│   │   └── Logout.cshtml         ← Logout handler
│   ├── Admin/
│   │   ├── Index.cshtml          ← 📊 Admin dashboard
│   │   ├── Profile.cshtml        ← 👤 Edit profile
│   │   ├── Projects.cshtml       ← 💻 Manage projects
│   │   ├── Skills.cshtml         ← ⭐ Manage skills
│   │   ├── Messages.cshtml       ← 📨 View messages
│   │   └── Users.cshtml          ← 👥 Manage users
│   └── Shared/
│       ├── _Layout.cshtml        ← Main layout
│       └── _AdminLayout.cshtml   ← Admin sidebar layout
├── wwwroot/
│   ├── css/site.css              ← 🎨 All styles
│   └── js/site.js                ← Interactions
├── Program.cs                    ← App startup + DI config
├── appsettings.json              ← MySQL connection string
└── setup_database.sql            ← SQL reference
```

---

## 🗄️ Database Tables (auto-created by EF)

| Table | Purpose |
|-------|---------|
| `CoderProfiles` | Your name, bio, location, etc. |
| `Skills` | Tech skills with proficiency % |
| `Projects` | Portfolio projects |
| `ContactMessages` | Messages from the contact form |
| `AspNetUsers` | Users (Identity) |
| `AspNetRoles` | Roles: Admin, User |
| `AspNetUserRoles` | User ↔ Role mapping |

---

## 🎨 Customization

### Change Your Profile Info
- Login as Admin → go to **Admin Panel** → **My Profile**
- Or edit the seed data in `Data/ApplicationDbContext.cs`

### Add Projects
- Admin Panel → **Projects** → click **Add Project**

### Change Admin Password
Update in `Data/DbSeeder.cs`:
```csharp
await userManager.CreateAsync(admin, "YourNewPassword@123");
```

---

## 🔒 Role Permissions

| Page | Public | User | Admin |
|------|--------|------|-------|
| Portfolio (/) | ✅ | ✅ | ✅ |
| Contact Form | ✅ | ✅ | ✅ |
| /Admin/* | ❌ | ❌ | ✅ |
| Register | ✅ | - | - |
