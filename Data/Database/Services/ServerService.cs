using System.Data;
using F1Project.Data.Database.Types;

namespace F1Project.Data.Database.Services;

internal class ServerService : Service<Server>
{
    protected override Server Deserialize(DataRow row)
    {
        return new Server
        {
            Id = row["Id"].ToString() ?? "",
            Url = row["Url"].ToString() ?? ""
        };
    }

    

    /*public static List<string> GetServers()
    {
        const string query =
            "SELECT * FROM Servers";
        var args = new Dictionary<string, object>();
        var data = Database.Read(query, args);

        var servers = new List<string>();
        if (data.Rows.Count <= 0) return servers;
        servers.AddRange(from DataRow row in data.Rows select Deserialize(row));

        return servers;
    }*/
}