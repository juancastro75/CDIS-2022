using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Services;

public class AccountService
{
    private readonly BankContext _context;

    public AccountService(BankContext context)
    {
        _context = context;
    }

    public IEnumerable<Account> GetAll()
    {
        return _context.Accounts.ToList();
    }

    public Account? GetById(int id)
    {
        return _context.Accounts.Find(id);
    }

    public AccountsModel Create(AccountsModel newAccount)
    {
        var newAccountRegister = new Account();

        newAccountRegister.AccountType = newAccount.AccountType;
        newAccountRegister.ClientId = newAccount.ClientId;
        newAccountRegister.Balence = newAccount.Balence;
        _context.Accounts.Add(newAccountRegister);
        _context.SaveChanges();

        return newAccount;
    }

    public void Update(int id, AccountsModel account)
    {
        var currentAccout = GetById(id);

        currentAccout.AccountType = account.AccountType;
        currentAccout.ClientId = account.ClientId;
        currentAccout.Balence = account.Balence;
        
        _context.Accounts.Update(currentAccout);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var accountToDelete = GetById(id);

        if(accountToDelete is not null)
        {
            _context.Accounts.Remove(accountToDelete);
            _context.SaveChanges();
        }
    }

    public Client? GetClientId(int id)
    {
        return _context.Clients.Find(id);
    }
}