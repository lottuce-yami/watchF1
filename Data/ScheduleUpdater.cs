namespace F1Project.Data;

public class ScheduleUpdater
{
    public ScheduleUpdater(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private readonly HttpClient _httpClient;
    
    public Task UpdateAsync()
    {
        return Task.CompletedTask;
    }
}