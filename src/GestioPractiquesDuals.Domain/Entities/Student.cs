using GestioPractiquesDuals.Domain.Common;

namespace GestioPractiquesDuals.Domain.Entities;

public sealed class Student : Entity
{
    public string SchoolEmail { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public ICollection<StudentAcademicEnrollment> Enrollments { get; set; } = new List<StudentAcademicEnrollment>();
    public ICollection<InternshipAgreement> Agreements { get; set; } = new List<InternshipAgreement>();
}
