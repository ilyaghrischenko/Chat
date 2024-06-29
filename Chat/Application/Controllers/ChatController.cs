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

public class ChatController(IChatControllerService chatControllerService)
    : Controller
{
    private User _user;

    [HttpGet]
    public IActionResult Index(int? id)
    {
        if (id != null) TempData["ChatId"] = id;
        return View();
    }

    [HttpGet]
    public IActionResult AddChat()
    {
        return View();
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
}