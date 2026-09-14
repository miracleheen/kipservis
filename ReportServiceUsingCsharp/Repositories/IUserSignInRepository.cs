namespace ReportServiceUsingCsharp.Repositories;

public interface IUserSignInRepository
{
    Task<int> CountByUserIdAndSignedInAtBetweenAsync(
        Guid userId, 
        DateTime periodFrom, 
        DateTime periodTo
        );
}