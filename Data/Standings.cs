namespace F1Project.Data;

public class Standings
{
    public class DriverModel
    {
        public string? Driver { get; set; }
            
        public string? Flag { get; set; }
            
        public string? Team { get; set; }
            
        public int Pts { get; set; }
    }

    public class TeamModel
    {
        public string? Team { get; set; }
            
        public string? Logo { get; set; }
            
        public int Pts { get; set; }
    }

    public DriverModel[] Drivers { get; set; } = {};
        
    public TeamModel[] Teams { get; set; } = {};
}