using DataBase.Models;

namespace Application.Models.Chat;

public class ChatViewModel(ChatDetail? currentChat, List<Contact>? contacts, List<Message>? messages, User user)
{
    public ChatDetail? CurrentChat { get; set; } = currentChat;
    public List<Contact>? Contacts { get; set; } = contacts;
    public List<Message>? Messages { get; set; } = messages;
    public User User { get; set; } = user;
}