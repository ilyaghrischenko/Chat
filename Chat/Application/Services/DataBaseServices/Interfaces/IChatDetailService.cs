using DataBase.Models;

namespace Application.Services.DataBaseServices.Interfaces;

public interface IChatDetailService
{
    Task Insert(ChatDetail chatDetail);
    Task Delete(ChatDetail chatDetail);
    Task<ChatDetail?> GetWithUsers(User user1, User user2);
    Task<ChatDetail?> Get(int id);
    Task<List<ChatDetail>?> GetAllForUser(int userId);
}