using Something.Core;

namespace Something.Infra;

public class InMemoryAccountsRepo : IAccountsRepository
{
    private List<Account> _accounts;
    public InMemoryAccountsRepo()
    {
        _accounts = new();
    }

    public Account Get(string username)
    {
        return _accounts.First(account => account.Username.ToUpper() == username.ToUpper());
    }

    public IEnumerable<Account> GetAll()
    {
        return _accounts.AsEnumerable();
    }

    public void Add(Account account)
    {
        _accounts.Add(account);
    }
    
    public bool Exists(string username)
    {
        return _accounts.Any(account => account.Username.ToUpper() == username.ToUpper());
    }
}
