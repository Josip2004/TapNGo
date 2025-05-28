using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapNGo.Models;

namespace TapNGo.DAL.Services.UserService
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetUser(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
