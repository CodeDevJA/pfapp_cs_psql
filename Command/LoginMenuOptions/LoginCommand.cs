namespace pfapp_cs_psql;

using System;

public class LoginCommand : Command
{
    public LoginCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("login", "Login with username and password.", userService, transactionService, menuService)
    {
    }

    public override void Execute(string[] args)
    {
        try
        {
            // login [username password]
            string username = args[1];
            string password = args[2];

            User? user = userService.Login(username, password);
            if (user == null)
            {
                Console.WriteLine("Wrong username or password.");
                return;
            }

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
