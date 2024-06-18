using System.Collections.Concurrent;
using System.Text;
using Application.Models.Chat;
using DataBase.CRUD.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendMessageViewModel model)
    {
        _user = await _userService.GetByIdAsync((uint)TempData["UserId"]);
        model.Date = DateTime.Now;
        
        var fullMessage = $"{model.Date}: {model.Content}\n\n";
        var data = Encoding.UTF8.GetBytes(model.Content);
        foreach (var item in _clients)
        {
            await item.WriteAsync($"data: {fullMessage}\n\n");
            await item.FlushAsync();
        }

        return View("Index");
    }
}