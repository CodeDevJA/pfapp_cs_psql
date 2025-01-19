namespace pfapp_cs_psql;

using System;

public class ViewTransactionsByYearCommand : Command
{
    public ViewTransactionsByYearCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService)
        : base("transactions-year", "View all transactions filtered by 'YEAR' in the bank-account. ", userService, transactionService, menuService) { }

    public override void Execute(string[] args)
    {
        try
        {
            // transactions-year
            transactionService.ViewTransactionsByYear();
            // Console.WriteLine("These are all the transactions filtered by 'YEAR' in the bank-account.");

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
