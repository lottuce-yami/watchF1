using F1Project.Data.Database.Types;

namespace F1Project.Data.Database.Services;

internal class UserService : Service<User>
{
    public static int Subscribe(string userId, int quantity)
    {
        var query = $"UPDATE {Table} SET Subscribed = Subscribed + @sub WHERE Id = @id";
        var args = new Dictionary<string, object?>
        {
            {"@id", userId},
            {"@sub", quantity}
        };
        
        return Database.Write(query, args);
    }
    
    /*public static int Unsubscribe(string userId, int quantity)
    {
        var query = $"UPDATE {Table} SET Subscribed = Subscribed - @sub WHERE Id = @id";
        var args = new Dictionary<string, object?>
        {
            {"@id", userId},
            {"@sub", quantity}
        };
        
        return Database.Write(query, args);
    }*/

    public static int ExpireAllSubs()
    {
        var query = $"UPDATE {Table} SET Subscribed = Subscribed - 1 WHERE Subscribed > 0";
        var args = new Dictionary<string, object?>();
        
        return Database.Write(query, args);
    }
    
    public void Authorize(User user)
    {
        try
        {
            user.Subscribed = Get(user.Id).Subscribed;
            Edit(user);
        }
        catch
        {
            Add(user);
        }
    }

    public bool ValidateAuthKey(string[] key)
    {
        var userId = key[0];
        var hash = key[1];

        return Get(userId).Hash == hash;
    }
}