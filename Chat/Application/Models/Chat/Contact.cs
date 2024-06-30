namespace Application.Models.Chat;

public class Contact(string login, int chatDetailId)
{
    public string Login { get; set; } = login;
    public int ChatDetailId { get; set; } = chatDetailId;

    public override string ToString()
    => Login;
}