using F1Project.Data;
using F1Project.Models;
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
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Video>> Get(string id)
    {
        var video = await _context.FindAsync<Video>(id);

        if (video == null) 
            return NotFound();

        return video;
    }
}