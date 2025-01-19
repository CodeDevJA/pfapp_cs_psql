namespace pfapp_cs_psql;

using System;

public class ViewTransactionsByWeekCommand : Command
{
    public ViewTransactionsByWeekCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService)
        : base("transactions-week", "View all transactions filtered by 'WEEK' in the bank-account. ", userService, transactionService, menuService) { }

    public override void Execute(string[] args)
    {
        try
        {
            // transactions-week
            transactionService.ViewTransactionsByWeek();
            // Console.WriteLine("These are all the transactions filtered by 'WEEK' in the bank-account.");

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
