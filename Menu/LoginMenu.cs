namespace pfapp_cs_psql;

using System;
using ConsoleColorUtilities;

public class LoginMenu : Menu
{
    public LoginMenu(IUserService userService, ITransactionService transactionService, IMenuService menuService)
    {        
        try
        {
            AddCommand(new RegisterUserCommand(userService, transactionService, menuService));
            AddCommand(new LoginCommand(userService, transactionService, menuService));
            AddCommand(new ExitAppCommand(userService, transactionService, menuService));
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public override void Display()
    {
        try{
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Personal Finance Application ");
            Console.WriteLine("Login Menu: ");
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine($"{TextColor.BrightBlack}Register a user by typing:{TextColor.Reset} {TextColor.Yellow}register-user username password{TextColor.Reset} ");
            Console.WriteLine(" ");
            Console.WriteLine($"{TextColor.BrightBlack}Login by typing:{TextColor.Reset} {TextColor.Green}login username password{TextColor.Reset} ");
            Console.WriteLine(" ");
            Console.WriteLine($"{TextColor.BrightBlack}Exit App by typing:{TextColor.Reset} {TextColor.Magenta}exit-app{TextColor.Reset} ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
        }
        catch(Exception)
        {
            throw new ArgumentException("No command-options found in the LoginMenu.");
        }
    }
}
