# Day 5 — API Mini Project (ASP.NET Core)

## Objective

Build a small REST API applying everything learned so far:

* Routing
* Controllers
* Services
* Dependency Injection
* Entity Framework Core

The project simulates a simple **Event Booking API**.

---

# Project Structure

```
EventBookingApi
 ├── Controllers
 │    └── EventsController.cs
 ├── Services
 │    └── EventService.cs
 ├── Data
 │    └── AppDbContext.cs
 ├── Models
 │    └── Event.cs
 ├── Program.cs
```

---

# Model

```csharp
namespace EventBookingApi.Models;

public class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public int Capacity { get; set; }
}
```

---

# DbContext

```csharp
using Microsoft.EntityFrameworkCore;
using EventBookingApi.Models;

namespace EventBookingApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events => Set<Event>();
}
```

---

# Service Layer

```csharp
using EventBookingApi.Data;
using EventBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApi.Services;

public class EventService
{
    private readonly AppDbContext _context;

    public EventService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> GetAll()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<Event?> Get(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task<Event> Create(Event ev)
    {
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();
        return ev;
    }

    public async Task<bool> Delete(int id)
    {
        var ev = await _context.Events.FindAsync(id);

        if (ev == null)
            return false;

        _context.Events.Remove(ev);
        await _context.SaveChangesAsync();

        return true;
    }
}
```

---

# Controller

```csharp
using Microsoft.AspNetCore.Mvc;
using EventBookingApi.Models;
using EventBookingApi.Services;

namespace EventBookingApi.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly EventService _service;

    public EventsController(EventService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _service.GetAll();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var ev = await _service.Get(id);

        if (ev == null)
            return NotFound();

        return Ok(ev);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Event ev)
    {
        var created = await _service.Create(ev);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
```

---

# Program.cs

```csharp
using EventBookingApi.Data;
using EventBookingApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("EventsDb"));

builder.Services.AddScoped<EventService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
```

---

# Run The Project

```bash
dotnet run
```

Open Swagger:

```
http://localhost:5000/swagger
```

---

# Example Request

POST /api/events

```json
{
  "title": "Tech Conference",
  "location": "Nairobi",
  "date": "2026-03-01T10:00:00",
  "capacity": 200
}
```

---

# Concepts Practiced

* ASP.NET Controllers
* Routing
* Dependency Injection
* Services Layer
* Entity Framework Core
* REST API design
