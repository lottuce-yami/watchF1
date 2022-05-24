namespace F1Project.Data.Database.Types;

public class Video : DatabaseType
{
    public override string Id { get; init; } = null!;
    public string File { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Preview { get; set; } = null!;
    public string Championship { get; set; } = null!;
    public string Season { get; set; } = null!;
    public string GrandPrix { get; set; } = null!;
    public string Type { get; set; } = null!;

    /*public Video(){}

    public Video(string file, string title, string preview, string championship, string season, string grandPrix, string type)
    {
        Id = RandomId();
        File = file;
        Title = title;
        Preview = preview;
        Championship = championship;
        Season = season;
        GrandPrix = grandPrix;
        Type = type;
    }

    private static string RandomId()
    {
        return Convert.ToBase64String(
            Guid.NewGuid().ToByteArray()
        )
            .Replace("/", "_")
            .Replace("+", "-")
            .Replace("=", "");
    }*/
}