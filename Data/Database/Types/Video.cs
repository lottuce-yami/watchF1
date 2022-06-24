namespace F1Project.Data.Database.Types;

public class Video : DatabaseType
{
    public override string Id { get; init; } = null!;
    public string Title { get; set; } = null!;
    public string Championship { get; set; } = null!;
    public string Season { get; set; } = null!;
    public string GrandPrix { get; set; } = null!;
    public string Type { get; set; } = null!;
    
    public SourceModel Source(string storageDirectory)
    {
        var source = new SourceModel();

        if (File.Exists(Path.GetFullPath($"{storageDirectory}videos/{Id}/{Id}.webp")))
            source.Preview = $"{BaseFile}.webp";
        if (File.Exists(Path.GetFullPath($"{storageDirectory}videos/{Id}/{Id}.jpg")))
            source.Preview = $"{BaseFile}.jpg";
        
        foreach (var (q, qualityName) in QualityTemplates)
        {
            var quality = new SourceModel.Quality
            {
                QualityName = qualityName
            };

            foreach (var (a, audioName) in AudioTemplates)
            {
                if (!File.Exists(Path.GetFullPath($"{storageDirectory}videos/{Id}/{Id}-{a}_{q}.mp4"))) continue;
                quality.Audios.Add(audioName, $"{BaseFile}-{a}_{q}.mp4");
            }
            
            if (quality.Audios.Any()) 
                source.Qualities.Add(quality);
        }

        if (!source.Qualities.Any()) 
            source.PlayerSource = $"{BaseFile}.mp4";
        else
        {
            source.PlayerSource =
                string.Join(
                    ',',
                    source.Qualities.Select(
                        q => 
                            $"[{q.QualityName}]" +
                            $"{string.Join(';', q.Audios.Select(a => $"{{{a.Key}}}{a.Value}"))}"
                    )
                );
        }
        
        return source;
    }

    private string BaseFile => $"storage/videos/{Id}/{Id}";
    
    private static Dictionary<string, string> QualityTemplates => new()
    {
        {"1080", "Высокое - 1080p"},
        {"720", "Среднее - 720p"}
    };

    private static Dictionary<string, string> AudioTemplates => new()
    {
        {"fx", "Интершум"},
        {"en", "Англ. комментарии"},
        {"ru", "Русс. комментарии"}
    };

    public class SourceModel
    {
        public string Preview { get; set; } = "storage/images/skeleton/preview.jpg";
        public string PlayerSource { get; set; } = "";
        public List<Quality> Qualities { get; set; } = new();

        public class Quality
        {
            public string QualityName { get; set; } = null!;
            public Dictionary<string, string> Audios { get; set; } = new();
        }
    }
}