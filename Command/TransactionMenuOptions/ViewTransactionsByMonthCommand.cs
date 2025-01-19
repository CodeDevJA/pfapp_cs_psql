namespace pfapp_cs_psql;

using System;

public class ViewTransactionsByMonthCommand : Command
{
    public ViewTransactionsByMonthCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService)
        : base("transactions-month", "View all transactions filtered by 'MONTH' in the bank-account. ", userService, transactionService, menuService) { }

    public override void Execute(string[] args)
    {
        try
        {
            // transactions-month
            transactionService.ViewTransactionsByMonth();
            // Console.WriteLine("These are all the transactions filtered by 'MONTH' in the bank-account.");

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
