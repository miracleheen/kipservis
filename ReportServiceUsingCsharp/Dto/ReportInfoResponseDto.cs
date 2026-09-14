using System.Text.Json.Serialization;

namespace ReportServiceUsingCsharp.Dto;

public class ReportInfoResponseDto
{
    [JsonPropertyName("query")]
    public Guid Query { get; }

    [JsonPropertyName("percent")]
    public int Percent { get; }

    [JsonPropertyName("result")]
    public ReportResultDto? Result { get; }

    public ReportInfoResponseDto(Guid query, int percent, ReportResultDto? result)
    {
        Query = query;
        Percent = percent;
        Result = result;
    }
}