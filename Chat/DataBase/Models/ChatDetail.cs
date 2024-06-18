using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models;

public class ChatDetail
{
    [Key]
    public uint Id { get; set; }
    [ForeignKey("User")]
    public uint User_1Id { get; set; }
    [ForeignKey("User")]
    public uint User_2Id { get; set; }

    public virtual List<Message> Messages { get; set; } = new();
    
    public ChatDetail() { }
    public ChatDetail(uint user_1, uint user_2)
    {
        User_1Id = user_1;
        User_2Id = user_2;
    }
}