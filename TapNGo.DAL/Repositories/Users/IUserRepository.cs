﻿using TapNGo.DAL.Models;

namespace TapNGo.DAL.Repositories.Users
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        User? GetByUsername(string username);
    }
}
