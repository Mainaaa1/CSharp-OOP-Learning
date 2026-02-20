using EventBookingAPI.Models;

namespace EventBookingAPI.Services
{
    public interface IBookingService
    {
        Booking Create(Booking booking);
        List<Booking> GetAll();
    }
}
