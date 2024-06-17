using System.ComponentModel.DataAnnotations;

namespace DataBase.Models;

public class Message
{
    [Key] public uint Id { get; set; }
    public ChatDetail ChatDetail { get; set; }
    public string MessageContent { get; set; }
    public User User { get; set; }
    public DateTime MessageDate { get; set; }
    
    public Message() { }
    public Message(ChatDetail chatDetail, string messageContent, User user, DateTime messageDate)
    {
        ChatDetail = chatDetail;
        MessageContent = messageContent;
        User = user;
        MessageDate = messageDate;
    }
    
    public override string ToString() =>
    $"{MessageContent}";
}