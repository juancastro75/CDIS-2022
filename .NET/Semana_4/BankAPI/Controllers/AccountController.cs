using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;

namespace BankAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AccountController : ControllerBase
{
    private readonly AccountService accountService;

    public AccountController(AccountService account)
    {
        accountService = account;
    }

    [HttpGet]
    public IEnumerable<Account> Get()
    {
        return accountService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Account> GetById(int id)
    {
        var account = accountService.GetById(id);

        if(account is null)
            return NotFound();

        return account;
    }

    [HttpPost]
    public IActionResult Create([FromBody] AccountsModel account)
    {
        var newClient = accountService.Create(account); 

        return CreatedAtAction(nameof(GetById), new {id = account.Id}, account);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] AccountsModel account)
    {
        if (id != account.Id)
            return BadRequest();
        
        var accountToUpdate = accountService.GetById(id);
        if(accountToUpdate is not null)
        {
            accountService.Update(id, account);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var accountToDelete = accountService.GetById(id);
        if(accountToDelete is not null)
        {
            accountService.Delete(id);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}