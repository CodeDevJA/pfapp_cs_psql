namespace pfapp_cs_psql;

using System;

public class ViewTransactionsByRangeOfDatesCommand : Command
{
    public ViewTransactionsByRangeOfDatesCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService)
        : base("transactions-date-range", "View all transactions filtered by start-date and end-date, in the bank-account. ", userService, transactionService, menuService) { }

    public override void Execute(string[] args)
    {
        try
        {
            // transactions-date-range
            transactionService.ViewTransactionsByRangeOfDates();
            // Console.WriteLine("These are all the transactions filtered by a 'startDate' <yyyy-MM-dd> and an 'endDate' <yyyy-MM-dd> in the bank-account.");

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
