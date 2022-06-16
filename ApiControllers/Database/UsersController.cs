using F1Project.Data.Database.Services;
using F1Project.Data.Database.Types;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.ApiControllers.Database;

public class UsersController : Controller<User>
{
    [HttpPost]
    public IActionResult Subscribe(string id)
    {
        if (Request.Headers.Authorization != Auth) return Unauthorized();

        try
        {
            UserService.Subscribe(id, 1);
        }
        catch
        {
            return Problem();
        }
        
        return Ok();
    }

    [HttpPost]
    public IActionResult Expire()
    {
        if (Request.Headers.Authorization != Auth) return Unauthorized();
        
        try
        {
            UserService.ExpireAllSubs();
        }
        catch
        {
            return Problem();
        }

        return Ok();
    }
}