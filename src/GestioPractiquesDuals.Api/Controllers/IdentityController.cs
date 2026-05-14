using GestioPractiquesDuals.Application.Security;
using Microsoft.AspNetCore.Mvc;

namespace GestioPractiquesDuals.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class IdentityController(IIdentityOverviewService identityOverviewService) : ControllerBase
{
    [HttpGet("overview")]
    public async Task<ActionResult<UserAccessSummaryDto>> GetOverview(CancellationToken cancellationToken)
    {
        var result = await identityOverviewService.GetAsync(cancellationToken);
        return Ok(result);
    }
}
