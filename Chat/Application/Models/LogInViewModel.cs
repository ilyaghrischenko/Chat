using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class LogInViewModel
{
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string Login { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 4)]
    public string Password { get; set; }
    
    public LogInViewModel() {}
    public LogInViewModel(string login, string password)
    {
        Login = login;
        Password = password;
    }
}