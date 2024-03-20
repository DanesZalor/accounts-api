using Microsoft.AspNetCore.Mvc;
using Something.Core;

namespace Something.Api.Controllers;

[Route("[controller]")]
[ApiController]
[RequireHttps]
public class AccountController : ControllerBase
{
    private IAccountsRepository _accountsRepository;

    public AccountController(IAccountsRepository accountsRepo)
    {
        _accountsRepository = accountsRepo ?? throw new ArgumentNullException(nameof(accountsRepo));
    }

    // GET: api/<AccountController>
    [HttpGet]
    public ActionResult<IEnumerable<Account>> Get()
    {
        Environment.SetEnvironmentVariable("TESTVAR", $"{Guid.NewGuid()}", EnvironmentVariableTarget.Process);
        return Ok(_accountsRepository.GetAll());
    }

    // GET api/<AccountController>/5
    [HttpGet("{username}")]
    public ActionResult<Account> Get(string username)
    {
        if (_accountsRepository.Exists(username)) 
        {
            return Ok(_accountsRepository.Get(username));
        }

        return NotFound();
    }

    // POST api/<AccountController>
    [HttpPost]
    public ActionResult<object> Post([FromBody] Account newAccount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newAccount.Username);
        ArgumentException.ThrowIfNullOrWhiteSpace(newAccount.Password);
        
        if(_accountsRepository.Exists(newAccount.Username))
        {
            return Conflict($"Account:'{newAccount.Username}' already exists");
        }

        var accountToAdd = new Core.Account(
            username: newAccount.Username, 
            password: newAccount.Password);
        
        _accountsRepository.Add(accountToAdd);
        
        return Created("a", newAccount);
    }
}

