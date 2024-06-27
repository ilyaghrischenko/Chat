using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;

namespace DataBase.CRUD.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context = new();

        public MessageRepository() { }

        public async Task Insert(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Message message)
        {
            var existingMessage = await Get(message.Id);
            _context.Messages.Remove(existingMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetByChatDetail(ChatDetail chatDetail) =>
            await _context.Messages
                .Where(x => x.ChatDetail == chatDetail)
                .ToListAsync();

        public async Task<Message> Get(int id) =>
             await _context.Messages
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}