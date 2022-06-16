using System.Data;
using System.Reflection;
using F1Project.Data.Database.Types;

namespace F1Project.Data.Database.Services;

public abstract class Service<T> where T : DatabaseType, new()
{
    protected static string Table => typeof(T).Name + 's';
    private static IEnumerable<PropertyInfo> TypeProperties => typeof(T).GetProperties();
    
    protected static T Deserialize(DataRow row)
    {
        var obj = new T();
        foreach (var property in TypeProperties)
        {
            var column = row[property.Name].ToString();
            object value;

            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                value = int.Parse(column ?? "0");
            }
            else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
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
        var query = $"INSERT INTO {Table} VALUES({string.Join(", ", TypeProperties.Select(p => '@' + p.Name))})";
        var args = added.GetType().GetProperties().ToDictionary(p => '@' + p.Name, p => p.GetValue(added));

        return Database.Write(query, args);
    }
    
    public List<T> Get()
    {
        var query = $"SELECT * FROM {Table}";
        var args = new Dictionary<string, object?>();
        var data = Database.Read(query, args);
        
        return (from DataRow row in data.Rows select Deserialize(row)).ToList();
    }
    
    public T Get(string id)
    {
        try
        {
            return GetBy("Id", id).Single();
        }
        catch
        {
            throw new Exception($"There is no exactly one element with the identifier {id} in the table {Table}");
        }
    }

    public List<T> GetBy(string property, object? value)
    {
        if (TypeProperties.All(prop => prop.Name != property))
        {
            throw new Exception($"Type {typeof(T)} does not have a property {property}");
        }

        var query = $"SELECT * FROM {Table} WHERE {property} = @{property}";
        var args = new Dictionary<string, object?>
        {
            {$"@{property}", value}
        };
        var data = Database.Read(query, args);

        return (from DataRow row in data.Rows select Deserialize(row)).ToList();
    }

    public List<T> GetByMany(Dictionary<string, object?> args)
    {
        var selection = string.Join(" AND ", args.Select(p => $"{p.Key} = @{p.Key}"));
        var query = $"SELECT * FROM {Table}";
        query += string.IsNullOrWhiteSpace(selection) ? "" : $" WHERE {selection}";
        var data = Database.Read(query, args);
        
        return (from DataRow row in data.Rows select Deserialize(row)).ToList();
    }

    public List<object> GetUniqueBy(string property, bool order)
    {
        var query = $"SELECT DISTINCT {property} FROM {Table}";
        query += order ? $" ORDER BY {property}" : "";
        var args = new Dictionary<string, object?>();
        var data = Database.Read(query, args);
        
        return (from DataRow row in data.Rows select row[property]).ToList();
    }

    public int Edit(T edited)
    {
        var properties = string.Join(
            ", ", TypeProperties.Skip(1).Select(p => p.Name + " = @" + p.Name));
        var args = edited.GetType().GetProperties().ToDictionary(
            p => '@' + p.Name, p => p.GetValue(edited));
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