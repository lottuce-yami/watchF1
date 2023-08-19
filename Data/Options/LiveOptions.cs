namespace F1Project.Data.Options;

public class LiveOptions
{
    public const string Live = "Live";
    
    public bool IsLive { get; set; }
    public string Title { get; set; } = null!;
}