using GestioPractiquesDuals.Application.AcademicStructure;
using Microsoft.AspNetCore.Mvc;

namespace GestioPractiquesDuals.Api.Controllers;

[ApiController]
[Route("api/academic-structure")]
public sealed class AcademicStructureController(IAcademicStructureService academicStructureService) : ControllerBase
{
    [HttpGet("overview")]
    public async Task<ActionResult<AcademicOverviewDto>> GetOverview(CancellationToken cancellationToken)
    {
        var result = await academicStructureService.GetOverviewAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("cycles")]
    public async Task<ActionResult<CycleManagementOverviewDto>> GetCycleManagementOverview(CancellationToken cancellationToken)
    {
        var result = await academicStructureService.GetCycleManagementOverviewAsync(cancellationToken);
        return Ok(result);
    }

    [HttpPost("class-groups/{classGroupId:guid}/profile-editing")]
    public async Task<IActionResult> SetClassProfileEditing(
        Guid classGroupId,
        [FromBody] ClassProfileEditingUpdateRequest request,
        CancellationToken cancellationToken)
    {
        await academicStructureService.SetClassProfileEditingAsync(classGroupId, request.CanEditProfile, cancellationToken);
        return NoContent();
    }
}
