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
        private readonly ChatDbContext _context = new();

        public ChatDetailService() { }

        public async Task AddAsync(ChatDetail chatDetail)
        {
            if (chatDetail == null)
                throw new ArgumentNullException(nameof(chatDetail));
            if (await GetAsync(chatDetail.User1, chatDetail.User2) != null)
                throw new Exception("ChatDetail already exists");

            await _context.ChatDetails.AddAsync(chatDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ChatDetail chatDetail)
        {
            if (chatDetail == null)
                throw new ArgumentNullException(nameof(chatDetail));
            if (await GetAsync(chatDetail.User1, chatDetail.User2) == null)
                throw new Exception("ChatDetail does not exist");

            _context.ChatDetails.Remove(chatDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<ChatDetail> GetAsync(User user1, User user2)
        {
            var chatDetail = await _context.ChatDetails
                .FirstOrDefaultAsync(x => x.User1.Id == user1.Id && x.User2.Id == user2.Id);

            if (chatDetail == null)
                throw new Exception("ChatDetail does not exist");

            return chatDetail;
        }

        public async Task<ChatDetail> GetByIdAsync(int id)
        {
            var chatDetail = await _context.ChatDetails
                .FirstOrDefaultAsync(x => x.Id == id);

            if (chatDetail == null)
                throw new Exception("ChatDetail does not exist");

            return chatDetail;
        }
    }
}