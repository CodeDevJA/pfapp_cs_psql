namespace pfapp_cs_psql;

using System;

public abstract class Menu
{

    private List<Command> commands = new List<Command>();

    // Add command to list.
    // Call this Method(), to "register" or "connect", a specific command (cmdOption) to the currentMenu.
    public void AddCommand(Command command)
    {
        try
        {
            this.commands.Add(command);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Run a command registered with "AddCommand", if one exists (cmdOption in currentMenu).
    public void ExecuteCommand(string inputCommand)
    {
        try
        {
            // Split command string (e.g login username password) into words: ["login", "username", "password"]
            // Each word becomes an element in the result array (commandParts).
            string[] commandParts = inputCommand.Split(" ");

            foreach (Command command in commands)
            {
                if (command.Name.Equals(commandParts[0]))
                {
                    command.Execute(commandParts);
                    return;
                }
            }            
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public abstract void Display();
}
