using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoutingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> _users = new()
        {
            new User { Id = 1, Name = "Ian", IsActive = true },
            new User { Id = 2, Name = "Alice", IsActive = false },
            new User { Id = 3, Name = "Brian", IsActive = true }
        };

        // GET: api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_users);
        }

        // GET: api/users/2
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(user);
        }

        // GET: api/users/search?name=Ian
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name)
        {
            var results = _users
                .Where(u => u.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(results);
        }

        // GET: api/users/active/true
        [HttpGet("active/{isActive:bool}")]
        public IActionResult GetByStatus(bool isActive)
        {
            var results = _users
                .Where(u => u.IsActive == isActive)
                .ToList();

            return Ok(results);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult Create([FromBody] User newUser)
        {
            newUser.Id = _users.Max(u => u.Id) + 1;
            _users.Add(newUser);
}