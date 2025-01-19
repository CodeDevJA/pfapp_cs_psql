// public void RemoveTransaction()
// {
//     try
//     {
//         User? user = userService.GetLoggedInUser();
//         if (user == null)
//         {
//             throw new ArgumentException("You are not logged in.");
//         }

//         GetAllTransactions();
//         Console.WriteLine($"Search for the transaction {TextColor.Cyan}ID{TextColor.Reset} and reuse its value when selecting witch transaction you want to {TextColor.Red}remove{TextColor.Reset}: ");
//         Console.WriteLine(" ");
//         Console.WriteLine(" ");

//         Console.WriteLine($"Enter the {TextColor.Cyan}ID{TextColor.Reset} of the transaction you want to {TextColor.Red}remove{TextColor.Reset}:");
//         string transactionIdInput = Console.ReadLine()!;
//         Guid transactionId = Guid.Parse(transactionIdInput);

//         var sql = @"
//             DELETE FROM transactions 
//             WHERE transaction_id = @transaction_id AND loggedinuser_id = @loggedinuser_id";
//         using var cmd = new NpgsqlCommand(sql, this.connection);
//         cmd.Parameters.AddWithValue("@transaction_id", transactionId);
//         cmd.Parameters.AddWithValue("@loggedinuser_id", user.UserId);

//         int rowsAffected = cmd.ExecuteNonQuery();
//         if (rowsAffected > 0)
//         {
//             Console.WriteLine($"Transaction with ID {transactionId} was successfully removed.");
//             GetAllTransactions();
//         }
//         else
//         {
//             Console.WriteLine("No transaction found with the given ID.");
//         }
//     }
//     catch(Exception ex)
//     {
//         Console.WriteLine($"Error: {ex.Message} ");
//     }
// }