using GestioPractiquesDuals.Shared.Security;

namespace GestioPractiquesDuals.Application.Security;

public sealed record UserAccessSummaryDto(
    string ProductName,
    string Phase,
    IReadOnlyList<UserRole> Roles,
    string AuthenticationModel,
    string Notes);
