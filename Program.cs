namespace pfapp_cs_psql;

using System;
using System.IO;
using ConsoleColorUtilities;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        // Define the path to the secret file
        string secretFilePath = "connectionstring.secret";
        string connectionString;

        try
        {
            // Read the connection string from the secret file
            if (File.Exists(secretFilePath))
            {
                connectionString = File.ReadAllText(secretFilePath).Trim();
            }
            else
            {
                throw new FileNotFoundException($"The secret file '{secretFilePath}' was not found.");
            }

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            Console.WriteLine($"{TextColor.BrightBlack}Connection to the PostgreSQL database was successful!{TextColor.Reset}");

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Create tables if they don't exist
                    var createTablesSql = @"
                    CREATE TABLE IF NOT EXISTS users (
                        user_id UUID PRIMARY KEY NOT NULL,
                        username TEXT UNIQUE NOT NULL,
                        password TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS transactions (
                        transaction_id UUID PRIMARY KEY NOT NULL,
                        type TEXT NOT NULL,
                        title TEXT NOT NULL,
                        amount DECIMAL NOT NULL,
                        date TIMESTAMPTZ NOT NULL,
                        loggedinuser_id UUID NOT NULL REFERENCES users(user_id) ON DELETE CASCADE ON UPDATE CASCADE
                    );
                    ";
                    using (var createTableCmd = new NpgsqlCommand(createTablesSql, connection, transaction))
                    {
                        createTableCmd.ExecuteNonQuery();
                    }
                    Console.WriteLine($"{TextColor.BrightBlack}Creation of the users and transactions tables was successful or they already exist!{TextColor.Reset}");

                    // Commit the transaction
                    transaction.Commit();
                    Console.WriteLine($"{TextColor.BrightBlack}Database setup transaction committed successfully.{TextColor.Reset}");
                }
                catch (Exception ex)
                {
                    // Rollback the transaction on error
                    transaction.Rollback();
                    Console.WriteLine($"Error during database setup. Transaction rolled back. Details: {ex.Message}");
                }
            }
        
            // Service initialization
            IUserService userService = new PsqlUserService(connection);
            ITransactionService transactionService = new PsqlTransactionService(connection, userService);
            IMenuService menuService = new SimpleMenuService();
            
            Menu initialMenu = new LoginMenu(userService, transactionService, menuService);
            menuService.SetMenu(initialMenu);

            // Main application loop
            while (true)
            {
                // Prompt the user for input
                string? inputCommand = Console.ReadLine();
                if (inputCommand != null)
                {
                    menuService.GetMenu().ExecuteCommand(inputCommand);
                }
                else
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
