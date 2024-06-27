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
            await _context.ChatDetails.AddAsync(chatDetail);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ChatDetail chatDetail)
        {
            _context.ChatDetails.Remove(chatDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<ChatDetail?> GetWithUsers(User user1, User user2) => 
            await _context.ChatDetails.FirstOrDefaultAsync(x => x.User1.Id.Equals(user1.Id) 
                                                                && x.User2.Id.Equals(user2.Id));

        public async Task<ChatDetail?> Get(int id) =>
            await _context.ChatDetails
                .FirstOrDefaultAsync(x => x.Id == id);
        
        public async Task<List<ChatDetail>> GetAllForUser(int userId) => 
            await _context.ChatDetails.Where(c => c.User1.Id.Equals(userId) || c.User2.Id.Equals(userId))
                .ToListAsync();
    }
}