using System.Text.Json;
using F1Project.Models;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Data;

public class StandingsUpdater
{
    public StandingsUpdater(WatchF1Context context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    private readonly WatchF1Context _context;
    private readonly HttpClient _httpClient;

    public async Task UpdateAsync()
    {
        IEnumerable<Driver> driverStandings = null!;
        IEnumerable<Constructor> constructorStandings = null!;
        
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
                    driverStandings = MapDrivers(standingsLists);
                }
                else if (finishedTask == constructorsTask)
                {
                    constructorStandings = MapTeams(standingsLists);
                }
            }
        }


        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        await _context.DriverStandings.ExecuteDeleteAsync();
        _context.AddRange(driverStandings, constructorStandings);
        await transaction.CommitAsync();
    }

    private async Task<JsonDocument> ProcessUrlAsync(string url)
    {
        var response = _httpClient.GetStreamAsync(url);

        return await JsonDocument.ParseAsync(await response);
    }

    private IEnumerable<Driver> MapDrivers(JsonElement standingsLists)
    {
        var driverStandings = new List<Driver>();
        var driverStandingsJson = standingsLists.GetProperty("DriverStandings");
        
        foreach (var driverJson in driverStandingsJson.EnumerateArray())
        {
            var driver = new Driver
            {
                Id = driverJson.GetProperty("Driver").GetProperty("driverId").GetString()!,
                Position = short.Parse(driverJson.GetProperty("position").GetString()!),
                Name = driverJson.GetProperty("Driver").GetProperty("givenName").GetString() 
                       + ' ' 
                       + driverJson.GetProperty("Driver").GetProperty("familyName").GetString(),
                // Team = driverJson.GetProperty("Constructors")[0].GetProperty("name").GetString(),
                Points = short.Parse(driverJson.GetProperty("points").GetString()!)
            };
            
            driverStandings.Add(driver);
        }

        return driverStandings;
    }

    private IEnumerable<Constructor> MapTeams(JsonElement standingsLists)
    {
        var constructorStandings = new List<Constructor>();
        var constructorStandingsJson = standingsLists.GetProperty("ConstructorStandings");
            
        foreach (var constructorJson in constructorStandingsJson.EnumerateArray())
        {
            var constructor = new Constructor
            {
                Id = constructorJson.GetProperty("Constructor").GetProperty("constructorId").GetString()!,
                Position = short.Parse(constructorJson.GetProperty("position").GetString()!),
                Name = constructorJson.GetProperty("Constructor").GetProperty("name").GetString()!,
                Points = short.Parse(constructorJson.GetProperty("points").GetString()!)
            };
            
            constructorStandings.Add(constructor);
        }

        return constructorStandings;
    }
}