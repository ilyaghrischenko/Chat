using DataBase.Models;

namespace Application.Services.DataBaseServices.Interfaces;

public interface IMessageService
{
    Task Insert(Message message);
    Task Delete(Message message);
    Task<List<Message>> GetByChatDetail(ChatDetail chatDetail);
    Task<Message> Get(int id);
}