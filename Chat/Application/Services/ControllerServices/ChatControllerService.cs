using Application.Models.Chat;
using Application.Services.ControllerServices.Interfaces;
using Application.Services.DataBaseServices;

namespace Application.Services.ControllerServices;

public class ChatControllerService(UserService userService, ChatDetailService chatDetailService, ILogger<ChatControllerService> logger)
    : IChatControllerService
{
    public async Task SendMessage(SendMessageViewModel model)
    {
        
    }
}