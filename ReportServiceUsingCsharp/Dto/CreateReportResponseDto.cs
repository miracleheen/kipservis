using System.Text.Json.Serialization;

namespace ReportServiceUsingCsharp.Dto;

public class CreateReportResponseDto
{
    [JsonPropertyName("query")]
    public Guid Query { get; }

    public CreateReportResponseDto(Guid query)
    {
        Query = query;
    }
}