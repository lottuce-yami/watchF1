using System.Data;
using F1Project.Data.Database.Types;

namespace F1Project.Data.Database.Services;

internal class UserService : Database<User>
{
    private static User Deserialize(DataRow row)
    {
        return new User
        {
            Id = row["Id"].ToString() ?? "",
            Subscribed = int.Parse(row["Subscribed"].ToString() ?? "0") > 0,
            FirstName = row["FirstName"].ToString() ?? "",
            LastName = row["LastName"].ToString() ?? "",
            Username = row["Username"].ToString() ?? "",
            Photo = row["Photo"].ToString() ?? "",
            AuthDate = int.Parse(row["AuthDate"].ToString() ?? "0"),
            Hash = row["Hash"].ToString() ?? ""
        };
    }

    public static int AddUser(User user)
    {
        const string query =
            "INSERT INTO Users VALUES(@id, @subscribed, @firstName, @lastName, @username, @photo, @authDate, @hash)";
        var args = new Dictionary<string, object>
        {
            {"@id", user.Id},
            {"@subscribed", user.Subscribed? 1 : 0},
            {"@firstName", user.FirstName},
            {"@lastName", user.LastName ?? ""},
            {"@username", user.Username ?? ""},
            {"@photo", user.Photo ?? ""},
            {"@authDate", user.AuthDate ?? 0},
            {"@hash", user.Hash ?? ""}
        };

        return Data.Database.Write(query, args);
    }

    public static int EditUser(User user)
    {
        const string query =
            "UPDATE Users SET FirstName = @firstName, LastName = @lastName, Username = @username, Photo = @photo, AuthDate = @authDate, Hash = @hash WHERE Id = @id";
        var args = new Dictionary<string, object>
        {
            {"@id", user.Id},
            {"@firstName", user.FirstName},
            {"@lastName", user.LastName ?? ""},
            {"@username", user.Username ?? ""},
            {"@photo", user.Photo ?? ""},
            {"@authDate", user.AuthDate ?? 0},
            {"@hash", user.Hash ?? ""}
        };

        return Data.Database.Write(query, args);
    }

    public static int DeleteUser(string userId)
    {
        const string query =
            "DELETE FROM Users WHERE Id = @id";
        var args = new Dictionary<string, object>
        {
            {"@id", userId}
        };

        return Data.Database.Write(query, args);
    }

    public static User GetUser(string userId)
    {
        const string query =
            "SELECT * FROM Users WHERE Id = @id";
        var args = new Dictionary<string, object>
        {
            {"@id", userId}
        };
        var data = Data.Database.Read(query, args);

        return data.Rows.Count > 0 ? Deserialize(data.Rows[0]) : new User();
    }

    public static void Authorize(User user)
    {
        if (string.IsNullOrWhiteSpace(GetUser(user.Id).Id))
        {
            AddUser(user);
        }
        else
        {
            EditUser(user);
        }
    }

    public static bool ValidateAuthKey(string[] key)
    {
        var userId = key[0];
        var hash = key[1];

        return GetUser(userId).Hash == hash;
    }
}