using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Models;

namespace DataBase.CRUD.Interfaces
{
    public interface IEntityListsRepository
    {
        Task<List<User>> GetAllUsers();
        Task<List<Message>> GetAllMessages();
        Task<List<ChatDetail>> GetAllChatDetails();
    }
}