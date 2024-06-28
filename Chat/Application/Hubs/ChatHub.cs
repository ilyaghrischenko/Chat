using Application.Services.DataBaseServices;
using Application.Services.DataBaseServices.Interfaces;
using DataBase.Context;
using DataBase.CRUD.Repositories;
using DataBase.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Application.Hubs;

public class ChatHub(IChatDetailService chatDetailService, IMessageService messageService, IUserService userService)
    : Hub
{
    private readonly ChatDbContext _context = new();
    
    
    public async Task JoinChat(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }

    public async Task SendMessage(string userId, string chatId, string message)
    {
        await Clients.Group(chatId).SendAsync("ReceiveMessage", userId, message);
        
        // User user = await _context.Users.FirstAsync(x => x.Id == int.Parse(userId));
        // ChatDetail chat = await _context.ChatDetails.FirstAsync(x => x.Id == int.Parse(chatId));
        // Message mes = new Message(chat, message, user, DateTime.Now);
        // await _context.Messages.AddAsync(mes);
        // await _context.SaveChangesAsync();
        
        User user = await _context.Users.FirstAsync(x => x.Id == int.Parse(userId));
        ChatDetail chat = await _context.ChatDetails.FirstAsync(x => x.Id == int.Parse(chatId));
        Message mes = new Message(chat, message, user, DateTime.Now);
        await messageService.Insert(mes);
    }

}