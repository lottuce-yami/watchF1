using System.Data;
using System.Data.SQLite;
using System.Text.Json;

namespace F1Project.Data;

public class VideoService
{
    private readonly string _database = JsonDocument.Parse(
        File.OpenRead("wwwroot/data/settings.json")).RootElement.GetProperty("database").ToString();
    
    public readonly string DefaultPath = JsonDocument.Parse(
            File.OpenRead("wwwroot/data/settings.json")).RootElement.GetProperty("defaultVideosPath").ToString();
    
    private int Write(string query, Dictionary<string, object> args)
    {
        using var conn = new SQLiteConnection($"Data Source={_database}");
            conn.Open();

            using var cmd = new SQLiteCommand(query, conn);
            foreach (var (key, value) in args)
            {
                cmd.Parameters.AddWithValue(key, value);
            }
            
            return cmd.ExecuteNonQuery();
    }

    private DataTable Read(string query, Dictionary<string, object> args)
    {
        using var conn = new SQLiteConnection($"Data Source={_database}");
            conn.Open();

            using var cmd = new SQLiteCommand(query, conn);
            foreach (var (key, value) in args)
            {
                cmd.Parameters.AddWithValue(key, value);
            }
            
            var dataTable = new DataTable();
            var dataAdapter = new SQLiteDataAdapter(cmd);
            dataAdapter.Fill(dataTable);
            dataAdapter.Dispose();
            return dataTable;
    }
    
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

    public int AddVideo(Video video)
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

        return Write(query, args);
    }

    public int EditVideo(Video video)
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

        return Write(query, args);
    }

    public int DeleteVideo(string videoId)
    {
        const string query =
            "DELETE FROM Videos WHERE Id = @id";
        var args = new Dictionary<string, object>
        {
            {"@id", videoId}
        };

        return Write(query, args);
    }

    public Video GetVideo(string videoId)
    {
        const string query =
            "SELECT * FROM Videos WHERE Id = @id";
        var args = new Dictionary<string, object>
        {
            {"@id", videoId}
        };
        var data = Read(query, args);

        return Deserialize(data.Rows[0]);
    }

    public enum SelectionOptions
    {
        Path,
        Championship,
        Season,
        GrandPrix,
        Type
    }

    public HashSet<Video> GetVideosBy(SelectionOptions selectionOption, string value)
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
        var data = Read(query, args);

        var videos = new HashSet<Video>();
        foreach (DataRow row in data.Rows)
        {
            videos.Add(Deserialize(row));
        }

        return videos;
    }
}