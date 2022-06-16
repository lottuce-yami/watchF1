using System.Text.Json.Serialization;
using F1Project.Data.Database.Services;
using F1Project.Data.Database.Types;
using Microsoft.AspNetCore.Mvc;

namespace F1Project.ApiControllers.Database;

[ApiController]
[Route("~/api/[controller]/[action]")]
public abstract class Controller<T> : ControllerBase where T : DatabaseType, new()
{
    protected string Auth => "***REMOVED***";
    protected static Service<T> Service => null!;

    public class ItemList
    {
        [JsonPropertyName("list")]
        public string[] List { get; set; } = null!;
        
        [JsonPropertyName("pages")]
        public int PagesCount { get; set; }
    }

    [HttpGet("~/api/[controller]")]
    public IActionResult Index(int page)
    {
        if (Request.Headers.Authorization != Auth) return Unauthorized();
        if (page < 0) return BadRequest();

        var itemsCount = Service.Get().Count;
        const int pageSize = 250;
        var pagesCount = Math.DivRem(itemsCount, pageSize, out var remainder);
        if (remainder > 0)
        {
            pagesCount++;
        }

        var items = new ItemList
        {
            List = Service.Get().
                Skip(pageSize * page).
                Take(pageSize).
                Select(i => i.Id).
                ToArray(),
            PagesCount = pagesCount
        };
        
        try
        {
            return new JsonResult(items);
        }
        catch
        {
            return Problem();
        }
    }

    [HttpGet("~/api/[controller]")]
    public IActionResult Index(string id)
    {
        if (Request.Headers.Authorization != Auth) return Unauthorized();

        try
        {
            return new JsonResult(Service.Get(id));
        }
        catch
        {
            return Problem();
        }
    }

    [HttpPost("")]
    public IActionResult Add([FromBody] T item)
    {
        if (Request.Headers.Authorization != Auth) return Unauthorized();

        try
        {
            Service.Add(item);
            return Ok();
        }
        catch
        {
            return Problem();
        }
    }

    [HttpPost("")]
    public IActionResult Edit([FromBody] T item)
    {
        if (Request.Headers.Authorization != Auth) return Unauthorized();

        try
        {
            Service.Edit(item);
            return Ok();
        }
        catch
        {
            return Problem();
        }
    }

    [HttpPost("")]
    public IActionResult Delete(string id)
    {
        if (Request.Headers.Authorization != Auth) return Unauthorized();

        try
        {
            Service.Delete(id);
            return Ok();
        }
        catch
        {
            return Problem();
        }
    }
}