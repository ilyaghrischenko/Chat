using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChatDetailId { get; set; }
        
        [ForeignKey("ChatDetailId")]
        public ChatDetail ChatDetail { get; set; }

        [Required]
        public string MessageContent { get; set; }

        [Required]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public DateTime MessageDate { get; set; }

        public Message() { }

        public Message(ChatDetail chatDetail, string messageContent, User user, DateTime messageDate)
        {
            ChatDetail = chatDetail;
            ChatDetailId = chatDetail.Id;
            MessageContent = messageContent;
            User = user;
            UserId = user.Id;
            MessageDate = messageDate;
        }

        public override string ToString()
            => $"{MessageContent}";
    }
}