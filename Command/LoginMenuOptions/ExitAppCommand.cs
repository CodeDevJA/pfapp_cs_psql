namespace pfapp_cs_psql;

using System;
using ConsoleColorUtilities;

public class ExitAppCommand : Command
{
    public ExitAppCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("exit-app", "Exits the application manually.", userService, transactionService, menuService)
    {
    }

    public override void Execute(string[] args)
    {
        try
        {
            // exit-app
            Console.WriteLine($"You have {TextColor.Magenta}exited{TextColor.Reset} the application manually.");
            Environment.Exit(0);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
