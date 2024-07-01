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
    IMessageService messageService,
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

    public async Task<ChatViewModel> GetChatInfo(int userId, int? chatId, int currentLength = 0)
    {
        UserService userService = new UserService(new UserRepository());
        ChatDetailService chatDetailService = new ChatDetailService(new ChatDetailRepository());
        MessageService messageService = new MessageService(new MessageRepository());

        var user = await userService.Get(userId);

        var chats = await chatDetailService.GetAllForUser(user.Id);

        int id = (chatId == null) ? 0 : chatId.Value;
        ChatDetail? currentChat = id == 0 ? null : await chatDetailService.Get(id);

        List<Contact> contacts = new List<Contact>();
        foreach (var chat in chats)
        {
            if (chat.Id == currentChat?.Id) continue;

            var contactUserId = chat.User_1Id == user.Id ? chat.User_2Id : chat.User_1Id;
            var contactUser = await userService.Get(contactUserId);
            contacts.Add(new Contact(contactUser.Login, chat.Id));
        }

        var messages = (currentChat == null)
            ? new()
            : await messageService.GetMessagesByLength(currentChat, currentLength + 50);
    
        return new ChatViewModel(currentChat,
            contacts,
            messages,
            user);
    }

    public async Task<ChatViewModel> LoadMoreMessages(int userId, int chatId, int currentLength)
    {
        var chat = await chatService.Get(chatId);
        var allMessages = await messageService.GetByChatDetail(chat);
        return await GetChatInfo(userId, chatId, currentLength);
    }
}