using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    public class ChatDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int User_1Id { get; set; }
        
        [ForeignKey("User_1Id")]
        public User User1 { get; set; }

        [Required]
        public int User_2Id { get; set; }
        
        [ForeignKey("User_2Id")]
        public User User2 { get; set; }

        public virtual List<Message> Messages { get; set; } = new();

        public ChatDetail() { }

        public ChatDetail(User user1, User user2)
        {
            User1 = user1;
            User_1Id = user1.Id;
            User2 = user2;
            User_2Id = user2.Id;
        }
    }
}