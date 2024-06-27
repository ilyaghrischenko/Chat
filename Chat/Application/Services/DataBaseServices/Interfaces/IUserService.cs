using DataBase.Models;

namespace Application.Services.DataBaseServices.Interfaces;

public interface IUserService
{
    Task Insert(User user);
    Task Update(User user);
    Task Delete(User user);
    Task<User?> GetByLoginNPassword(string login, string password);
    Task<User?> Get(int id);
}