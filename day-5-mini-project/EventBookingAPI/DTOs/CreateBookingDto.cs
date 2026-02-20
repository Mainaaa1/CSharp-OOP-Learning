using System.ComponentModel.DataAnnotations;

namespace EventBookingAPI.DTOs
{
    public class CreateBookingDto
    {
        [Required]
        public string UserName { get; set; } = "";

        public int EventId { get; set; }
    }
}
