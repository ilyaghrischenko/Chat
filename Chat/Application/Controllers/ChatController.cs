using System.Collections.Concurrent;
using System.Text;
using Application.Models.Chat;
using Application.Services.ControllerServices.Interfaces;
using Application.Services.DataBaseServices;
using Application.Services.DataBaseServices.Interfaces;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.CRUD.Repositories;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.Controllers;

public class ChatController(
    IUserRepository userRepository,
    IUserService userService,
    IChatDetailService chatService,
    IChatControllerService chatControllerService)
    : Controller
{
    private User _user;

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
        _user = await userRepository.Get(int.Parse(TempData["UserId"].ToString()));
        Message message = new(model.ChatDetail, model.Content, await db.Users.FirstAsync(u => u.Id == _user.Id),
            model.Date);
        await db.Messages.AddAsync(message);
        await db.SaveChangesAsync();
        
        await chatControllerService.SendMessage(model);

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
        var me = await userService.Get(int.Parse(TempData["UserId"].ToString()));
        var other = await userService.Get(id);

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
        var chat = await chatService.Get(int.Parse(TempData["ChatId"].ToString()));
        TempData["ChatId"] = null;
        await chatService.Delete(chat);
        return RedirectToAction("Index");
    }
}