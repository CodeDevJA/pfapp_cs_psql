namespace pfapp_cs_psql;

using System;
using ConsoleColorUtilities;

public class TransactionMenu : Menu
{
    public TransactionMenu(IUserService userService, ITransactionService transactionService, IMenuService menuService) 
    {
        try
        {
            AddCommand(new GetAllTransactionsCommand(userService, transactionService, menuService));
            AddCommand(new GetBalanceCommand(userService, transactionService, menuService));
            AddCommand(new ViewTransactionsByYearCommand(userService, transactionService, menuService));
            AddCommand(new ViewTransactionsByMonthCommand(userService, transactionService, menuService));
            AddCommand(new ViewTransactionsByWeekCommand(userService, transactionService, menuService));
            AddCommand(new ViewTransactionsByDayCommand(userService, transactionService, menuService));
            AddCommand(new ViewTransactionsByRangeOfDatesCommand(userService, transactionService, menuService));
            AddCommand(new AddTransactionCommand(userService, transactionService, menuService));
            AddCommand(new RemoveTransactionCommand(userService, transactionService, menuService));
            AddCommand(new LogoutCommand(userService, transactionService, menuService));
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public override void Display()
    {
        try
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Pesonal Finance Application ");
            Console.WriteLine("Transaction Menu: ");
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine($"{TextColor.BrightBlack}To view all transactions, type:{TextColor.Reset} {TextColor.Yellow}all-transactions{TextColor.Reset} ");
            Console.WriteLine(" ");
            Console.WriteLine($"{TextColor.BrightBlack}To view the total balance, type:{TextColor.Reset} {TextColor.Yellow}get-balance{TextColor.Reset} ");
            Console.WriteLine(" ");
            Console.WriteLine($"{TextColor.BrightBlack}To view all transactions by year, type:{TextColor.Reset} {TextColor.White}transactions-year{TextColor.Reset} ");
            Console.WriteLine(
                $"{TextColor.BrightBlack}To view all transactions by year and month, type:{TextColor.Reset} {TextColor.White}transactions-month{TextColor.Reset} "
            );
            Console.WriteLine($"{TextColor.BrightBlack}To view all transactions by year and week, type:{TextColor.Reset} {TextColor.White}transactions-week{TextColor.Reset} ");
            Console.WriteLine(
                $"{TextColor.BrightBlack}To view all transactions by year, month and day, type:{TextColor.Reset} {TextColor.White}transactions-day{TextColor.Reset} "
            );
            Console.WriteLine(" ");
            Console.WriteLine(
                $"{TextColor.BrightBlack}To view all transactions by a date range, type:{TextColor.Reset} {TextColor.White}transactions-date-range{TextColor.Reset} "
            );
            Console.WriteLine(" ");
            Console.WriteLine($"{TextColor.BrightBlack}To add a transaction, type:{TextColor.Reset} {TextColor.Green}add-transaction{TextColor.Reset} ");
            Console.WriteLine(" ");
            Console.WriteLine($"{TextColor.BrightBlack}To remove a transaction, type:{TextColor.Reset} {TextColor.Red}remove-transaction{TextColor.Reset} ");
            Console.WriteLine(" ");
            Console.WriteLine($"{TextColor.BrightBlack}To logout user, type:{TextColor.Reset} {TextColor.Magenta}logout{TextColor.Reset} ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
        }
        catch(Exception)
        {
            throw new ArgumentException("No command-options found in the TransactionMenu.");
        }
    }
}
