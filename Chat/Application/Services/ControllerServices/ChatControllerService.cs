using Application.Models.Chat;
using Application.Services.ControllerServices.Interfaces;
using Application.Services.DataBaseServices;
using DataBase.Context;
using DataBase.CRUD.Interfaces;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ControllerServices;

public class ChatControllerService(ILogger<ChatControllerService> logger)
    : IChatControllerService
{
    public async Task SendMessage(SendMessageViewModel model)
    {
        await Task.Delay(1);
    }
}