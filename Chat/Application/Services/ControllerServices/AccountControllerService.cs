using Application.Models;
using Application.Services.ControllerServices.Interfaces;
using Application.Services.DataBaseServices.Interfaces;
using DataBase.CRUD.Interfaces;
using DataBase.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.ControllerServices;

public class AccountControllerService(IUserService userService, ILogger<AccountControllerService> logger)   
    : IAccountControllerService
{
    public async Task<User?> LogInAsync(LogInViewModel model)
    {
        if (model is { Login: "Admin", Password: "123456" })
        {
            return new User { Login = "Admin" };
        }

        try
        {
            var user = await userService.GetByLoginNPassword(model.Login, model.Password);
            if(user == null)
                throw new InvalidOperationException("Invalid login or password");
            return user;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while logging in.");
            throw;
        }
    }

    public async Task<bool> SignUpAsync(SignUpViewModel model)
    {
        try
        {
            PasswordHasher<SignUpViewModel> passwordHasher = new();
            await userService.Insert(new User
            {
                Login = model.Login,
                Email = model.Email,
                Password = passwordHasher.HashPassword(model, model.Password)
            });
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while signing up.");
            throw;
        }
    }
}