namespace ReportServiceUsingCsharp.Entities;

public class UserSignIn
{
    public long Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime SignedInAt { get; private set; }

    private UserSignIn()
    {
    }

    public UserSignIn(Guid userId, DateTime signedInAt)
    {
        UserId = userId;
        SignedInAt = signedInAt;
    }
}