using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;

namespace BankAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AccountController : ControllerBase
{
    private readonly AccountService accountService;
    private readonly AccountTypeService accountTypeService;
    private readonly ClientService clientService;

    public AccountController(AccountService accountService, 
                            AccountTypeService accountTypeService,
                            ClientService clientService)
    {
        this.accountService = accountService;
        this.accountTypeService = accountTypeService;
        this.clientService = clientService;
    }

    [HttpGet]
    public async Task<IEnumerable<Account>> Get()
    {
        return await accountService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetById(int id)
    {
        var account = await accountService.GetById(id);

        if(account is null)
            return AccountNotFound(id);

        return account;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AccountDTO account)
    {
        string validationResult = await ValidateAccount(account);
        if(!validationResult.Equals("Valid"))
            return BadRequest(new {message = validationResult});
        
        var newAccount = await accountService.Create(account);

        return CreatedAtAction(nameof(GetById), new {id = newAccount.Id}, newAccount);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Account account)
    {
        if (id != account.Id)
            return BadRequest(new {message = $"El ID({id}) de la URL no concide con el ID({account.Id}) del cuerpo de la solicitud."});
        
        var accountToUpdate = await accountService.GetById(id);

        if(accountToUpdate is not null)
        {
            string validationResult = await ValidateAccount(account);
            
            if(validationResult.Equals("Valid"))
                return BadRequest(new {message = validationResult});
            
            await accountService.Update(account);
            return NoContent();
        }
        else
        {
            return AccountNotFound(id);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var accountToDelete = await accountService.GetById(id);
        if(accountToDelete is not null)
        {
            await accountService.Delete(id);
            return Ok();
        }
        else
        {
            return AccountNotFound(id);
        }
    }

    public NotFoundObjectResult AccountNotFound(int id)
    {
        return NotFound(new { message = $"La cuenta con ID = {id} no existe."});
    }

    public async Task<string> ValidateAccount(AccountDTO account)
    {
        string result = "Valid";

        var accountType = await accountTypeService.GetById(account.AccountType);

        if(accountType is null)
            result = $"El tipo de cuenta {account.AccountType} no existe.";
        
        var clientID = account.ClientId.GetValueOrDefault();

        var client = await clientService.GetById(clientID);

        if(client is null)
            result = $"El cliente {clientID} no existe.";
        
        return result;
    }
}