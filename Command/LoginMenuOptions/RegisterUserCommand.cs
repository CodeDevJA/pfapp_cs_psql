namespace pfapp_cs_psql;

using System;

public class RegisterUserCommand : Command
{
    public RegisterUserCommand(IUserService userService, ITransactionService transactionService, IMenuService menuService) : base("register-user", "Create a new user account.", userService, transactionService, menuService)
    {
    }

    public override void Execute(string[] args)
    {
        try
        {
            // register-user [username password]
            string username = args[1];
            string password = args[2];

            User user = userService.RegisterUser(username, password);
            
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
