using F1Project.Data;
using F1Project.Models;

namespace F1Project.Controllers.Api;

public class UsersController : GenericController<User>
{
    private readonly WatchF1Context _context;
    
    public UsersController(WatchF1Context context) : base(context)
    {
        _context = context;
    }
}