using RoutingDemo.Data;
using RoutingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace RoutingDemo.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetAll() => _context.Users.ToList();

        public User? GetById(int id) =>
            _context.Users.Find(id);

        public List<User> Search(string name) =>
            _context.Users
                .Where(u => u.Name.Contains(name))
                .ToList();

        public List<User> GetByStatus(bool isActive) =>
            _context.Users
                .Where(u => u.IsActive == isActive)
                .ToList();

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
}