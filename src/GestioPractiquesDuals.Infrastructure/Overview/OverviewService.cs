using GestioPractiquesDuals.Application.Overview;
using GestioPractiquesDuals.Domain.Enums;
using GestioPractiquesDuals.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestioPractiquesDuals.Infrastructure.Overview;

internal sealed class OverviewService(DualsDbContext dbContext) : IOverviewService
{
    public async Task<OverviewDto> GetAsync(CancellationToken cancellationToken = default)
    {
        return new OverviewDto(
            await dbContext.AcademicYears.CountAsync(cancellationToken),
            await dbContext.TrainingCycles.CountAsync(cancellationToken),
            await dbContext.ClassGroups.CountAsync(cancellationToken),
            await dbContext.Students.CountAsync(cancellationToken),
            await dbContext.Teachers.CountAsync(cancellationToken),
            await dbContext.Companies.CountAsync(cancellationToken),
            await dbContext.InternshipAgreements.CountAsync(x => x.Status == AgreementStatus.Open, cancellationToken));
    }
}
