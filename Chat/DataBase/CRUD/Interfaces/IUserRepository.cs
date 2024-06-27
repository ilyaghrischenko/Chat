using System.Threading.Tasks;
using DataBase.Models;

namespace DataBase.CRUD.Interfaces
{
    public interface IUserRepository
    {
        Task Insert(User user);
        Task Update(User user);
        Task Delete(User user);
        Task<User?> GetByLogin(string login);
        Task<User?> Get(int id);
    }
}