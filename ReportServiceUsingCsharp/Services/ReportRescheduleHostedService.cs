using ReportServiceUsingCsharp.Repositories;

namespace ReportServiceUsingCsharp.Services;

public class ReportRescheduleHostedService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ReportScheduler _scheduler;
    private readonly ILogger<ReportRescheduleHostedService> _logger;

    public ReportRescheduleHostedService(IServiceScopeFactory scopeFactory, ReportScheduler scheduler,
        ILogger<ReportRescheduleHostedService> logger)
    {
        _scopeFactory = scopeFactory;
        _scheduler = scheduler;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IReportRequestRepository>();

        var unfinished = await repository.FindByCompletedAtIsNullAsync();
        foreach (var reportRequest in unfinished)
        {
            var completionAt = reportRequest.CreatedAt.AddMilliseconds(reportRequest.TimeoutMs);
            _scheduler.ScheduleCompletion(reportRequest.Id, completionAt);
        }

        _logger.LogInformation("Rescheduled {Count} unfinished report request(s) after startup", unfinished.Count);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}