using ReportServiceUsingCsharp.Entities;

namespace ReportServiceUsingCsharp.Repositories;

public interface IReportRequestRepository
{
    Task<ReportRequest?> FindByIdAsync(Guid id);
    Task SaveAsync(ReportRequest reportRequest);
    Task<List<ReportRequest>> FindByCompletedAtIsNullAsync();
}