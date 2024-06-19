using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Models;

namespace DataBase.CRUD.Interfaces
{
    public interface IMessageService
    {
        Task AddAsync(Message message);
        Task DeleteAsync(Message message);
        Task<List<Message>> GetAsync(ChatDetail chatDetail);
        Task<Message> GetByIdAsync(int id);
    }
}