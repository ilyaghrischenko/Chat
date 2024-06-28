using System.Collections.Concurrent;
using System.Text;
using Application.Models.Chat;
using Application.Services.DataBaseServices;
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
    private readonly UserService _userService = new(new UserRepository());
    private readonly ChatDetailService _chatService = new(new ChatDetailRepository());
    private User _user;

    public ChatController(ILogger<ChatController> logger)
    {
        _logger = logger;
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

        ChatDbContext db = new();
        model.ChatDetail = await db.ChatDetails.FirstAsync(c => c.Id == int.Parse(TempData["ChatId"].ToString()));
        _user = await _userRepository.Get(int.Parse(TempData["UserId"].ToString()));
        Message message = new(model.ChatDetail, model.Content, await db.Users.FirstAsync(u => u.Id == _user.Id),
            model.Date);
        await db.Messages.AddAsync(message);
        await db.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AddChat()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddChat(int id)
    {
        var me = await _userService.Get(int.Parse(TempData["UserId"].ToString()));
        var other = await _userService.Get(id);

        using ChatDbContext db = new();

        var meFromDb = await db.Users.FirstAsync(u => u.Id == me.Id);
        var otherFromDb = await db.Users.FirstAsync(u => u.Id == other.Id);

        var chatDetail = new ChatDetail(meFromDb, otherFromDb);

        await db.AddAsync(chatDetail);
        await db.SaveChangesAsync();
        TempData["ChatId"] = chatDetail.Id;

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteChat(int id)
    {
        var chat = await _chatService.Get(int.Parse(TempData["ChatId"].ToString()));
        TempData["ChatId"] = null;
        await _chatService.Delete(chat);
        return RedirectToAction("Index");
    }
}