namespace pfapp_cs_psql;

using System;

public class RemoveTransactionCommand : Command
{
    public RemoveTransactionCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService)
        : base("remove-transaction", "Removes a transaction from the bank-account. ", userService, transactionService, menuService) { }

    public override void Execute(string[] args)
    {
        try
        {
            // remove-transaction
            transactionService.RemoveTransaction();
            
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
