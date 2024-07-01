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
    IChatControllerService chatControllerService,
    IUserService userService)
    : Controller
{
    private User _user;
    
    [HttpGet]
    public async Task<IActionResult> Index(int? id)
    {
        if (id != null)
        {
            TempData["ChatId"] = id;
        }
        
        var userId = int.Parse(TempData["UserId"].ToString());
        int? chatId = null;
        try
        {
            chatId = int.Parse(TempData["ChatId"].ToString());
        }
        catch (NullReferenceException)
        {
            chatId = null;
        }

        var currentLength = TempData["CurrentLength"] == null ? 0 : int.Parse(TempData["CurrentLength"].ToString());
        var chatInfo = await chatControllerService.GetChatInfo(userId, chatId, currentLength);
        TempData["CurrentLength"] = chatInfo.Messages?.Count;
        TempData["UserId"] = userId;
        TempData["ChatId"] = chatId;
        
        return View(chatInfo);
    }

    [HttpGet]
    public async Task<IActionResult> AddChat()
    {
        var userId = int.Parse(TempData["UserId"].ToString());
        var allUsers = await chatControllerService.GetAllUsers(userId);
        TempData["UserId"] = userId;
        return View(allUsers);
    }

    [HttpPost]
    public async Task<IActionResult> AddChat(int id)
    {
        var newChat = await chatControllerService.AddChat(int.Parse(TempData["UserId"].ToString()), id);
        TempData["ChatId"] = newChat.Id;

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteChat()
    {
        try
        {
            await chatControllerService.DeleteChat(int.Parse(TempData["ChatId"].ToString()));
        }
        catch (ArgumentNullException ex)
        {
            return RedirectToAction("Index");
        }

        TempData["ChatId"] = null;
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> LoadMoreMessages()
    {
        var userId = int.Parse(TempData["UserId"].ToString());
        var chatId = int.Parse(TempData["ChatId"].ToString());
        var currentLength = int.Parse(TempData["CurrentLength"].ToString());
    
        var newChat = await chatControllerService.LoadMoreMessages(userId, chatId, currentLength);
        TempData["CurrentLength"] = newChat.Messages?.Count;
        TempData["UserId"] = userId;
        TempData["ChatId"] = chatId;

        return RedirectToAction("Index", new { id = chatId });
    }
}