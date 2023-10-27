namespace F1Project.Models;

public class Standings
{
    public IEnumerable<Driver> DriverStandings { get; set; } = Array.Empty<Driver>();

    public IEnumerable<Constructor> ConstructorStandings { get; set; } = Array.Empty<Constructor>();
}