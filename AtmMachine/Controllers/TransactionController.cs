using Microsoft.AspNetCore.Mvc;
// using AtmMachine;

namespace AtmMachine.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        //transaction handler
        private readonly ITransactionService transactionService; 
        //constructor
        public TransactionController(ITransactionService transactionService){
            this.transactionService = transactionService;
        }

        //Deposit amount to account if it exists
        [HttpPost("deposit/{accountNumber}/{amount}")]
        public ActionResult<decimal> Deposit(string accountNumber, int amount)
        {
            // Update the account balance based on the accountNumber and the provided amount
            try{
                //return Ok with account's new balance
                return Ok($"Account {accountNumber}'s current balance: {transactionService.Deposit(accountNumber, amount)}\n");
            }
            catch (Exception ex)
            {
                // return BadRequest if there was a problem
                return BadRequest($"Error Updating account balance: {ex.Message}\n");
            }
        }

        //Withdraw amount from account if it exists and balance is sufficient
        [HttpPost("withdraw/{accountNumber}/{amount}")]
        public ActionResult<decimal> withdraw(string accountNumber, decimal amount)
        {
           // Update the account balance based on the account number and the provided amount
            try{
                return Ok($"Account {accountNumber}'s current balance: {transactionService.Withdraw(accountNumber, amount)}\n");
            }
            catch (Exception ex)
            {
                // return BadRequest if there was a problem
                return BadRequest($"Error Updating account balance: {ex.Message}\n");
            }
        }
    }
}


