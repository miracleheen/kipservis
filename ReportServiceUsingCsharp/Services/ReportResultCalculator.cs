using ReportServiceUsingCsharp.Repositories;

namespace ReportServiceUsingCsharp.Services;

public class ReportResultCalculator
{
    private readonly IReportRequestRepository _reportRequestRepository;
    private readonly IUserSignInRepository _userSignInRepository;

    public ReportResultCalculator(
        IReportRequestRepository reportRequestRepository,
        IUserSignInRepository userSignInRepository)
    {
        _reportRequestRepository = reportRequestRepository;
        _userSignInRepository = userSignInRepository;
    }

    public async Task ComputeAndStoreAsync(Guid id)
    {
        var reportRequest = await _reportRequestRepository.FindByIdAsync(id);
        if (reportRequest is null || reportRequest.ResultCountSignIn is not null)
        {
            return; // не найден, либо уже посчитано ранее (идемпотентность)
        }

        var count = await _userSignInRepository.CountByUserIdAndSignedInAtBetweenAsync(
            reportRequest.UserId, reportRequest.PeriodFrom, reportRequest.PeriodTo);

        reportRequest.ResultCountSignIn = count;
        reportRequest.CompletedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

        await _reportRequestRepository.SaveAsync(reportRequest);
    }
}