namespace Application.Models.Chat;

public class UsersListViewModel(string login, int id)
{
    public string Login { get; set; } = login;
    public int Id { get; set; } = id;
    
    public override string ToString()
        => $"{Login}";
}