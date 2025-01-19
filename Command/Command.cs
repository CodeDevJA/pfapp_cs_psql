namespace pfapp_cs_psql;

public abstract class Command
{
    public string Name { get; init; }
    public string Description { get; init; }

    protected IUserService userService;
    protected ITransactionService transactionService;
    protected IMenuService menuService;

    public Command(string name, string description, IUserService userService, ITransactionService transactionService, IMenuService menuService)
    {
        this.Name = name;
        this.Description = description;
        this.userService = userService;
        this.transactionService = transactionService;
        this.menuService = menuService;
    }

    public abstract void Execute(string[] args);
}
