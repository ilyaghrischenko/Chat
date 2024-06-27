using Application.Services.DataBaseServices.Interfaces;
using DataBase.CRUD.Interfaces;
using DataBase.CRUD.Repositories;
using DataBase.Models;

namespace Application.Services.DataBaseServices;

public class MessageService(IMessageRepository messageRepository) : IMessageService
{
    public async Task Insert(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }

        if (message.ChatDetail.User1.Id == message.ChatDetail.User2.Id)
        {
            throw new InvalidOperationException("You can't send a message to yourself.");
        }
        
        await messageRepository.Insert(message);
    }

    public async Task Delete(Message message)
    {
        if (message == null)
        {
            throw new ArgumentNullException(nameof(message));
        }
        
        await messageRepository.Delete(message);
    }

    public async Task<List<Message>> GetByChatDetail(ChatDetail chatDetail)
    {
        if (chatDetail == null)
        {
            throw new ArgumentNullException(nameof(chatDetail));
        }
        
        return await messageRepository.GetByChatDetail(chatDetail);
    }

    public async Task<Message> Get(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        
        return await messageRepository.Get(id);
    }
}