namespace GestioPractiquesDuals.Application.AcademicStructure;

public sealed record AcademicOverviewDto(
    string ActiveAcademicYear,
    IReadOnlyList<AcademicYearDto> AcademicYears,
    IReadOnlyList<ClassGroupOverviewDto> ClassGroups);

public sealed record AcademicYearDto(Guid Id, string Code, string Name, bool IsActive);

public sealed record ClassGroupOverviewDto(
    Guid Id,
    string Code,
    string Name,
    string CycleCode,
    string CycleName,
    string TutorName,
    int StudentCount,
    int OpenAgreementCount,
    bool CanStudentsEditProfile);

public sealed record ClassProfileEditingUpdateRequest(bool CanEditProfile);
