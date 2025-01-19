namespace pfapp_cs_psql;

using System;

public class User
{
    public required Guid UserId { get; init; } // Psql-datatype: Guid/UUID
    public required string Username { get; set; }
    public required string Password { get; set; }
}
