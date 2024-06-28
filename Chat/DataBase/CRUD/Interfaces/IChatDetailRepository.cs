using System.Threading.Tasks;
using DataBase.Models;

namespace DataBase.CRUD.Interfaces
{
    public interface IChatDetailRepository
    {
        Task Insert(ChatDetail chatDetail);
        Task Delete(ChatDetail chatDetail);
        Task<ChatDetail?> GetWithUsers(User user1, User user2);
        Task<ChatDetail?> Get(int id);
        Task<List<ChatDetail>> GetAllForUser(int userId);
        Task RemoveRange(List<ChatDetail> chatDetails);
    }
}