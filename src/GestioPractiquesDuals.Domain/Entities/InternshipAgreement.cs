using GestioPractiquesDuals.Domain.Common;
using GestioPractiquesDuals.Domain.Enums;

namespace GestioPractiquesDuals.Domain.Entities;

public sealed class InternshipAgreement : Entity
{
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid AcademicYearId { get; set; }
    public AcademicYear? AcademicYear { get; set; }
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public Guid? CompanyMentorId { get; set; }
    public CompanyMentor? CompanyMentor { get; set; }
    public string InternshipType { get; set; } = string.Empty;
    public AgreementStatus Status { get; set; } = AgreementStatus.Open;
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}
