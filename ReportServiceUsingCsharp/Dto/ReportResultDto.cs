using System.Text.Json.Serialization;

namespace ReportServiceUsingCsharp.Dto;

public class ReportResultDto
{
    [JsonPropertyName("user_id")]
    public string UserId { get; }

    [JsonPropertyName("count_sign_in")]
    public string CountSignIn { get; }

    public ReportResultDto(string userId, string countSignIn)
    {
        UserId = userId;
        CountSignIn = countSignIn;
    }
}