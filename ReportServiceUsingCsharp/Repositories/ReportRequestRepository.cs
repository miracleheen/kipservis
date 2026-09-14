using Microsoft.EntityFrameworkCore;
using ReportServiceUsingCsharp.Data;
using ReportServiceUsingCsharp.Entities;

namespace ReportServiceUsingCsharp.Repositories;

public class ReportRequestRepository : IReportRequestRepository
{
    private readonly ReportDbContext _dbContext;

    public ReportRequestRepository(ReportDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReportRequest?> FindByIdAsync(Guid id)
    {
        return await _dbContext.ReportRequests.FindAsync(id);
    }

    public async Task SaveAsync(ReportRequest reportRequest)
    {
        if (_dbContext.Entry(reportRequest).State == EntityState.Detached)
        {
            _dbContext.ReportRequests.Add(reportRequest);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ReportRequest>> FindByCompletedAtIsNullAsync()
    {
        return await _dbContext.ReportRequests
            .Where(r => r.CompletedAt == null)
            .ToListAsync();
    }
}