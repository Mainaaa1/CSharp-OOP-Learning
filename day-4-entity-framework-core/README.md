# Day 4 — Entity Framework Core (Database Integration)

Today we replace the in-memory list with a real database using Entity Framework Core.

Goal:

* Introduce DbContext
* Configure EF Core
* Connect to a database
* Perform CRUD using EF
* Prepare foundation for production-ready APIs

---

# Step 1 — Install EF Core Packages

Inside the project folder:

```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet restore
```

(Using SQLite for simplicity.)

---

# Updated Project Structure

```
RoutingDemo/
│
├── Controllers/
│   └── UsersController.cs
│
├── Services/
│   ├── IUserService.cs
│   └── UserService.cs
│
├── Models/
│   └── User.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── Program.cs
└── RoutingDemo.csproj
```

---

# Models/User.cs

```csharp
using System.ComponentModel.DataAnnotations;

namespace RoutingDemo.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
```

---

# Data/AppDbContext.cs

```csharp
using Microsoft.EntityFrameworkCore;
using RoutingDemo.Models;

namespace RoutingDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
    }
}
```

---

# Update Services/UserService.cs (Now Using EF Core)

```csharp
using RoutingDemo.Data;
using RoutingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace RoutingDemo.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll() => _context.Users.ToList();

        public User? GetById(int id) =>
            _context.Users.Find(id);

        public List<User> Search(string name) =>
            _context.Users
                .Where(u => u.Name.Contains(name))
                .ToList();

        public List<User> GetByStatus(bool isActive) =>
            _context.Users
                .Where(u => u.IsActive == isActive)
                .ToList();

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool Update(int id, User updatedUser)
        {
            var existing = _context.Users.Find(id);
            if (existing == null) return false;

            existing.Name = updatedUser.Name;
            existing.IsActive = updatedUser.IsActive;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
```

---

# Update Program.cs (Register DbContext)

```csharp
using Microsoft.EntityFrameworkCore;
using RoutingDemo.Data;
using RoutingDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Register Service
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
```

---

# Create Initial Migration

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

This will:

* Create migration files
* Generate SQLite database file (app.db)

---

# What Changed From Day 3

* Introduced DbContext
* Replaced in-memory list with real database
* Added data annotations for validation
* Registered EF Core with dependency injection
* Added migrations support

---

# Architecture Insight

We now have:

Controller → Service → DbContext → Database

This is the foundation of real-world ASP.NET Core backend systems.

Tomorrow we build a structured API mini-project using this database setup.

---

Run the project:

```
dotnet clean
dotnet run
```

Swagger:

```
https://localhost:<port>/swagger
```

