using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;

namespace DataBase.CRUD.Repositories
{
    public class ChatDetailRepository : IChatDetailRepository
    {
        private readonly ChatDbContext _context = new();

        public ChatDetailRepository() { }

        public async Task Insert(ChatDetail chatDetail)
        {
            chatDetail.User1 = await _context.Users.FirstAsync(u => u.Id == chatDetail.User_1Id);
            chatDetail.User2 = await _context.Users.FirstAsync(u => u.Id == chatDetail.User_2Id);
            await _context.ChatDetails.AddAsync(chatDetail);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ChatDetail chatDetail)
        {
            _context.ChatDetails.Remove(chatDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<ChatDetail?> GetWithUsers(User user1, User user2) => 
            await _context.ChatDetails.FirstOrDefaultAsync(x => x.User_1Id == user1.Id 
                                                                && x.User_2Id == user2.Id);

        public async Task<ChatDetail?> Get(int id) =>
            await _context.ChatDetails
                .FirstOrDefaultAsync(x => x.Id == id);
        
        public async Task<List<ChatDetail>> GetAllForUser(int userId) => 
            await _context.ChatDetails.Where(c => c.User1.Id.Equals(userId) || c.User2.Id.Equals(userId))
                .ToListAsync();

        public async Task RemoveRange(List<ChatDetail> chatDetails)
        {
            _context.ChatDetails.RemoveRange(chatDetails);
            await _context.SaveChangesAsync();
        }
    }
}