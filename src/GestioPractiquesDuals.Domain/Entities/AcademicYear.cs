using GestioPractiquesDuals.Domain.Common;

namespace GestioPractiquesDuals.Domain.Entities;

public sealed class AcademicYear : Entity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool IsActive { get; set; }
}
