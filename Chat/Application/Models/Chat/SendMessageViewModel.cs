using System.ComponentModel.DataAnnotations;
using DataBase.Models;

namespace Application.Models.Chat;

public class SendMessageViewModel
{
    [Required]
    public string Content { get; set; }
    public ChatDetail ChatDetail { get; set; }
    public DateTime Date { get; set; }
    
    public SendMessageViewModel() {}
    public SendMessageViewModel(ChatDetail chatDetail, string content)
    {
        ChatDetail = chatDetail;
        Content = content;
        Date = DateTime.Now;
    }

    public override string ToString()
        => $"{Content}";
}