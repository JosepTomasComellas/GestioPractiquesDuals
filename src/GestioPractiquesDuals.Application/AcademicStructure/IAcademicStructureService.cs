namespace GestioPractiquesDuals.Application.AcademicStructure;

public interface IAcademicStructureService
{
    Task<AcademicOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default);
    Task<CycleManagementOverviewDto> GetCycleManagementOverviewAsync(CancellationToken cancellationToken = default);
    Task SetClassProfileEditingAsync(Guid classGroupId, bool canEditProfile, CancellationToken cancellationToken = default);
}
