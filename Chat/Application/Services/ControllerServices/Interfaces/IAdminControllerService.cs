namespace Application.Services.ControllerServices.Interfaces;

public interface IAdminControllerService
{
    Task DeleteUserWithCascade(int userId);
    Task DeleteUser(int userId);
    Task DeleteChatsAndMessagesForUser(int userId);
    Task DeleteUserChats(int userId);
}