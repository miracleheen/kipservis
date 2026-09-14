using Microsoft.AspNetCore.Mvc;
using ReportServiceUsingCsharp.Dto;
using ReportServiceUsingCsharp.Services;

namespace ReportServiceUsingCsharp.Controllers;

[ApiController]
[Route("report")]
public class ReportController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost("user_statistics")]
    public async Task<ActionResult<CreateReportResponseDto>> CreateUserStatisticsReport(
        [FromBody] CreateReportRequestDto request)
    {
        var query = await _reportService.CreateReportAsync(request);
        return Ok(new CreateReportResponseDto(query));
    }

    [HttpGet("info")]
    public async Task<ActionResult<ReportInfoResponseDto>> GetReportInfo([FromQuery] Guid query)
    {
        var response = await _reportService.GetReportInfoAsync(query);
        if (response is null)   
        {
            return NotFound($"Report request not found: {query}");
        }
        return Ok(response);
    }
}