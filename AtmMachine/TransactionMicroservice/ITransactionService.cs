namespace AtmMachine;

//Interface for TransactionService
public interface ITransactionService
{
    //Deposit money from the account and update the bills in the atm.
    decimal Deposit(string accountNumber, decimal amount);

    //Withdraw money from the account and update the bills in the atm.
    decimal Withdraw(string accountNumber, decimal amount);

    //update the bills database after deposit/withdraw
    void UpdateBills(int amount);
}