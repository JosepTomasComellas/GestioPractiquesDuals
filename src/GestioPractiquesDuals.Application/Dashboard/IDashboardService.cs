namespace GestioPractiquesDuals.Application.Dashboard;

public interface IDashboardService
{
    Task<DashboardSnapshotDto> GetTeacherPreviewAsync(CancellationToken cancellationToken = default);
}
