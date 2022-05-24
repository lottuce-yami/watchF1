namespace F1Project.Data.Database.Types;

public class Setting : DatabaseType
{
    public override string Id { get; init; } = null!;
    public string? Value { get; set; }
}