using Application.Models.Chat;
using Application.Services.ControllerServices.Interfaces;
using Application.Services.DataBaseServices;
using Application.Services.DataBaseServices.Interfaces;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ControllerServices;

public class ChatControllerService(
    IUserService userService,
    IChatDetailService chatService,
    ILogger<ChatControllerService> logger)
    : IChatControllerService
{
    public async Task<ChatDetail> AddChat(int userId, int id)
    {
        var me = await userService.Get(userId);
        var other = await userService.Get(id);

        using ChatDbContext db = new();

        ChatDetail newChat = new(await db.Users.FirstAsync(u => u.Id == me.Id),
            await db.Users.FirstAsync(u => u.Id == other.Id));

        await chatService.Insert(newChat);
        return newChat;
    }

    public async Task DeleteChat(int chatId)
    {
        var chat = await chatService.Get(chatId);

        await chatService.Delete(chat);
    }
}