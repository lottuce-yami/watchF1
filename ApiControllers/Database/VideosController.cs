using F1Project.Data.Database.Services;
using F1Project.Data.Database.Types;

namespace F1Project.ApiControllers.Database;

public class VideosController : Controller<Video>
{
    protected override Service<Video> Service { get; } = new VideoService();
}