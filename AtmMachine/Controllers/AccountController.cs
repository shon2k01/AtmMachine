using Microsoft.AspNetCore.Mvc;
using AtmMachine;

namespace AtmMachine.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        //account handler
        private readonly IAccountService accountService; 

        //constructor   
        public AccountController(IAccountService accountService){
            this.accountService = accountService;
        }

        //Retrieve the account balance based on the account number on GET request 
        [HttpGet("{accountNumber}")]
        public ActionResult<decimal> GetAccountBalance(string accountNumber)
        {
            try{
                //return Ok(202) with account's balance
                return Ok($"Account {accountNumber}'s current balance: {accountService.GetAccountBalance(accountNumber)}\n");
            }
            catch (Exception ex)
            {
                // return BadRequest if there was a problem
                return BadRequest($"Error retrieving account balance: {ex.Message}\n");
            }
        }

        // Update the account balance based on the account number and the provided amount
        [HttpPost("update/{accountNumber}/{amount}")]
        public ActionResult<decimal> UpdateAccountBalance(string accountNumber, decimal amount)
        {
            
            try{
                //update account balance, than
                //return Ok(202) with account's new balance
                return Ok($"Account {accountNumber}'s current balance: {accountService.UpdateAccountBalance(accountNumber, amount)}\n");
            }
            catch (Exception ex)
            {
                // return BadRequest if there was a problem
                return BadRequest($"Error Updating account balance: {ex.Message}\n");
            }
        }

        //Add account to database
        [HttpPut("new/{accountNumber}/{balance}")]
        public ActionResult<decimal> AddAccount(string accountNumber, decimal balance){
            try{
                var account = new Account (accountNumber, balance);
                
                //add acount to database
                accountService.AddAccount(account);
                return Ok("Account added succesfully\n");

            }
            catch (Exception ex)
            {
                // return BadRequest if there was a problem
                return BadRequest($"Error creating account: {ex.Message}\n");
            }
            
        }

    }
}






