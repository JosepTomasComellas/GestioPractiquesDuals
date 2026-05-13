using GestioPractiquesDuals.Application.Overview;
using Microsoft.AspNetCore.Mvc;

namespace GestioPractiquesDuals.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OverviewController(IOverviewService overviewService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OverviewDto>> Get(CancellationToken cancellationToken)
    {
        var summary = await overviewService.GetAsync(cancellationToken);
        return Ok(summary);
    }
}
