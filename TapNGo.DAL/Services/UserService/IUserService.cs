using TapNGo.DAL.Models;

namespace TapNGo.DAL.Services.UserService
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetUser(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        User? GetByUsername(string username);
    }
}
