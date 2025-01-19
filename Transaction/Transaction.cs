namespace pfapp_cs_psql;

using System;

public class Transaction
{
    public required Guid TransactionId { get; init; } // Psql-datatype: Guid/UUID
    public required string Type { get; init; }
    public required string Title { get; init; }
    public required decimal Amount { get; init; }
    public required DateTime Date { get; init; }
    public required User? User { get; init; }
}

public enum TransactionType
{
    INCOME = 1,
    EXPENSE = 2,
}

public enum TransactionPeriod
{
    YEAR = 1,
    MONTH = 2,
    WEEK = 3,
    DAY = 4,
    RANGE_START_DATE = 5,
    RANGE_END_DATE = 6,
}
