﻿using System.Data;

namespace F1Project.Data;

public class SettingsService
{
    private static string Deserialize(DataRow row)
    {
        return row["Value"].ToString() ?? "";
    }
    
    public static string GetSetting(string id)
    {
        const string query =
            "SELECT * FROM Settings WHERE Id = @id";
        var args = new Dictionary<string, object>
        {
            {"@id", id}
        };
        var data = Database.Read(query, args);

        return data.Rows.Count > 0 ? Deserialize(data.Rows[0]) : "";
    }
}