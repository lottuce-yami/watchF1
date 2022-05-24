using System.Data;
using F1Project.Data.Database.Types;

namespace F1Project.Data.Database.Services;

public abstract class Service<T> where T : DatabaseType, new()
{
    protected string Table => typeof(T).Name + 's';
    
    private static T Deserialize(DataRow row)
    {
        var obj = new T();
        foreach (var property in obj.GetType().GetProperties())
        {
            var column = row[property.Name].ToString();
            object value;

            if (property.PropertyType == typeof(int))
            {
                value = int.Parse(column ?? "0");
            }
            else if (property.PropertyType == typeof(bool))
            {
                value = int.Parse(column ?? "0") > 0;
            }
            else
            {
                value = column ?? "";
            }
            
            property.SetValue(obj, value);
        }

        return obj;
    }

    public int Add(T added)
    {
        var properties = added.GetType().GetProperties();
        var query = $"INSERT INTO {Table} VALUES({string.Join(", ", properties.Select(p => '@' + p.Name))})";
        var args = properties.ToDictionary(p => '@' + p.Name, p => p.GetValue(this));

        return Database.Write(query, args);
    }
    
    public T Get(string id)
    {
        var query = $"SELECT * FROM {Table} WHERE Id = @id";
        var args = new Dictionary<string, object?>
        {
            {"@id", id}
        };
        var data = Database.Read(query, args);

        return Deserialize(data.Rows[0]);
    }

    public List<T> Get()
    {
        var query = $"SELECT * FROM {Table}";
        var args = new Dictionary<string, object?>();
        var data = Database.Read(query, args);
        var objects = (from DataRow row in data.Rows select Deserialize(row)).ToList();
        
        return objects;
    }

    public int Edit(T edited)
    {
        var typeProperties = edited.GetType().GetProperties();
        var properties = string.Join(
            ", ", typeProperties.Skip(1).Select(p => p.Name + " = @" + p.Name));
        var args = typeProperties.ToDictionary(p => '@' + p.Name, p => p.GetValue(this));
        var query = $"UPDATE {Table} SET {properties} WHERE Id = @id";
        
        return Database.Write(query, args);
    }

    public int Delete(string id)
    {
        var query = $"DELETE FROM {Table} WHERE Id = @id";
        var args = new Dictionary<string, object?>
        {
            {"@id", id}
        };

        return Database.Write(query, args);
    }
}