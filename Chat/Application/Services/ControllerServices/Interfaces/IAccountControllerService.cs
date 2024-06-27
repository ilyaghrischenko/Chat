using Application.Models;
using DataBase.Models;

namespace Application.Services.ControllerServices.Interfaces;

public interface IAccountControllerService
{
    Task<User?> LogInAsync(LogInViewModel model);
    Task<bool> SignUpAsync(SignUpViewModel model);
}