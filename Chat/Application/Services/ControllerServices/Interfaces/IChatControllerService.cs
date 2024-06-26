using Application.Models.Chat;
using DataBase.Models;

namespace Application.Services.ControllerServices.Interfaces;

public interface IChatControllerService
{
    Task<List<UsersListViewModel>?> GetAllUsers(int userId);
    Task<ChatDetail> AddChat(int userId, int id);
    Task DeleteChat(int chatId);
    Task<ChatViewModel> GetChatInfo(int userId, int? chatId, int currentLength = 0);
    Task<ChatViewModel> LoadMoreMessages(int userId, int chatId, int currentLength);
}