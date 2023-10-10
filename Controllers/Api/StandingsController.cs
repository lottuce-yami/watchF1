using F1Project.Data;
using Microsoft.AspNetCore.Mvc;

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
}