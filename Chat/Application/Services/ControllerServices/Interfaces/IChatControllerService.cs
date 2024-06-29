using Application.Models.Chat;
using DataBase.Models;

namespace Application.Services.ControllerServices.Interfaces;

public interface IChatControllerService
{
    Task<List<UsersListViewModel>?> GetAllUsers(int userId);
    Task<ChatDetail> AddChat(int userId, int id);
    Task DeleteChat(int chatId);
}