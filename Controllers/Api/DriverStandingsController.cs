using F1Project.Data;
using F1Project.Models;

namespace F1Project.Controllers.Api;

public class DriverStandingsController : GenericController<DriverStanding>
{
    private readonly WatchF1Context _context;
    
    public DriverStandingsController(WatchF1Context context) : base(context)
    {
        _context = context;
    }
}