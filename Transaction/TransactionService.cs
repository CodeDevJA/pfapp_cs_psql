namespace pfapp_cs_psql;

using System;
using ConsoleColorUtilities;
using Npgsql;

public interface ITransactionService
{
    void GetAllTransactions();
    void GetBalance();
    void ViewTransactionsByYear();
    void ViewTransactionsByMonth();
    void ViewTransactionsByWeek();
    void ViewTransactionsByDay();
    void ViewTransactionsByRangeOfDates();
    void AddTransaction();
    void RemoveTransaction();
}

public class PsqlTransactionService : ITransactionService
{
    private readonly NpgsqlConnection connection;
    private readonly IUserService userService;
    
    public PsqlTransactionService(NpgsqlConnection connection, IUserService userService)
    {
        this.connection = connection;
        this.userService = userService;
    }

    public void GetAllTransactions()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            var sql = @"
                SELECT transaction_id, type, title, amount, date 
                FROM transactions 
                INNER JOIN users
                ON users.user_id = transactions.loggedinuser_id
                WHERE loggedinuser_id = @loggedinuser_id
                ORDER BY date DESC";

            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);

            using var reader = cmd.ExecuteReader();
            Console.WriteLine(" ");
            Console.WriteLine("Your transactions:");
            while (reader.Read())
            {
                Console.WriteLine(" ");
                Console.WriteLine($"ID: {TextColor.Cyan}{reader["transaction_id"]}{TextColor.Reset}");
                Console.WriteLine($"Type: {reader["type"]}");
                Console.WriteLine($"Title: {reader["title"]}");
                Console.WriteLine($"Amount: {TextColor.Yellow}{reader["amount"]}{TextColor.Reset}");
                Console.WriteLine($"Date: {reader["date"]}");
                Console.WriteLine(" ");
                Console.WriteLine("--------------------------");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message} ");
        }
    }
    
    public void GetBalance()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            var sql = @"
                SELECT 
                    SUM(CASE WHEN type = 'INCOME' THEN amount ELSE 0 END) - 
                    SUM(CASE WHEN type = 'EXPENSE' THEN amount ELSE 0 END) AS balance
                FROM transactions 
                INNER JOIN users
                ON users.user_id = transactions.loggedinuser_id
                WHERE loggedinuser_id = @loggedinuser_id";
            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);

            var balance = cmd.ExecuteScalar();
            Console.WriteLine($"Your {TextColor.Yellow}total balance{TextColor.Reset} is: {TextColor.Yellow}{balance}{TextColor.Reset}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message} ");
        }
    }
    
    public void ViewTransactionsByYear()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            Console.WriteLine($"Enter a year ({TextColor.Yellow}yyyy{TextColor.Reset}): ");
            int year = int.Parse(Console.ReadLine()!);

            var sql = @"
                SELECT transaction_id, type, title, amount, date 
                FROM transactions 
                INNER JOIN users
                ON users.user_id = transactions.loggedinuser_id
                WHERE loggedinuser_id = @loggedinuser_id
                AND EXTRACT(YEAR FROM date) = @year
                ORDER BY date DESC";
            
            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);
            cmd.Parameters.AddWithValue("@year", year);

            using var reader = cmd.ExecuteReader();
            Console.WriteLine(" ");
            Console.WriteLine($"Your transactions (filtered by {TransactionPeriod.YEAR}):");
            while (reader.Read())
            {
                Console.WriteLine(" ");
                Console.WriteLine($"ID: {TextColor.Cyan}{reader["transaction_id"]}{TextColor.Reset}");
                Console.WriteLine($"Type: {reader["type"]}");
                Console.WriteLine($"Title: {reader["title"]}");
                Console.WriteLine($"Amount: {TextColor.Yellow}{reader["amount"]}{TextColor.Reset}");
                Console.WriteLine($"Date: {reader["date"]}");
                Console.WriteLine(" ");
                Console.WriteLine("--------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    public void ViewTransactionsByMonth()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            Console.WriteLine($"Enter a year ({TextColor.Yellow}yyyy{TextColor.Reset}): ");
            int year = int.Parse(Console.ReadLine()!);

            Console.WriteLine($"Enter a month ({TextColor.Yellow}1-12{TextColor.Reset}): ");
            int month = int.Parse(Console.ReadLine()!);

            if (month < 1 || month > 12)
            {
                throw new ArgumentException("Month must be between 1 and 12.");
            }

            var sql = @"
                SELECT transaction_id, type, title, amount, date 
                FROM transactions 
                INNER JOIN users
                ON users.user_id = transactions.loggedinuser_id
                WHERE loggedinuser_id = @loggedinuser_id
                AND EXTRACT(YEAR FROM date) = @year
                AND EXTRACT(MONTH FROM date) = @month
                ORDER BY date DESC";

            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);

            using var reader = cmd.ExecuteReader();
            Console.WriteLine(" ");
            Console.WriteLine($"Your transactions (filtered by {TransactionPeriod.MONTH}):");
            while (reader.Read())
            {
                Console.WriteLine(" ");
                Console.WriteLine($"ID: {TextColor.Cyan}{reader["transaction_id"]}{TextColor.Reset}");
                Console.WriteLine($"Type: {reader["type"]}");
                Console.WriteLine($"Title: {reader["title"]}");
                Console.WriteLine($"Amount: {TextColor.Yellow}{reader["amount"]}{TextColor.Reset}");
                Console.WriteLine($"Date: {reader["date"]}");
                Console.WriteLine(" ");
                Console.WriteLine("--------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void ViewTransactionsByWeek()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            Console.WriteLine($"Enter a year ({TextColor.Yellow}yyyy{TextColor.Reset}): ");
            int year = int.Parse(Console.ReadLine()!);

            Console.WriteLine($"Enter a week number ({TextColor.Yellow}1-53{TextColor.Reset}): ");
            int week = int.Parse(Console.ReadLine()!);

            if (week < 1 || week > 53)
            {
                throw new ArgumentException("Week number must be between 1 and 53.");
            }

            var sql = @"
                SELECT transaction_id, type, title, amount, date 
                FROM transactions 
                INNER JOIN users
                ON users.user_id = transactions.loggedinuser_id
                WHERE loggedinuser_id = @loggedinuser_id
                AND EXTRACT(YEAR FROM date) = @year
                AND EXTRACT(WEEK FROM date) = @week
                ORDER BY date DESC";

            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@week", week);

            using var reader = cmd.ExecuteReader();
            Console.WriteLine(" ");
            Console.WriteLine($"Your transactions (filtered by {TransactionPeriod.WEEK}):");
            while (reader.Read())
            {
                Console.WriteLine(" ");
                Console.WriteLine($"ID: {TextColor.Cyan}{reader["transaction_id"]}{TextColor.Reset}");
                Console.WriteLine($"Type: {reader["type"]}");
                Console.WriteLine($"Title: {reader["title"]}");
                Console.WriteLine($"Amount: {TextColor.Yellow}{reader["amount"]}{TextColor.Reset}");
                Console.WriteLine($"Date: {reader["date"]}");
                Console.WriteLine(" ");
                Console.WriteLine("--------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void ViewTransactionsByDay()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            Console.WriteLine($"Enter a date ({TextColor.Yellow}yyyy-MM-dd{TextColor.Reset}): ");
            string inputDate = Console.ReadLine()!;
            DateTime date = DateTime.ParseExact(inputDate, "yyyy-MM-dd", null);

            var sql = @"
                SELECT transaction_id, type, title, amount, date 
                FROM transactions 
                INNER JOIN users
                ON users.user_id = transactions.loggedinuser_id
                WHERE loggedinuser_id = @loggedinuser_id
                AND date::date = @date
                ORDER BY date DESC";

            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);
            cmd.Parameters.AddWithValue("@date", date);

            using var reader = cmd.ExecuteReader();
            Console.WriteLine(" ");
            Console.WriteLine($"Your transactions (filtered by {TransactionPeriod.DAY}):");
            while (reader.Read())
            {
                Console.WriteLine(" ");
                Console.WriteLine($"ID: {TextColor.Cyan}{reader["transaction_id"]}{TextColor.Reset}");
                Console.WriteLine($"Type: {reader["type"]}");
                Console.WriteLine($"Title: {reader["title"]}");
                Console.WriteLine($"Amount: {TextColor.Yellow}{reader["amount"]}{TextColor.Reset}");
                Console.WriteLine($"Date: {reader["date"]}");
                Console.WriteLine(" ");
                Console.WriteLine("--------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void ViewTransactionsByRangeOfDates()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            Console.WriteLine($"Enter the start date ({TextColor.Yellow}yyyy-MM-dd{TextColor.Reset}): ");
            string inputStartDate = Console.ReadLine()!;
            DateTime startDate = DateTime.ParseExact(inputStartDate, "yyyy-MM-dd", null);

            Console.WriteLine($"Enter the end date ({TextColor.Yellow}yyyy-MM-dd{TextColor.Reset}): ");
            string inputEndDate = Console.ReadLine()!;
            DateTime endDate = DateTime.ParseExact(inputEndDate, "yyyy-MM-dd", null);

            if (startDate > endDate)
            {
                throw new ArgumentException("Start date must be earlier than or equal to end date.");
            }

            var sql = @"
                SELECT transaction_id, type, title, amount, date 
                FROM transactions 
                INNER JOIN users
                ON users.user_id = transactions.loggedinuser_id
                WHERE loggedinuser_id = @loggedinuser_id
                AND date >= @startDate
                AND date <= @endDate
                ORDER BY date DESC";

            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);

            using var reader = cmd.ExecuteReader();
            Console.WriteLine(" ");
            Console.WriteLine($"Your transactions (filtered by {TransactionPeriod.RANGE_START_DATE} {startDate:yyyy-MM-dd} to {TransactionPeriod.RANGE_END_DATE} {endDate:yyyy-MM-dd}):");
            while (reader.Read())
            {
                Console.WriteLine(" ");
                Console.WriteLine($"ID: {TextColor.Cyan}{reader["transaction_id"]}{TextColor.Reset}");
                Console.WriteLine($"Type: {reader["type"]}");
                Console.WriteLine($"Title: {reader["title"]}");
                Console.WriteLine($"Amount: {TextColor.Yellow}{reader["amount"]}{TextColor.Reset}");
                Console.WriteLine($"Date: {reader["date"]}");
                Console.WriteLine(" ");
                Console.WriteLine("--------------------------");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    public void AddTransaction()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            Console.WriteLine("Enter a transaction type, by typing a number: ");
            Console.WriteLine($"1. {TextColor.Green}{TransactionType.INCOME}{TextColor.Reset} ");
            Console.WriteLine($"2. {TextColor.Red}{TransactionType.EXPENSE}{TextColor.Reset} ");
            string inputType = Console.ReadLine()!;
            string transactionType = inputType.ToLower() == "1" ? TransactionType.INCOME.ToString() : TransactionType.EXPENSE.ToString();

            Console.WriteLine("Enter a transaction title: ");
            string title = Console.ReadLine()!;
            
            Console.WriteLine("Enter a transaction amount: ");
            decimal amount = Decimal.Parse(Console.ReadLine()!);
                    
            Console.WriteLine($"Enter a transaction date <{TextColor.Yellow}yyyy-MM-dd{TextColor.Reset}>: ");
            DateTime date = DateTime.Parse(Console.ReadLine()!);

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Type = transactionType,
                Title = title,
                Amount = amount,
                Date = date, // Alternetive: DateTime.UtcNow, for an automated-value
                User = user, // Stores the whole user-object and its nested values
            };

            var sql = @"
                INSERT INTO transactions (transaction_id, type, title, amount, date, loggedinuser_id) VALUES (
                @transaction_id, 
                @type, 
                @title, 
                @amount, 
                @date, 
                @loggedinuser_id
            )";
            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@transaction_id", transaction.TransactionId);
            cmd.Parameters.AddWithValue("@type", transaction.Type);
            cmd.Parameters.AddWithValue("@title", transaction.Title);
            cmd.Parameters.AddWithValue("@amount", transaction.Amount);
            cmd.Parameters.AddWithValue("@date", transaction.Date);
            cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);

            cmd.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message} ");
        }
    }

    public void RemoveTransaction()
    {
        try
        {
            User? user = userService.GetLoggedInUser();
            if (user == null)
            {
                throw new ArgumentException("You are not logged in.");
            }

            GetAllTransactions();
            Console.WriteLine($"Search for the transaction {TextColor.Cyan}ID{TextColor.Reset} and reuse its value when selecting witch transaction you want to {TextColor.Red}remove{TextColor.Reset}: ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.WriteLine($"Enter the {TextColor.Cyan}ID{TextColor.Reset} of the transaction you want to {TextColor.Red}remove{TextColor.Reset}:");
            string transactionIdInput = Console.ReadLine()!;
            Guid transactionId = Guid.Parse(transactionIdInput);

            var sql = @"
                DELETE FROM transactions 
                WHERE transaction_id = @transaction_id AND loggedinuser_id = @loggedinuser_id";
            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@transaction_id", transactionId);
            cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine($"Transaction with ID {transactionId} was successfully removed.");
                GetAllTransactions();
            }
            else
            {
                Console.WriteLine("No transaction found with the given ID.");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message} ");
        }
    }
}
