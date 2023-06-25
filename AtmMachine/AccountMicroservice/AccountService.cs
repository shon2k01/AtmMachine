namespace AtmMachine;

public class AccountService : IAccountService
{
    //DbHandler object
    private readonly AppDbContext _dbContext;
    
    //constructor
    public AccountService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //return account balance if it exists
    //throw error otherwise
    public decimal GetAccountBalance(string accountNumber)
    {
        // Account does not exist
        if (!AccountExists(accountNumber))
            throw new Exception($"Account doesn't exist!");
 
        //Get account and return  balance
        Account account = _dbContext.Accounts.First(a => a.AccountNumber == accountNumber);
        return account.Balance;
    }

    //update account balance if it exists
    //throw error otherwise
    public decimal UpdateAccountBalance(string accountNumber, decimal amount)
    {
        // Account does not exist
        if (!AccountExists(accountNumber))
            throw new Exception($"Account doesn't exist!");
 
        //Get account and update balance
        Account account = _dbContext.Accounts.First(a => a.AccountNumber == accountNumber);
        account.Balance = amount;         

        
        // Save the changes back to the database
        _dbContext.SaveChanges();
        //return new balance
        return account.Balance;
    }

    //check if the account exists
    public bool AccountExists(string? accountNumber)
    {
        if (string.IsNullOrEmpty(accountNumber))
            return false;
        
        return _dbContext.Accounts.Any(a => a.AccountNumber == accountNumber);
    }

    //Check if accout exists and Add it to the DB if it doesn't.
    public void AddAccount(Account account)
    {
        //check if account exists
        if(AccountExists(account.AccountNumber)){
            //throw error if it does (this exists the function)
            throw new Exception($"Account already exists!");
        }

        //add account to db and save changes
        _dbContext.Accounts.Add(account);
        _dbContext.SaveChanges();
    }
}

