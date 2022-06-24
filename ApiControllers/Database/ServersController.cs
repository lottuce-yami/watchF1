using F1Project.Data.Database.Services;
using F1Project.Data.Database.Types;

namespace F1Project.ApiControllers.Database;

public class ServersController : Controller<Server>
{
    protected override Service<Server> Service { get; } = new ServerService();
}