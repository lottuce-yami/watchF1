using System.Data;

namespace F1Project.Data;

public class ServerService
{
    private static string Deserialize(DataRow row)
    {
        return row["Url"].ToString() ?? "";
    }
    
    public static List<string> GetServers()
    {
        const string query =
            "SELECT * FROM Servers";
        var args = new Dictionary<string, object>();
        var data = Database.Read(query, args);

        var servers = new List<string>();
        if (data.Rows.Count <= 0) return servers;
        servers.AddRange(from DataRow row in data.Rows select Deserialize(row));

        return servers;
    }
}