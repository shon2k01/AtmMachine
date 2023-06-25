namespace AtmMachine;
public class TransactionService : ITransactionService
{
    //Db handler object
    private readonly AppDbContext _dbContext;

    //Accountservice handler object
    private readonly IAccountService _accountService;

    //constructor
    public TransactionService(IAccountService accountService, AppDbContext dbContext)
    {
        _dbContext= dbContext;
        _accountService = accountService;
    }

    //Deposit money to account and update bills table
    //returns the updated balance if the operation was succesful. throws error otherwise.
    public decimal Deposit(string accountNumber, decimal amount)
    {
        //deposit must be positive!
        if(amount <= 0)
            throw new Exception("Error: invalid amount! must be a positive number!");

        //get the current account balance and deposit/ withdraw the amount
        decimal newBalance = _accountService.GetAccountBalance(accountNumber) + amount;

        //depost bills
        UpdateBills((int)amount);    

        //update the account and return the new amount
        return _accountService.UpdateAccountBalance(accountNumber, newBalance);
    }

    //Withdraw money frp, account and update bills table
    //returns the updated balance if the operation was succesful. throws error otherwise.
    public decimal Withdraw(string accountNumber, decimal amount){
        //withdraw amount must be positive!
        if(amount <= 0)
            throw new Exception("Error: invalid amount! must be a positive number!");
        
        //change amount to negative to withdraw    
        amount = -amount;

        //get the current account balance and withdraw the amount
        decimal newBalance = _accountService.GetAccountBalance(accountNumber) + amount;

        //check if account is sufficient
        if(newBalance < 0)
            throw new Exception($"Error wihtdrawing money! not enough money in account {accountNumber}\n" );

        //depost bills
        UpdateBills((int)amount);    

        //update the account and return the new amount
        return _accountService.UpdateAccountBalance(accountNumber, newBalance);
        
    }


    //splits the amount into bills and deposits them/ withdraws them from the databse
    public void UpdateBills(int amount){
        //remaining amount to deposit/withdraw
        int remainingAmount = amount;

        // Retrieve the bills table from the database
        var bills = _dbContext.Bills.ToList();

        // Sort the bills in descending order by value
        bills.Sort((b1, b2) => b2.Value.CompareTo(b1.Value));

        // Iterate through the bills and update the amount based on the available bills
        //!!!!Bugg - when trying to withdraw numbers like "60" by taking 50 first instead of 3 "20" bills. this throws an error !!!!
        foreach (var bill in bills)
        {
            if (remainingAmount / bill.Value != 0)
            {
                int billCount = (int)(remainingAmount / (decimal)bill.Value);
                remainingAmount -= bill.Value * billCount;
                bill.Amount += billCount;
                if(bill.Amount < 0)
                    throw new Exception($"SORRY! Not enough bills in the atm to make this withdrawl..");
            }
        }
        //if we still have money left after removing all 20,50,100,200 bills, than the amount is insufficient - throws error
        if(remainingAmount != 0)
            throw new Exception($"Error: the atm only excepts bills of 20, 50, 100, and 200. {Math.Abs(remainingAmount)} is an invalid amount!!");

        // Save the changes back to the database
        _dbContext.SaveChanges();
    }

}
