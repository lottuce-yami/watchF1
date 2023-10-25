using F1Project.Data;
using F1Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly WatchF1Context _context;
    
    public UsersController(WatchF1Context context)
    {
        _context = context;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> Get(long id)
    {
        var user = await _context.FindAsync<User>(id);

        if (user == null) 
            return NotFound();

        return user;
    }
}