using RoutingDemo.Models;

namespace RoutingDemo.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new()
        {
            new User { Id = 1, Name = "Ian", IsActive = true },
            new User { Id = 2, Name = "Alice", IsActive = false },
            new User { Id = 3, Name = "Brian", IsActive = true }
        };

        public List<User> GetAll() => _users;

        public User? GetById(int id) =>
            _users.FirstOrDefault(u => u.Id == id);

        public List<User> Search(string name) =>
            _users.Where(u =>
                u.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        public List<User> GetByStatus(bool isActive) =>
            _users.Where(u => u.IsActive == isActive).ToList();

        public User Create(User user)
        {
            user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
            return user;
        }

        public bool Update(int id, User updatedUser)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.Name = updatedUser.Name;
            existing.IsActive = updatedUser.IsActive;
            return true;
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;

            _users.Remove(user);
            return true;
        }
    }
}
