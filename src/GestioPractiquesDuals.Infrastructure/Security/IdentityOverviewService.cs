using GestioPractiquesDuals.Application.Security;
using GestioPractiquesDuals.Shared;
using GestioPractiquesDuals.Shared.Security;

namespace GestioPractiquesDuals.Infrastructure.Security;

internal sealed class IdentityOverviewService : IIdentityOverviewService
{
    public Task<UserAccessSummaryDto> GetAsync(CancellationToken cancellationToken = default)
    {
        var result = new UserAccessSummaryDto(
            ProjectInfo.ProductName,
            "Fase 2",
            new[]
            {
                UserRole.Administrator,
                UserRole.Manager,
                UserRole.Teacher,
                UserRole.Student
            },
            "Correu electrònic de l'escola + contrasenya gestionada pel sistema",
            "Base d'identitat i rols preparada per a la següent iteració funcional.");

        return Task.FromResult(result);
    }
}
