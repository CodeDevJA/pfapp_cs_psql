namespace pfapp_cs_psql;

using System;

public class ViewTransactionsByDayCommand : Command
{
    public ViewTransactionsByDayCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService)
        : base("transactions-day", "View all transactions filtered by 'DAY' in the bank-account. ", userService, transactionService, menuService) { }

    public override void Execute(string[] args)
    {
        try
        {
            // transactions-day
            transactionService.ViewTransactionsByDay();
            // Console.WriteLine("These are all the transactions filtered by 'DAY' in the bank-account.");

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
