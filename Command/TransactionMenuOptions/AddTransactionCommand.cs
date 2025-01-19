namespace pfapp_cs_psql;

using System;

public class AddTransactionCommand : Command
{
    public AddTransactionCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService)
        : base("add-transaction", "Adds a transaction to the bank-account. ", userService, transactionService, menuService) { }

    public override void Execute(string[] args)
    {
        try
        {
            // add-transaction
            transactionService.AddTransaction();
            
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
