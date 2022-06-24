using F1Project.Data.Database.Services;
using F1Project.Data.Database.Types;

namespace F1Project.ApiControllers.Database;

public class SettingsController : Controller<Setting>
{
    protected override Service<Setting> Service { get; } = new SettingService();
}