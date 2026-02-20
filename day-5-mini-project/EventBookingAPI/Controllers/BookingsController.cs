using Microsoft.AspNetCore.Mvc;
using EventBookingAPI.Services;
using EventBookingAPI.Models;
using EventBookingAPI.DTOs;

namespace EventBookingAPI.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingsController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Create(CreateBookingDto dto)
        {
            var booking = new Booking
            {
                UserName = dto.UserName,
                EventId = dto.EventId
            };

            return Ok(_service.Create(booking));
        }
    }
}
