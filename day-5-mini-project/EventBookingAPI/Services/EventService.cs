using EventBookingAPI.Data;
using EventBookingAPI.Models;

namespace EventBookingAPI.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public List<Event> GetAll()
        {
            return _context.Events.ToList();
        }

        public Event Create(Event newEvent)
        {
            _context.Events.Add(newEvent);
            _context.SaveChanges();
            return newEvent;
        }

        public Event? GetById(int id)
        {
            return _context.Events.Find(id);
        }
    }
}
