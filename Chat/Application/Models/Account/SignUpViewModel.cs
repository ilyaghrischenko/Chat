using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class SignUpViewModel
{
    [StringLength(20, MinimumLength = 4)]
    public string Login { get; set; }
    
    [StringLength(20, MinimumLength = 4)]
    public string Password { get; set; }
    
    [StringLength(20, MinimumLength = 4)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public SignUpViewModel() {}
    public SignUpViewModel(string login, string password, string confirmPassword, string email)
    {
        Login = login;
        Password = password;
        ConfirmPassword = confirmPassword;
        Email = email;
    }
}