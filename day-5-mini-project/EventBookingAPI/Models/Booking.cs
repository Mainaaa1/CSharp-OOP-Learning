using System.ComponentModel.DataAnnotations;

namespace EventBookingAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = "";

        public int EventId { get; set; }

        public Event? Event { get; set; }
    }
}
