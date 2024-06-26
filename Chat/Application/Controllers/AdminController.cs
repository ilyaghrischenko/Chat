using DataBase.Context;
using DataBase.CRUD.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Controllers;

public class AdminController : Controller
{
    private readonly ChatDbContext _context = new();
    private readonly EntityListsService _entityListsService = new();
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<User> users = await _entityListsService.GetAllUsersAsync();
        return View(users);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await DeleteUserWithCascade(id); 
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteUserChats(int id)
    {
        await DeleteChatsAndMessagesForUser(id);
        return RedirectToAction("Index");
    }


    public async Task DeleteUserWithCascade(int userId)
    {
        // Найти пользователя с указанным ID
        var user = await _context.Users
            .Include(u => u.Chats)
            .ThenInclude(c => c.Messages)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user != null)
        {
            // Удалить пользователя и связанные записи
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
    public async Task DeleteChatsAndMessagesForUser(int userId)
    {
        // Найти все чаты, где пользователь является участником
        var chats = await _context.ChatDetails
            .Where(c => c.User_1Id == userId || c.User_2Id == userId)
            .Include(c => c.Messages)
            .ToListAsync();

        // Удалить все сообщения в этих чатах
        foreach (var chat in chats)
        {
            _context.Messages.RemoveRange(chat.Messages);
        }

        // Удалить сами чаты
        _context.ChatDetails.RemoveRange(chats);

        // Сохранить изменения
        await _context.SaveChangesAsync();
    }
}