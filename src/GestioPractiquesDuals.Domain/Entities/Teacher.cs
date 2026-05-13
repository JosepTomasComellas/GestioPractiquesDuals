using GestioPractiquesDuals.Domain.Common;

namespace GestioPractiquesDuals.Domain.Entities;

public sealed class Teacher : Entity
{
    public string SchoolEmail { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid TrainingCycleId { get; set; }
    public TrainingCycle? TrainingCycle { get; set; }
    public bool IsManager { get; set; }
}
