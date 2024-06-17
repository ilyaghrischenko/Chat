using System.Collections.Generic;
using System.Threading.Tasks;
using DataBase.Models;

namespace DataBase.CRUD.Interfaces
{
    public interface IEntityListsService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<ChatDetail>> GetAllChatDetailsAsync();
    }
}