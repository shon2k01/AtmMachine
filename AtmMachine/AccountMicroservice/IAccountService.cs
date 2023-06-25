namespace AtmMachine;

//Interface for AccountService
public interface IAccountService
{
    decimal GetAccountBalance(string accountNumber);
    //get account balance

    decimal UpdateAccountBalance(string accountNumber, decimal amount); 
    //update account balance

    bool AccountExists(string? accountNumber); 
    //check if account exists
    
    void AddAccount(Account account);     
    //add acount to db


}

