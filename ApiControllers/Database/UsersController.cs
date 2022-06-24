using System.Data.SQLite;
using F1Project.Data.Database.Services;
using F1Project.Data.Database.Types;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.ApiControllers.Database;

public class UsersController : Controller<User>
{
    protected override Service<User> Service { get; } = new UserService();
    private static int MaxSub => int.Parse(new SettingService().Get("maxSub").Value ?? "1");

    [HttpGet]
    public IActionResult Statistics()
    {
        if (Request.Headers.Authorization != Auth) return Unauthorized();
        
        try
        {
            using var conn = new SQLiteConnection($"Data Source={Data.Database.Database.DatabaseFile}");
            conn.Open();
        
            var subscribers = new Dictionary<int, int>();
            for (var i = 1; i <= MaxSub; i++)
            {
                var query = $"SELECT COUNT(*) FROM Users WHERE Subscribed = {i}";
                subscribers.Add(i, int.Parse(new SQLiteCommand(query, conn).ExecuteScalar().ToString() ?? "0"));
            }

            return new JsonResult(subscribers);
        }
        catch
        {
            return Problem();
        }
    }

    [HttpPost]
    public IActionResult Subscribe(string id, int quantity)
    {
        if (Request.Headers.Authorization != Auth) return Unauthorized();

        try
        {
            UserService.Subscribe(id, quantity);
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