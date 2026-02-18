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
}
