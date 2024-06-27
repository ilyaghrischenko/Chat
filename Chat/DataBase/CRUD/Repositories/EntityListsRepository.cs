using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;

namespace DataBase.CRUD.Repositories
{
    public class EntityListsRepository : IEntityListsRepository
    {
        private readonly ChatDbContext _context = new();

        public EntityListsRepository() { }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<Message>> GetAllMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<List<ChatDetail>> GetAllChatDetails()
        {
            return await _context.ChatDetails.ToListAsync();
        }
    }
}