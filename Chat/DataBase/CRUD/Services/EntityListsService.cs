using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;

namespace DataBase.CRUD.Services
{
    public class EntityListsService : IEntityListsService
    {
        private readonly ChatDbContext _context = new();

        public EntityListsService() { }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<List<ChatDetail>> GetAllChatDetailsAsync()
        {
            return await _context.ChatDetails.ToListAsync();
        }
    }
}