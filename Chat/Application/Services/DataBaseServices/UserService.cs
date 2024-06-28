using Application.Services.DataBaseServices.Interfaces;
using DataBase.CRUD.Interfaces;
using DataBase.CRUD.Repositories;
using DataBase.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.DataBaseServices;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task Insert(User user)
    {
        var existingUser = await userRepository.GetByLogin(user.Login);
        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this login already exists");
        }

        await userRepository.Insert(user);
    }

    public async Task Update(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        if (await userRepository.Get(user.Id) == null)
            throw new KeyNotFoundException("User with this ID doesn't exist");

        await userRepository.Update(user);
    }

    public async Task Delete(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        await userRepository.Delete(user);
    }

    public async Task<User?> GetByLoginNPassword(string login, string password)
    {
        ArgumentNullException.ThrowIfNull(login);
        ArgumentNullException.ThrowIfNull(password);

        var user = await userRepository.GetByLogin(login);
        if (user == null || new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, password) ==
            PasswordVerificationResult.Failed)
            return null;
        return user;
    }

    public async Task<User?> Get(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
        return await userRepository.Get(id);
    }
}