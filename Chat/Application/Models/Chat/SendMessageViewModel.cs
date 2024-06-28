using System.ComponentModel.DataAnnotations;
using DataBase.Models;

namespace Application.Models.Chat;

public class SendMessageViewModel
{
    [Required]
    public string Content { get; set; }
    public ChatDetail? ChatDetail { get; set; } = null;
    public DateTime Date { get; set; } = DateTime.Now;
    
    public override string ToString()
        => $"{Content}";
}