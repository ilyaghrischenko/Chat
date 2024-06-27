using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Identity;

namespace DataBase.CRUD.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _context = new();

        public UserRepository()
        {
        }

        public async Task Insert(User user)
        {
            // if (user == null)
            //     throw new ArgumentNullException(nameof(user));
            //
            // var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == user.Login);
            // if (existingUser != null)
            //     throw new Exception("User with this login already exists");
            //
            // try
            // {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            // }
            // catch (Exception ex)
            // {
            //     throw new Exception("Failed to add user");
            // }
        }

        public async Task Update(User user)
        {
            var existingUser = await Get(user.Id);

            existingUser.Login = user.Login;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByLogin(string login) => await _context.Users.FirstOrDefaultAsync(x => x.Login.Equals(login));

        public async Task<User?> Get(int id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
}
