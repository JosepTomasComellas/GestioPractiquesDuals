using GestioPractiquesDuals.Domain.Common;

namespace GestioPractiquesDuals.Domain.Entities;

public sealed class Company : Entity
{
    public string Name { get; set; } = string.Empty;
    public string? ContactEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<CompanyMentor> Mentors { get; set; } = new List<CompanyMentor>();
}
