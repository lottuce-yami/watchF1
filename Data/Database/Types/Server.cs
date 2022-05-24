namespace F1Project.Data.Database.Types;

public class Server : DatabaseType
{
    public override string Id { get; init; } = null!;
    public string Url { get; set; } = null!;
}