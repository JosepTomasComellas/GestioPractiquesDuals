namespace GestioPractiquesDuals.Application.Security;

public interface IIdentityOverviewService
{
    Task<UserAccessSummaryDto> GetAsync(CancellationToken cancellationToken = default);
}
