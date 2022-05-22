using System.Data;

namespace F1Project.Data;

public class VideoService
{
    public readonly string DefaultPath = SettingsService.GetSetting("defaultVideosPath");

    private static Video Deserialize(DataRow row)
    {
        return new Video
        {
            Id = row["Id"].ToString() ?? "",
            File = row["File"].ToString() ?? "",
            Title = row["Title"].ToString() ?? "",
            Preview = row["Preview"].ToString() ?? "",
            Championship = row["Championship"].ToString() ?? "",
            Season = row["Season"].ToString() ?? "",
            GrandPrix = row["GrandPrix"].ToString() ?? "",
            Type = row["Type"].ToString() ?? ""
        };
    }

    public static int AddVideo(Video video)
    {
        const string query =
            "INSERT INTO Videos VALUES(@id, @file, @title, @preview, @championship, @season, @grandPrix, @type)";
        var args = new Dictionary<string, object>
        {
            {"@id", video.Id},
            {"@file", video.File},
            {"@title", video.Title},
            {"@preview", video.Preview},
            {"@championship", video.Championship},
            {"@season", video.Season},
            {"@grandPrix", video.GrandPrix},
            {"@type", video.Type}
        };

        return Database.Write(query, args);
    }

    public static int EditVideo(Video video)
    {
        const string query =
            "UPDATE Videos SET File = @file, Title = @title, Preview = @preview, Championship = @championship, Season = @season, GrandPrix = @grandPrix, Type = @type WHERE Id = @id";
        var args = new Dictionary<string, object>
        {
            {"@id", video.Id},
            {"@file", video.File},
            {"@title", video.Title},
            {"@preview", video.Preview},
            {"@championship", video.Championship},
            {"@season", video.Season},
            {"@grandPrix", video.GrandPrix},
            {"@type", video.Type}
        };

        return Database.Write(query, args);
    }

    public static int DeleteVideo(string videoId)
    {
        const string query =
            "DELETE FROM Videos WHERE Id = @id";
        var args = new Dictionary<string, object>
        {
            {"@id", videoId}
        };

        return Database.Write(query, args);
    }

    public static Video GetVideo(string videoId)
    {
        const string query =
            "SELECT * FROM Videos WHERE Id = @id";
        var args = new Dictionary<string, object>
        {
            {"@id", videoId}
        };
        var data = Database.Read(query, args);

        return data.Rows.Count > 0 ? Deserialize(data.Rows[0]) : new Video();
    }

    public enum SelectionOptions
    {
        Path,
        Championship,
        Season,
        GrandPrix,
        Type
    }

    public static HashSet<Video> GetVideosBy(SelectionOptions selectionOption, string value)
    {
        string selection;
        if (selectionOption == SelectionOptions.Path)
        {
            var categories = value.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (categories.Length == 0)
            {
                selection = "";
            }
            else
            {
                var definedCategories = Enum.GetNames(typeof(SelectionOptions)).Skip(1).ToArray();
                var categorySelections = categories.Select(
                    (v, i) => $"{definedCategories[i]} = '{v}'").ToList();
            
                selection = $"WHERE {string.Join(" AND ", categorySelections)}";
            }
        }
        else
        {
            selection = $"WHERE {selectionOption} = '{value}'";
        }
        var query =
            $"SELECT * FROM Videos {selection}";
        var args = new Dictionary<string, object>();
        var data = Database.Read(query, args);

        var videos = new HashSet<Video>();
        foreach (DataRow row in data.Rows)
        {
            videos.Add(Deserialize(row));
        }

        return videos;
    }
}