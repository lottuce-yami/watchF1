using System.Data;
using F1Project.Data.Database.Types;

namespace F1Project.Data.Database.Services;

internal class VideoService : Service<Video>
{
    /*public readonly string DefaultPath = SettingService.Get("defaultVideosPath").Value;*/

    /*public List<Video> GetBy(SelectionOptions selectionOption, string value)
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

        return (from DataRow row in data.Rows select Deserialize(row)).ToList();
    }*/
}