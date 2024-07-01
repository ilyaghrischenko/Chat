using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Models;

namespace DataBase.CRUD.Interfaces
{
    public interface IMessageRepository
    {
        Task Insert(Message message);
        Task Delete(Message message);
        Task<List<Message>> GetByChatDetail(ChatDetail chatDetail);
        Task<Message?> Get(int id);
        Task RemoveRange(List<Message> messages);
        Task<List<Message>> GetLast50Messages(ChatDetail chatDetail);
    }
}