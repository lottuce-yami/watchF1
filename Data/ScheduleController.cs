using F1Project.Data.Database.Services;

namespace F1Project.Data;

public class ScheduleController
{
    public ScheduleController(ILogger<ScheduleController> logger, SettingService settings, HttpClient httpClient)
    {
        _logger = logger;
        _settings = settings;
        _httpClient = httpClient;
    }

    private readonly ILogger _logger;
    private readonly SettingService _settings;
    private readonly HttpClient _httpClient;
    
    public Task UpdateAsync()
    {
        return Task.CompletedTask;
    }
}