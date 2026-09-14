using Microsoft.EntityFrameworkCore;
using ReportServiceUsingCsharp.Data;

namespace ReportServiceUsingCsharp.Repositories;

public class UserSignInRepository : IUserSignInRepository
{
    private readonly ReportDbContext _dbContext;
    public UserSignInRepository(ReportDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CountByUserIdAndSignedInAtBetweenAsync(Guid userId, DateTime periodFrom, DateTime periodTo)
    {
        return await _dbContext.UserSignIns
            .CountAsync(s => s.UserId == userId && s.SignedInAt >= periodFrom && s.SignedInAt <= periodTo);
    }
}