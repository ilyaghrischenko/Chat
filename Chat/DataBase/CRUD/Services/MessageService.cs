using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;

namespace DataBase.CRUD.Services
{
    public class MessageService : IMessageService
    {
        private readonly ChatDbContext _context;

        public MessageService(ChatDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var existingMessage = await GetByIdAsync(message.Id);
            _context.Messages.Remove(existingMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetAsync(ChatDetail chatDetail)
        {
            if (chatDetail == null)
                throw new ArgumentNullException(nameof(chatDetail));

            var messages = await _context.Messages
                .Where(x => x.ChatDetail == chatDetail)
                .ToListAsync();

            if (!messages.Any())
                throw new Exception("Messages do not exist");

            return messages;
        }

        public async Task<Message> GetByIdAsync(uint id)
        {
            var message = await _context.Messages
                .FirstOrDefaultAsync(x => x.Id == id);

            if (message == null)
                throw new Exception("Message does not exist");

            return message;
        }
    }
}