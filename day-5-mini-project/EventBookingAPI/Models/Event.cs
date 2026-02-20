using System.ComponentModel.DataAnnotations;

namespace EventBookingAPI.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string Location { get; set; } = "";

        public DateTime Date { get; set; }

        public int Capacity { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }
}
