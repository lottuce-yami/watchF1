using F1Project.Data;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.Controllers.Api;

[ApiController]
[Route("~/api/[controller]/[action]")]
public abstract class GenericController<T> : ControllerBase where T : class
{
    private readonly WatchF1Context _context;
    
    protected GenericController(WatchF1Context context)
    {
        _context = context;
    }
    
    [HttpGet("~/api/[controller]")]
    public IActionResult Index()
    {
        return new JsonResult(_context.Set<T>().ToList());
    }
}