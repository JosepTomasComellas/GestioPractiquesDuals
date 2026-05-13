namespace GestioPractiquesDuals.Application.Overview;

public interface IOverviewService
{
    Task<OverviewDto> GetAsync(CancellationToken cancellationToken = default);
}
