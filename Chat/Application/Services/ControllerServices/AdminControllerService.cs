using Application.Services.ControllerServices.Interfaces;
using Application.Services.DataBaseServices.Interfaces;
using DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ControllerServices;

public class AdminControllerService(
    IUserService userService,
    IChatDetailService chatDetailService,
    ILogger<AdminControllerService> logger)
    : IAdminControllerService
{
    private async Task DeleteUserWithCascade(int userId)
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

    //CHANGE!!!
    private async Task DeleteChatsAndMessagesForUser(int userId)
    {
        await using (var _context = new ChatDbContext())
        {
            var chats = await _context.ChatDetails
                .Where(c => c.User_1Id == userId || c.User_2Id == userId)
                .Include(c => c.Messages)
                .ToListAsync();

            foreach (var chat in chats)
            {
                _context.Messages.RemoveRange(chat.Messages);
            }

            _context.ChatDetails.RemoveRange(chats);

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteUserChats(int userId)
    {
        await DeleteChatsAndMessagesForUser(userId);
    }
}