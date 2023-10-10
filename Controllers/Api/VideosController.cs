using F1Project.Data;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class VideosController : ControllerBase
{
    private readonly WatchF1Context _context;
    
    public VideosController(WatchF1Context context)
    {
        _context = context;
    }
}