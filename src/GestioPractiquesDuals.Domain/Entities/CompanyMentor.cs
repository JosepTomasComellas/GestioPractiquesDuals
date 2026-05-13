using GestioPractiquesDuals.Domain.Common;

namespace GestioPractiquesDuals.Domain.Entities;

public sealed class CompanyMentor : Entity
{
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
}
