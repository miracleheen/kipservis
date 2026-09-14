using System.Collections.Concurrent;

namespace ReportServiceUsingCsharp.Services;

public class ReportScheduler
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ConcurrentDictionary<Guid, Timer> _timers = new();

    public ReportScheduler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void ScheduleCompletion(Guid reportRequestId, DateTime completionAtLocal)
    {
        var delay = completionAtLocal - DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        if (delay < TimeSpan.Zero)
        {
            delay = TimeSpan.Zero; // просрочено (н-р, после рестарта) выполнить немедленно
        }

        var timer = new Timer(_ => OnTimerElapsed(reportRequestId), null, delay, Timeout.InfiniteTimeSpan);
        _timers[reportRequestId] = timer;
    }

    private async void OnTimerElapsed(Guid reportRequestId)
    {
        using var scope = _scopeFactory.CreateScope();
        var calculator = scope.ServiceProvider.GetRequiredService<ReportResultCalculator>();
        await calculator.ComputeAndStoreAsync(reportRequestId);

        if (_timers.TryRemove(reportRequestId, out var timer))
        {
            await timer.DisposeAsync();
        }
    }
}