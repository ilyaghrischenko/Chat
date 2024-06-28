using Application.Services.ControllerServices.Interfaces;
using Application.Services.DataBaseServices.Interfaces;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ControllerServices;

public class AdminControllerService(
    IUserService userService,
    IMessageRepository messageRepository,
    IChatDetailRepository chatDetailRepository,
    IEntityListsRepository entityListsRepository,
    ILogger<AdminControllerService> logger)
    : IAdminControllerService
{
    public async Task DeleteUserWithCascade(int userId)
    {
        var user = await userService.Get(userId);

        if (user != null)
        {
            await userService.Delete(user);
        }
    }

    public async Task DeleteUser(int userId)
    {
        await DeleteUserWithCascade(userId);
    }

    public async Task DeleteChatsAndMessagesForUser(int userId)
    {
        var allChats = await entityListsRepository.GetAllChatDetails();
        var userChats = allChats
            .Where(c => c.User_1Id == userId || c.User_2Id == userId)
            .ToList();
        
        userChats.ForEach(async c =>
        {
            await messageRepository.RemoveRange(c.Messages);
        });
        await chatDetailRepository.RemoveRange(userChats);
    }

    public async Task DeleteUserChats(int userId)
    {
        await DeleteChatsAndMessagesForUser(userId);
    }
}