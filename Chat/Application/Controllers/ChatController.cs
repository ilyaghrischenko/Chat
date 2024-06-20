using System.Collections.Concurrent;
using System.Text;
using Application.Models.Chat;
using DataBase.Context;
using DataBase.CRUD.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.Controllers;

public class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;
    
    private readonly UserService _userService = new();
    private User _user;
    private static readonly ConcurrentBag<StreamWriter> _clients = new ConcurrentBag<StreamWriter>();
    
    public ChatController(ILogger<ChatController> logger)
    {
        _logger = logger;
    }

    private async Task<User> GetUser()
    {
        return await _userService.GetByIdAsync(int.Parse(TempData["UserId"].ToString()));
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendMessageViewModel model)
    {
        model.Date = DateTime.Now;
        _user = await _userService.GetByIdAsync(int.Parse(TempData["UserId"].ToString()));
        Message message = new(model.ChatDetail, model.Content, _user, model.Date);
        ChatDbContext db = new();
        model.ChatDetail = await db.ChatDetails.FirstAsync(c => c.Id == 1);
        await db.AddAsync(message);
        await db.SaveChangesAsync();

        return View("Index");
    }
}