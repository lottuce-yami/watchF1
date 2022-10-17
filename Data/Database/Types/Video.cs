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
        {
            source.PlayerSource = 
                File.Exists(Path.GetFullPath($"{storageDirectory}videos/{Id}/{Id}1080.mp4")) ? 
                    $"{BaseFile}1080.mp4" : $"{BaseFile}.mp4";
            return source;
        }

        if (source.Qualities.Count == 1)
        {
            var quality = source.Qualities.Single();
            source.PlayerSource = quality.Audios.Count == 1 ? 
                quality.Audios.Single().Value 
                : 
                string.Join(';', quality.Audios.Select(a => $"{{{a.Key}}}{a.Value}"));
            return source;
        }

        source.PlayerSource =
            string.Join(
                ',',
                source.Qualities.Select(
                    q => q.Audios.Count == 1 ?
                        $"[{q.QualityName}]" + 
                        q.Audios.Single().Value
                        :
                        $"[{q.QualityName}]" + 
                        string.Join(';', q.Audios.Select(a => $"{{{a.Key}}}{a.Value}"))
                )
            );
        return source;
    }

    public string Preview(string storageDirectory)
    {
        if (File.Exists(Path.GetFullPath($"{storageDirectory}videos/{Id}/{Id}.webp")))
            return $"{BaseFile}.webp";
        if (File.Exists(Path.GetFullPath($"{storageDirectory}videos/{Id}/{Id}.jpg")))
            return $"{BaseFile}.jpg";
        
        return "storage/images/skeleton/preview.jpg";
    }

    private string BaseFile => $"storage/videos/{Id}/{Id}";
    
    private static Dictionary<string, string> QualityTemplates => new()
    {
        {"1080", "Высокое - 1080p"},
        {"720", "Среднее - 720p"},
        {"480", "Низкое - 480p"}
    };

    private static Dictionary<string, string> AudioTemplates => new()
    {
        {"fx", "Интершум"},
        {"en", "Англ. комментарии"},
        {"ru", "Русс. комментарии"}
    };

    public class SourceModel
    {
        public string PlayerSource { get; set; } = "";
        public List<Quality> Qualities { get; set; } = new();

        public class Quality
        {
            public string QualityName { get; set; } = null!;
            public Dictionary<string, string> Audios { get; set; } = new();
        }
    }
}