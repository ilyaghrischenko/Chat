using System.Threading.Tasks;
using DataBase.Models;

namespace DataBase.CRUD.Interfaces
{
    public interface IChatDetailService
    {
        Task AddAsync(ChatDetail chatDetail);
        Task DeleteAsync(ChatDetail chatDetail);
        Task<ChatDetail> GetAsync(User user1, User user2);
        Task<ChatDetail> GetByIdAsync(int id);
    }
}