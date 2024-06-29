using Application.Models.Chat;
using Application.Services.ControllerServices.Interfaces;
using Application.Services.DataBaseServices;
using Application.Services.DataBaseServices.Interfaces;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.CRUD.Repositories;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ControllerServices;

public class ChatControllerService(
    IUserService userService,
    IChatDetailService chatService,
    IEntityListsRepository entityListRepository,
    ILogger<ChatControllerService> logger)
    : IChatControllerService
{
    public async Task<List<UsersListViewModel>?> GetAllUsers(int userId)
    {
        var user = await userService.Get(userId);

        var myChats = await chatService.GetAllForUser(user.Id);
        var users = (await entityListRepository
                .GetAllUsers())
            .Where(u => u.Id != user.Id)
            .ToList();

        List<UsersListViewModel> usernames = new();
        users.ForEach(u =>
        {
            if (!myChats.Any(c => c.User_1Id == u.Id || c.User_2Id == u.Id))
            {
                usernames.Add(new(u.Login, u.Id));
            }
        });
        
        return usernames;
    }

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