using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Login { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public virtual List<ChatDetail> Chats { get; set; } = new();

        public User() { }

        public User(string login, string password, string email)
        {
            Login = login;
            Password = password;
            Email = email;
        }

        public override string ToString()
            => $"{Login} - {Email}";
    }
}