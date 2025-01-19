namespace pfapp_cs_psql;

using System;
using System.Security.Cryptography;
using System.Text;
using Npgsql;

public interface IUserService {
    User RegisterUser(string username, string password);
    User? Login(string username, string password);
    void Logout();
    User? GetLoggedInUser();
}

public class PsqlUserService : IUserService
{
    private readonly NpgsqlConnection connection;
    private Guid? loggedInUser = null;
    
    public PsqlUserService(NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    private static string GetHexString(byte[] array) // Argument/Parameter inputSource (Row-nbr: 38, 45, 98, from this class)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in array)
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

    public User RegisterUser(string username, string password) // Argument/Parameter inputSource RegisterUserCommand.cs
    {
        // Generate salt
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        string saltString = GetHexString(salt);

        byte[] fullBytes = Encoding.UTF8.GetBytes(password + saltString);

        using (HashAlgorithm algorithm = SHA256.Create())
        {
            byte[] hash = algorithm.ComputeHash(fullBytes);
            password = GetHexString(hash);
        }

        password += ":" + saltString;

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Username = username,
            Password = password
        };

        try
        {
            var sql = @"INSERT INTO users (user_id, username, password) VALUES (
            @id,
            @username,
            @password
            )";
            using var cmd = new NpgsqlCommand(sql, this.connection);
            cmd.Parameters.AddWithValue("@id", user.UserId);
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@password", user.Password);

            cmd.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return user;
    }

    public User? Login(string username, string password) // Argument/Parameter inputSource LoginCommand.cs
    {
        // With username and password - try to find a matching user.
        var sql = @"SELECT * FROM users WHERE username = @username";
        // Use parameters instead of string concatenation to avoid SQL injections
        using var cmd = new NpgsqlCommand(sql, this.connection);
        cmd.Parameters.AddWithValue("@username", username);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var user = new User
            {
                UserId = reader.GetGuid(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2)
            };

            string[] passwordSplit = user.Password.Split(":");
            string passwordHash = passwordSplit[0];
            string salt = passwordSplit[1];

            byte[] fullBytes = Encoding.UTF8.GetBytes(password + salt);
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                byte[] hash = algorithm.ComputeHash(fullBytes);
                password = GetHexString(hash);
            }

            if (!passwordHash.Equals(password))
            {
                continue;
            }

            // Retrieve information from results and return it in the form of a User object
            loggedInUser = user.UserId;
            return user;
        }

        // If no user matched - return null
        return null;
    }

    public User? GetLoggedInUser()
    {
        // If no one is logged in, we can return null because there is nothing to retrieve from the database.
        if (loggedInUser == null)
        {
            return null;
        }

        // Try to find the user with matching id
        var sql = @"SELECT * FROM users WHERE user_id = @id";
        using var cmd = new NpgsqlCommand(sql, this.connection);
        // Use parameters instead of string concatenation to avoid SQL injections
        cmd.Parameters.AddWithValue("@id", loggedInUser);

        using var reader = cmd.ExecuteReader();
        // If no user matched - return null
        if (!reader.Read())
        {
            return null;
        }

        // (otherwise) read the information from the query results and return it as a User object

        var user = new User
        {
            UserId = reader.GetGuid(0),
            Username = reader.GetString(1),
            Password = reader.GetString(2)
        };

        return user;
    }

    public void Logout()
    {
        loggedInUser = null;
    }
}
