using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using F1Project.Data.Database.Services;
using F1Project.Data.Database.Types;

namespace F1Project.Data;

public class StandingsController
{
    public StandingsController(ILogger<StandingsController> logger, IConfiguration configuration, SettingService settings, HttpClient httpClient)
    {
        _logger = logger;
        _configuration = configuration;
        _settings = settings;
        _httpClient = httpClient;
    }

    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly SettingService _settings;
    private readonly HttpClient _httpClient;

    public async Task UpdateAsync()
    {
        var standings = new Standings();

        var driversTask =
            ProcessUrlAsync("https://ergast.com/api/f1/current/driverStandings.json");
        var constructorsTask =
            ProcessUrlAsync("https://ergast.com/api/f1/current/constructorStandings.json");

        var apiTasks = new List<Task<JsonDocument>> { driversTask, constructorsTask };
        while (apiTasks.Any())
        {
            var finishedTask = await Task.WhenAny(apiTasks);
            apiTasks.Remove(finishedTask);
            
            var document = await finishedTask;
            using (document)
            {
                var standingsLists = document.RootElement
                    .GetProperty("MRData")
                    .GetProperty("StandingsTable")
                    .GetProperty("StandingsLists")[0];
                
                if (finishedTask == driversTask)
                {
                    standings.Drivers = MapDrivers(standingsLists).ToArray();
                }
                else if (finishedTask == constructorsTask)
                {
                    standings.Teams = MapTeams(standingsLists).ToArray();
                }
            }
        }

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
        };
        
        _settings.Edit(new Setting 
        { 
            Id = "standings", 
            Value = JsonSerializer.Serialize(standings, serializerOptions)
        });
    }

    private async Task<JsonDocument> ProcessUrlAsync(string url)
    {
        var response = _httpClient.GetStreamAsync(url);

        return await JsonDocument.ParseAsync(await response);
    }

    private List<Standings.DriverModel> MapDrivers(JsonElement standingsLists)
    {
        var driverStandings = new List<Standings.DriverModel>();
        var driverStandingsJson = standingsLists.GetProperty("DriverStandings");
        
        foreach (var driverJson in driverStandingsJson.EnumerateArray())
        {
            var driver = new Standings.DriverModel();

            var driverNationality = driverJson.GetProperty("Driver").GetProperty("nationality").GetString();
            var driverEnglishName = 
                driverJson.GetProperty("Driver").GetProperty("givenName").GetString() 
                + ' ' 
                + driverJson.GetProperty("Driver").GetProperty("familyName").GetString();

            driver.Driver = _configuration[$"names:{driverEnglishName}"];
            driver.Flag = _configuration[$"flags:{driverNationality}"];
            driver.Team = driverJson.GetProperty("Constructors")[0].GetProperty("name").GetString();
            driver.Pts = int.Parse(driverJson.GetProperty("points").GetString()!);

            driverStandings.Add(driver);
        }

        return driverStandings;
    }

    private List<Standings.TeamModel> MapTeams(JsonElement standingsLists)
    {
        var constructorStandings = new List<Standings.TeamModel>();
        var constructorStandingsJson = standingsLists.GetProperty("ConstructorStandings");
            
        foreach (var constructorJson in constructorStandingsJson.EnumerateArray())
        {
            var constructor = new Standings.TeamModel();

            constructor.Team = constructorJson.GetProperty("Constructor").GetProperty("name").GetString();
            constructor.Logo = _configuration[$"logos:{constructor.Team}"];
            constructor.Pts = int.Parse(constructorJson.GetProperty("points").GetString()!);

            constructorStandings.Add(constructor);
        }

        return constructorStandings;
    }
}