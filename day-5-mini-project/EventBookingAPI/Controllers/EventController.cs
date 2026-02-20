using Microsoft.AspNetCore.Mvc;
using EventBookingAPI.Services;
using EventBookingAPI.Models;
using EventBookingAPI.DTOs;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Create(CreateEventDto dto)
        {
            var newEvent = new Event
            {
                Title = dto.Title,
                Location = dto.Location,
                Date = dto.Date,
                Capacity = dto.Capacity
            };

            var created = _service.Create(newEvent);

            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}
