using F1Project.Data;
using F1Project.Models;

namespace F1Project.Controllers.Api;

public class EventsController : GenericController<Event>
{
    private readonly WatchF1Context _context;
    
    public EventsController(WatchF1Context context) : base(context)
    {
        _context = context;
    }
}