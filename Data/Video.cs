namespace F1Project.Data;

public class Video
{
    public string Id { get; init; } = null!;
    public string File { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Preview { get; set; } = null!;
    public string Championship { get; set; } = null!;
    public string Season { get; set; } = null!;
    public string GrandPrix { get; set; } = null!;
    public string Type { get; set; } = null!;

    public Video(){}
}