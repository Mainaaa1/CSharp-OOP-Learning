using RoutingDemo.Models;

namespace RoutingDemo.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User? GetById(int id);
        List<User> Search(string name);
        List<User> GetByStatus(bool isActive);
        User Create(User user);
        bool Update(int id, User user);
        bool Delete(int id);
    }
}
