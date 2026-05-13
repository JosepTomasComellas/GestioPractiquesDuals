using GestioPractiquesDuals.Domain.Common;

namespace GestioPractiquesDuals.Domain.Entities;

public sealed class ClassGroup : Entity
{
    public Guid AcademicYearId { get; set; }
    public AcademicYear? AcademicYear { get; set; }
    public Guid TrainingCycleId { get; set; }
    public TrainingCycle? TrainingCycle { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<StudentAcademicEnrollment> Enrollments { get; set; } = new List<StudentAcademicEnrollment>();
}
