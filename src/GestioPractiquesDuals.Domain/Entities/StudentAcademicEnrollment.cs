using GestioPractiquesDuals.Domain.Common;

namespace GestioPractiquesDuals.Domain.Entities;

public sealed class StudentAcademicEnrollment : Entity
{
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid AcademicYearId { get; set; }
    public AcademicYear? AcademicYear { get; set; }
    public Guid TrainingCycleId { get; set; }
    public TrainingCycle? TrainingCycle { get; set; }
    public Guid ClassGroupId { get; set; }
    public ClassGroup? ClassGroup { get; set; }
    public bool CanEditProfile { get; set; }
}
