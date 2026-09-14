using ReportServiceUsingCsharp.Dto;
using ReportServiceUsingCsharp.Entities;
using ReportServiceUsingCsharp.Repositories;

namespace ReportServiceUsingCsharp.Services;

public class ReportService
{
    private readonly IReportRequestRepository _reportRequestRepository;
    private readonly ReportScheduler _scheduler;
    private readonly long _defaultTimeoutMs;

    public ReportService(
        IReportRequestRepository reportRequestRepository, 
        ReportScheduler scheduler,
        IConfiguration configuration)
    {
        _reportRequestRepository = reportRequestRepository;
        _scheduler = scheduler;
        _defaultTimeoutMs = configuration.GetValue<long>("Report:Processing:TimeoutMs");
    }

    public async Task<Guid> CreateReportAsync(CreateReportRequestDto request)
    {
        var id = Guid.NewGuid();
        var createdAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

        var reportRequest = new ReportRequest(
            id,
            request.UserId!.Value,
            request.PeriodFrom!.Value,
            request.PeriodTo!.Value,
            createdAt,
            _defaultTimeoutMs);

        await _reportRequestRepository.SaveAsync(reportRequest);
        _scheduler.ScheduleCompletion(id, createdAt.AddMilliseconds(_defaultTimeoutMs));

        return id;
    }

    public async Task<ReportInfoResponseDto?> GetReportInfoAsync(Guid query)
    {
        var reportRequest = await _reportRequestRepository.FindByIdAsync(query);
        if (reportRequest is null)
        {
            return null;
        }

        var percent = CalculatePercent(reportRequest);
        ReportResultDto? result = reportRequest.ResultCountSignIn is not null
            ? new ReportResultDto(reportRequest.UserId.ToString(), reportRequest.ResultCountSignIn.Value.ToString())
            : null;

        return new ReportInfoResponseDto(reportRequest.Id, percent, result);
    }

    private static int CalculatePercent(ReportRequest reportRequest)
    {
        var now = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
        var elapsedMs = (now - reportRequest.CreatedAt).TotalMilliseconds;
        var timeoutMs = reportRequest.TimeoutMs;

        var percent = timeoutMs <= 0 ? 100 : (int)(elapsedMs * 100 / timeoutMs);
        return Math.Max(0, Math.Min(100, percent));
    }
}