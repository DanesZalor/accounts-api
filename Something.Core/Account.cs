namespace Something.Core;
public class Account
{
    public string Username {get; init;}
    public string Password {get; init;}

    public Account(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
