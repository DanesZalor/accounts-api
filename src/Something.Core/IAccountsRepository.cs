namespace Something.Core;

public interface IAccountsRepository
{
    Account Get(string username);
    IEnumerable<Account> GetAll();
    void Add(Account account);
    bool Exists(string username);
}
