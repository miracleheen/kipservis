using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReportServiceUsingCsharp.Dto;

public class CreateReportRequestDto
{
    [Required]
    [JsonPropertyName("user_id")]
    public Guid? UserId { get; set; }

    [Required]
    [JsonPropertyName("period_from")]
    public DateTime? PeriodFrom { get; set; }

    [Required]
    [JsonPropertyName("period_to")]
    public DateTime? PeriodTo { get; set; }
}