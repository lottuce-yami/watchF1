namespace F1Project.Data;

public class TimedHostedService : BackgroundService
{
    public TimedHostedService(ILogger<TimedHostedService> logger, StandingsController standingsController, ScheduleController scheduleController)
    {
        _logger = logger;
        _standingsController = standingsController;
        _scheduleController = scheduleController;
    }
    
    private readonly ILogger _logger;
    private readonly StandingsController _standingsController;
    private readonly ScheduleController _scheduleController;

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
        var standingsTask = _standingsController.UpdateAsync();
        var scheduleTask = _scheduleController.UpdateAsync();

        await Task.WhenAll(standingsTask, scheduleTask);
    }
}