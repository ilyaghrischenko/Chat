using System.ComponentModel.DataAnnotations;

namespace DataBase.Models;

public class User
{
    [Key]
    public uint Id { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string Login { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public User() {    }
    public User(string login, string password, string email)
    {
        Login = login;
        Password = password;
        Email = email;
    }
}