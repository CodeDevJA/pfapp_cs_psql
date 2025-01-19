// public void GetBalance()
// {
//     try
//     {
//         User? user = userService.GetLoggedInUser();
//         if (user == null)
//         {
//             throw new ArgumentException("You are not logged in.");
//         }

//         var sql = @"
//             SELECT 
//                 SUM(CASE WHEN type = 'INCOME' THEN amount ELSE 0 END) - 
//                 SUM(CASE WHEN type = 'EXPENSE' THEN amount ELSE 0 END) AS balance
//             FROM transactions 
//             INNER JOIN users
//             ON users.user_id = transactions.loggedinuser_id
//             WHERE loggedinuser_id = @loggedinuser_id";
//         using var cmd = new NpgsqlCommand(sql, this.connection);
//         cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);

//         var balance = cmd.ExecuteScalar();
//         Console.WriteLine($"Your {TextColor.Yellow}total balance{TextColor.Reset} is: {TextColor.Yellow}{balance}{TextColor.Reset}");
//     }
//     catch(Exception ex)
//     {
//         Console.WriteLine($"Error: {ex.Message} ");
//     }
// }