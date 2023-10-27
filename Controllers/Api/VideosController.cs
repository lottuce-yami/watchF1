using F1Project.Data;
using F1Project.Models;

namespace F1Project.Controllers.Api;

public class VideosController : GenericController<Video>
{
    private readonly WatchF1Context _context;
    
    public VideosController(WatchF1Context context) : base(context)
    {
        _context = context;
    }
}