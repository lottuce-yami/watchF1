using System.Data;
using System.Data.SQLite;

namespace F1Project.Data.Database;

internal static class Database
{
    private const string DatabaseFile = "wwwroot/data/website.db";

    /*private static SQLiteCommand GetCommand(string query, Dictionary<string, object?> args)
    {
        using var conn = new SQLiteConnection($"Data Source={DatabaseFile}");
        conn.Open();

        using var cmd = new SQLiteCommand(query, conn);
        foreach (var (key, value) in args)
        {
            cmd.Parameters.AddWithValue(key, value);
        }

        return cmd;
    }*/
    
    internal static int Write(string query, Dictionary<string, object?> args)
    {
        using var conn = new SQLiteConnection($"Data Source={DatabaseFile}");
        conn.Open();

        using var cmd = new SQLiteCommand(query, conn);
        foreach (var (key, value) in args)
        {
            cmd.Parameters.AddWithValue(key, value);
        }
        
        return cmd.ExecuteNonQuery();
    }

    internal static DataTable Read(string query, Dictionary<string, object?> args)
    {
        using var conn = new SQLiteConnection($"Data Source={DatabaseFile}");
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
}