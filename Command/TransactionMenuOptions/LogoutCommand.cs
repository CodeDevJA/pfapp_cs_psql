namespace pfapp_cs_psql;

using System;
using ConsoleColorUtilities;

public class LogoutCommand : Command
{
    public LogoutCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("logout", "Loggs out the user account.", userService, transactionService, menuService)
    {
    }

    public override void Execute(string[] args)
    {
        try
        {
            // logout
            userService.Logout();
            Console.WriteLine($"{TextColor.Magenta}User are now logged out.{TextColor.Reset}");

            Console.WriteLine("Type any key to continue...");
            Console.ReadKey();

            menuService.SetMenu(new LoginMenu(userService, transactionService, menuService));
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }        
    }
}
