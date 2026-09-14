namespace ReportServiceUsingCsharp.Entities;

public class ReportRequest
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime PeriodFrom { get; private set; }
    public DateTime PeriodTo { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long TimeoutMs { get; private set; }
    public int? ResultCountSignIn { get; set; }
    public DateTime? CompletedAt { get; set; }

    private ReportRequest()
    {
    }

    public ReportRequest(Guid id, Guid userId, DateTime periodFrom, DateTime periodTo,
        DateTime createdAt, long timeoutMs)
    {
        Id = id;
        UserId = userId;
        PeriodFrom = periodFrom;
        PeriodTo = periodTo;
        CreatedAt = createdAt;
        TimeoutMs = timeoutMs;
    }
}