using System.ComponentModel.DataAnnotations;

namespace EventBookingAPI.DTOs
{
    public class CreateEventDto
    {
        [Required]
        public string Title { get; set; } = "";

        public string Location { get; set; } = "";

        public DateTime Date { get; set; }

        public int Capacity { get; set; }
    }
}
