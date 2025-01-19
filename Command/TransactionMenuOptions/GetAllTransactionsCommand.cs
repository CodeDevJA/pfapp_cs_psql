namespace pfapp_cs_psql;

using System;

public class GetAllTransactionsCommand : Command
{
    public GetAllTransactionsCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService)
        : base("all-transactions", "Gets all the transactions from the bank-account. ", userService, transactionService, menuService) { }

    public override void Execute(string[] args)
    {
        try
        {
            // get-balance
            transactionService.GetAllTransactions();
            // Console.WriteLine("This is all the transactions in the bank-account.");
            
            Console.WriteLine("Type any key to continue...");
            Console.ReadKey();

            menuService.SetMenu(new TransactionMenu(userService, transactionService, menuService));
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }                
    }
}
