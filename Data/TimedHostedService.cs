namespace F1Project.Data;

public class TimedHostedService : BackgroundService
{
    public TimedHostedService(ILogger<TimedHostedService> logger, StandingsUpdater standingsUpdater, ScheduleUpdater scheduleUpdater)
    {
        _logger = logger;
        _standingsUpdater = standingsUpdater;
        _scheduleUpdater = scheduleUpdater;
    }
    
    private readonly ILogger _logger;
    private readonly StandingsUpdater _standingsUpdater;
    private readonly ScheduleUpdater _scheduleUpdater;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("TimedHostedService has started working.");
        await DoWork();

        using PeriodicTimer timer = new(TimeSpan.FromSeconds(60));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await DoWork();
        }
    }

    private async Task DoWork()
    {
        var standingsTask = _standingsUpdater.UpdateAsync();
        var scheduleTask = _scheduleUpdater.UpdateAsync();

        await Task.WhenAll(standingsTask, scheduleTask);
    }
}