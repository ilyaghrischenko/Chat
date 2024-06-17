using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;

namespace DataBase.CRUD.Services
{
    public class UserService : IUserService
    {
        private readonly ChatDbContext _context = new();

        public UserService() { }

        public async Task AddAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (GetAsync(user.Login, user.Password) != null)
                throw new Exception("User already exists");

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = await GetByIdAsync(user.Id);
            if (existingUser == null)
                throw new Exception("User does not exist");

            existingUser.Login = user.Login;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (GetAsync(user.Login, user.Password) == null)
                throw new Exception("User does not exist");

            var existingUser = await GetByIdAsync(user.Id);
            if (existingUser == null)
                throw new Exception("User does not exist");

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Login and password must not be null or empty");

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (user == null)
                throw new Exception("User not found");

            return user;
        }

        public async Task<User> GetByIdAsync(uint id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new Exception("User not found");

            return user;
        }
    }
}
