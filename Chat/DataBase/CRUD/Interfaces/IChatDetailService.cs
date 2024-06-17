using System.Threading.Tasks;
using DataBase.Models;

namespace DataBase.CRUD.Interfaces
{
    public interface IChatDetailService
    {
        Task AddAsync(ChatDetail chatDetail);
        Task DeleteAsync(ChatDetail chatDetail);
        Task<ChatDetail> GetAsync(uint user_1, uint user_2);
        Task<ChatDetail> GetByIdAsync(uint id);
    }
}