using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;

namespace DataBase.CRUD.Services
{
    public class ChatDetailService : IChatDetailService
    {
        private readonly ChatDbContext _context;

        public ChatDetailService(ChatDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(ChatDetail chatDetail)
        {
            if (chatDetail == null)
                throw new ArgumentNullException(nameof(chatDetail));

            await _context.ChatDetails.AddAsync(chatDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ChatDetail chatDetail)
        {
            if (chatDetail == null)
                throw new ArgumentNullException(nameof(chatDetail));

            _context.ChatDetails.Remove(chatDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<ChatDetail> GetAsync(uint user_1, uint user_2)
        {
            var chatDetail = await _context.ChatDetails
                .FirstOrDefaultAsync(x => x.User_1Id == user_1 && x.User_2Id == user_2);

            if (chatDetail == null)
                throw new Exception("ChatDetail does not exist");

            return chatDetail;
        }

        public async Task<ChatDetail> GetByIdAsync(uint id)
        {
            var chatDetail = await _context.ChatDetails
                .FirstOrDefaultAsync(x => x.Id == id);

            if (chatDetail == null)
                throw new Exception("ChatDetail does not exist");

            return chatDetail;
        }
    }
}