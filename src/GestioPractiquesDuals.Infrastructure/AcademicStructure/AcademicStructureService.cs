using GestioPractiquesDuals.Application.AcademicStructure;
using GestioPractiquesDuals.Domain.Enums;
using GestioPractiquesDuals.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestioPractiquesDuals.Infrastructure.AcademicStructure;

internal sealed class AcademicStructureService(DualsDbContext dbContext) : IAcademicStructureService
{
    public async Task<AcademicOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default)
    {
        var years = await dbContext.AcademicYears
            .AsNoTracking()
            .OrderByDescending(x => x.StartDate)
            .Select(x => new AcademicYearDto(x.Id, x.Code, x.Name, x.IsActive))
            .ToListAsync(cancellationToken);

        var classGroups = await dbContext.ClassGroups
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new ClassGroupOverviewDto(
                x.Id,
                x.Code,
                x.Name,
                x.TrainingCycle != null ? x.TrainingCycle.Code : string.Empty,
                x.TrainingCycle != null ? x.TrainingCycle.Name : "Cicle pendent",
                dbContext.Teachers
                    .Where(t => t.TrainingCycleId == x.TrainingCycleId)
                    .Select(t => t.FirstName + " " + t.LastName)
                    .FirstOrDefault() ?? "Tutor pendent",
                x.Enrollments.Count,
                dbContext.InternshipAgreements.Count(a =>
                    a.Status == AgreementStatus.Open &&
                    a.Student != null &&
                    a.Student.Enrollments.Any(e => e.ClassGroupId == x.Id && e.AcademicYearId == x.AcademicYearId)),
                x.Enrollments.Any(e => e.CanEditProfile)))
            .ToListAsync(cancellationToken);

        var activeYear = years.FirstOrDefault(x => x.IsActive)?.Name
            ?? years.FirstOrDefault()?.Name
            ?? "Cap curs actiu";

        return new AcademicOverviewDto(activeYear, years, classGroups);
    }

    public async Task<CycleManagementOverviewDto> GetCycleManagementOverviewAsync(CancellationToken cancellationToken = default)
    {
        var activeYear = await dbContext.AcademicYears
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Select(x => x.Name)
            .FirstOrDefaultAsync(cancellationToken)
            ?? "Cap curs actiu";

        var cycles = await dbContext.TrainingCycles
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new CycleOverviewDto(
                x.Id,
                x.Code,
                x.Name,
                x.ClassGroups.Count,
                dbContext.Enrollments.Count(e => e.TrainingCycleId == x.Id),
                dbContext.InternshipAgreements.Count(a => a.Status == AgreementStatus.Open && a.Student != null && a.Student.Enrollments.Any(e => e.TrainingCycleId == x.Id)),
                dbContext.Teachers
                    .Where(t => t.TrainingCycleId == x.Id)
                    .OrderBy(t => t.LastName)
                    .ThenBy(t => t.FirstName)
                    .Select(t => new TeacherOverviewDto(
                        t.Id,
                        t.FirstName + " " + t.LastName,
                        t.SchoolEmail,
                        t.IsManager))
                    .ToList(),
                x.ClassGroups
                    .OrderBy(g => g.Code)
                    .Select(g => g.Code)
                    .ToList()))
            .ToListAsync(cancellationToken);

        return new CycleManagementOverviewDto(activeYear, cycles);
    }

    public async Task SetClassProfileEditingAsync(Guid classGroupId, bool canEditProfile, CancellationToken cancellationToken = default)
    {
        var enrollments = await dbContext.Enrollments
            .Where(x => x.ClassGroupId == classGroupId)
            .ToListAsync(cancellationToken);

        foreach (var enrollment in enrollments)
        {
            enrollment.CanEditProfile = canEditProfile;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
