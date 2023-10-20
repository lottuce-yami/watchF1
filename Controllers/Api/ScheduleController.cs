using F1Project.Data;
using F1Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly WatchF1Context _context;
    
    public ScheduleController(WatchF1Context context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> Get(short id)
    {
        var scheduleEvent = await _context.FindAsync<Event>(id);

        if (scheduleEvent == null) 
            return NotFound();

        return scheduleEvent;
    }
}