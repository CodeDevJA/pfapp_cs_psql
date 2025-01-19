namespace pfapp_cs_psql;

using System;

public class GetBalanceCommand : Command
{
    public GetBalanceCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService)
        : base("get-balance", "Gets the total balance from the bank-account. ", userService, transactionService, menuService) { }

    public override void Execute(string[] args)
    {
        try
        {
            // get-balance
            transactionService.GetBalance();
            // Console.WriteLine("This is the total balance in the bank-account.");

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
