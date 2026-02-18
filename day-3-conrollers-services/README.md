# Day 3 — Controllers + Services (Dependency Injection & Separation of Concerns)

Today we refactor the Day 2 project to introduce a proper Service Layer and Dependency Injection.

Goal:

* Controllers handle HTTP concerns only
* Services handle business logic
* Models represent domain data

---

# Final Project Structure

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
├── Program.cs
└── RoutingDemo.csproj
```

---

# Models/User.cs

```csharp
namespace RoutingDemo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
```

---

# Services/IUserService.cs

```csharp
using RoutingDemo.Models;

namespace RoutingDemo.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User? GetById(int id);
        List<User> Search(string name);
        List<User> GetByStatus(bool isActive);
        User Create(User user);
        bool Update(int id, User user);
        bool Delete(int id);
    }
}
```

---

# Services/UserService.cs

```csharp
using RoutingDemo.Models;

namespace RoutingDemo.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new()
        {
            new User { Id = 1, Name = "Ian", IsActive = true },
            new User { Id = 2, Name = "Alice", IsActive = false },
            new User { Id = 3, Name = "Brian", IsActive = true }
        };

        public List<User> GetAll() => _users;

        public User? GetById(int id) =>
            _users.FirstOrDefault(u => u.Id == id);

        public List<User> Search(string name) =>
            _users.Where(u =>
                u.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        public List<User> GetByStatus(bool isActive) =>
            _users.Where(u => u.IsActive == isActive).ToList();

        public User Create(User user)
        {
            user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
            return user;
        }

        public bool Update(int id, User updatedUser)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.Name = updatedUser.Name;
            existing.IsActive = updatedUser.IsActive;
            return true;
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;

            _users.Remove(user);
            return true;
        }
    }
}
```

---

# Controllers/UsersController.cs

```csharp
using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Models;
using RoutingDemo.Services;

namespace RoutingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name)
        {
            return Ok(_userService.Search(name));
        }

        [HttpGet("active/{isActive:bool}")]
        public IActionResult GetByStatus(bool isActive)
        {
            return Ok(_userService.GetByStatus(isActive));
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            var created = _userService.Create(user);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            var updated = _userService.Update(id, user);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var deleted = _userService.Delete(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
```

---

# Program.cs (Register Dependency Injection)

```csharp
using RoutingDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

# What Changed From Day 2

* Removed business logic from controller
* Introduced service interface (IUserService)
* Implemented service class (UserService)
* Registered dependency in DI container
* Controller now depends on abstraction, not concrete class

---

# Architecture Insight

This pattern enables:

* Testability (mock IUserService)
* Separation of concerns
* Cleaner controllers
* Easier transition to database (EF Core tomorrow)

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
