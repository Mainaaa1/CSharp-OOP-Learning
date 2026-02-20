using EventBookingAPI.Models;

namespace EventBookingAPI.Services
{
    public interface IEventService
    {
        List<Event> GetAll();
        Event Create(Event newEvent);
        Event? GetById(int id);
    }
}
