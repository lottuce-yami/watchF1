namespace F1Project.Data.Database.Types;

public class User : DatabaseType
{
    public override string Id { get; init; } = null!;
    public int Subscribed { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Photo { get; set; }
    public int? AuthDate { get; set; }
    public string Hash { get; set; } = null!;
}