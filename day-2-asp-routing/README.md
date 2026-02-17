# C# & ASP.NET Core Learning Journey

## Day 2 — ASP.NET Core Routing

**Date:** Tue, 17 Feb
**Focus:** Understanding ASP.NET Core Routing System

---

## Overview

Today’s session focused on how routing works in ASP.NET Core and how HTTP requests are mapped to endpoints.

Key concepts covered:

* What routing is
* Endpoint routing in ASP.NET Core
* Attribute routing
* Route parameters
* Query string parameters
* Route constraints
* RESTful route design

Routing is the foundation of Web API development. Understanding it clearly prevents structural issues later when building controllers and services.

---

## Project Setup

Project Type: ASP.NET Core Web API

Create project:

```
dotnet new webapi -n RoutingDemo
cd RoutingDemo
```

Run the project:

```
dotnet run
```

---

## Project Structure (Relevant Files)

```
RoutingDemo/
│
├── Controllers/
│   └── UsersController.cs
│
├── Program.cs
└── RoutingDemo.csproj
```

---

## Minimal Routing (Program.cs)

ASP.NET Core uses endpoint routing configured in `Program.cs`.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
```

`MapControllers()` enables attribute-based routing inside controllers.

---

## Attribute Routing Example

### UsersController.cs

```csharp
using Microsoft.AspNetCore.Mvc;

namespace RoutingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // GET: api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("All users returned");
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"User with ID: {id}");
        }

        // GET: api/users/search?name=Ian
        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            return Ok($"Searching for user: {name}");
        }

        // GET: api/users/active/true
        [HttpGet("active/{isActive:bool}")]
        public IActionResult GetByStatus(bool isActive)
        {
            return Ok($"Users active: {isActive}");
        }
    }
}
```

---

## Routing Concepts Explained

### Route Templates

```
[Route("api/[controller]")]
```

* `[controller]` token automatically resolves to the controller name without the "Controller" suffix.
* `UsersController` → `api/users`

---

### Route Parameters

```
[HttpGet("{id}")]
```

* `{id}` is a route parameter.
* Value is automatically bound to method parameter.

Example:

```
GET /api/users/10
```

---

### Query Parameters

```
GET /api/users/search?name=Ian
```

Bound automatically to method parameters by model binding.

---

### Route Constraints

```
{isActive:bool}
```

Constraints ensure correct data types.

Common constraints:

* `int`
* `bool`
* `guid`
* `datetime`
* `minlength(x)`

---

### RESTful Design Principles

Good routing follows REST conventions:

* GET → retrieve data
* POST → create resource
* PUT → update resource
* DELETE → remove resource

Example:

```
GET    /api/users
GET    /api/users/5
POST   /api/users
PUT    /api/users/5
DELETE /api/users/5
```

---

## Testing Endpoints

Use:

* Browser (for GET requests)
* Postman
* curl
* Swagger UI (enabled by default in development)

Swagger runs at:

```
https://localhost:<port>/swagger
```

---

## Architectural Insight

Routing is responsible for:

1. Matching incoming HTTP requests
2. Extracting route data
3. Invoking the correct controller action
4. Enabling model binding

Clean routing design improves API readability, maintainability, and scalability.

---

## Next Session

Wed — Controllers + Services

We will separate business logic from controllers and introduce dependency injection.

---

## Key Takeaways

* Routing maps URLs to actions
* Attribute routing provides fine-grained control
* Route parameters and query strings are automatically bound
* Constraints improve validation and robustness
* Proper RESTful routing is essential for scalable APIs
