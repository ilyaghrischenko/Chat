using Application.Models.Chat;

namespace Application.Services.ControllerServices.Interfaces;

public interface IChatControllerService
{
    Task SendMessage(SendMessageViewModel model);
}