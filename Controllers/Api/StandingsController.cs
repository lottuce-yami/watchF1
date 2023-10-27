using F1Project.Data;
using F1Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class StandingsController : ControllerBase
{
    private readonly WatchF1Context _context;
    
    public StandingsController(WatchF1Context context)
    {
        _context = context;
    }
    
    [HttpGet("drivers/{id}")]
    public async Task<ActionResult<Driver>> GetDriver(string id)
    {
        var driver = await _context.FindAsync<Driver>(id);

        if (driver == null) 
            return NotFound();

        return driver;
    }

    [HttpGet("constructors/{id}")]
    public async Task<ActionResult<Constructor>> GetConstructor(string id)
    {
        var constructor = await _context.FindAsync<Constructor>(id);

        if (constructor == null) 
            return NotFound();

        return constructor;
    }

    [HttpGet]
    public ActionResult<Standings> GetAll()
    {
        var standings = new Standings
        {
            DriverStandings = _context.DriverStandings
                .OrderBy(d => d.Position),
            ConstructorStandings = _context.ConstructorStandings
                .OrderBy(c => c.Position)
        };
        
        return standings;
    }
}