using GestioPractiquesDuals.Application.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace GestioPractiquesDuals.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DashboardController(IDashboardService dashboardService) : ControllerBase
{
    [HttpGet("teacher-preview")]
    public async Task<ActionResult<DashboardSnapshotDto>> GetTeacherPreview(CancellationToken cancellationToken)
    {
        var result = await dashboardService.GetTeacherPreviewAsync(cancellationToken);
        return Ok(result);
    }
}
