using Microsoft.AspNetCore.Mvc;

namespace AtmMachine;

//this class represents an Account object in the database
public class Account
{
    public Account(string accountNumber, decimal balance)
    {
        //check that accountNUmber is both numerical and positive
        if(!int.TryParse(accountNumber, out int intAccountNum))
            throw new Exception($"account must be a number!");
        if(intAccountNum < 0)
            throw new Exception($"accountNumber must be positive!");

        AccountNumber = accountNumber;
        Balance = balance;
    }

    public string? AccountNumber { get; set; } 
    public decimal Balance { get; set; } //using decimal for better percision
}

