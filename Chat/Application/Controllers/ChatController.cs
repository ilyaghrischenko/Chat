using System.Collections.Concurrent;
using System.Text;
using Application.Models.Chat;
using DataBase.Context;
using DataBase.CRUD.Repositories;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.Controllers;

public class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;

    private readonly UserRepository _userRepository = new();
    private User _user;

    public ChatController(ILogger<ChatController> logger)
    {
        _logger = logger;
    }

    private async Task<User> GetUser()
    {
        return await _userRepository.Get(int.Parse(TempData["UserId"].ToString()));
    }

    [HttpGet]
    public IActionResult Index(int? id)
    {
        if (id != null) TempData["ChatId"] = id;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendMessageViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        
        model.Date = DateTime.Now;
        ChatDbContext db = new();
        model.ChatDetail = await db.ChatDetails.FirstAsync(c => c.Id == 1);
        _user = await _userRepository.Get(int.Parse(TempData["UserId"].ToString()));
        Message message = new(model.ChatDetail, model.Content, await db.Users.FirstAsync(u => u.Id == _user.Id),
            model.Date);
        model.ChatDetail = await db.ChatDetails.FirstAsync(c => c.Id == 1);
        await db.Messages.AddAsync(message);
        await db.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}