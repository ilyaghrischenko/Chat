using Application.Services.DataBaseServices.Interfaces;
using DataBase.CRUD.Interfaces;
using DataBase.CRUD.Repositories;
using DataBase.Models;

namespace Application.Services.DataBaseServices;

public class ChatDetailService(IChatDetailRepository chatDetailRepository) : IChatDetailService
{
    public async Task Insert(ChatDetail chatDetail)
    {
        ArgumentNullException.ThrowIfNull(chatDetail);

        if (chatDetail.User_1Id == chatDetail.User_2Id)
        {
            throw new InvalidOperationException("You can't create a chat with yourself.");
        }

        if (await chatDetailRepository.GetWithUsers(chatDetail.User1, chatDetail.User2) != null)
        {
            throw new InvalidOperationException("This chat already exists.");
        }
        
        await chatDetailRepository.Insert(chatDetail);
    }

    public async Task Delete(ChatDetail chatDetail)
    {
        if (chatDetail == null)
        {
            throw new ArgumentNullException("This chat already deleted");
        }

        await chatDetailRepository.Delete(chatDetail);
    }

    public async Task<ChatDetail?> GetWithUsers(User user1, User user2)
    {
        if (user1 == null || user2 == null)
        {
            throw new ArgumentNullException(user1 == null ? nameof(user1) : nameof(user2));
        }

        if (user1.Id == user2.Id)
        {
            throw new InvalidOperationException("You can't get a chat with yourself.");
        }
        
        return await chatDetailRepository.GetWithUsers(user1, user2);
    }

    public async Task<ChatDetail?> Get(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

        return await chatDetailRepository.Get(id);
    }

    public async Task<List<ChatDetail>?> GetAllForUser(int userId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId);

        return await chatDetailRepository.GetAllForUser(userId);
    }
}
