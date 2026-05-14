namespace GestioPractiquesDuals.Application.AcademicStructure;

public sealed record CycleManagementOverviewDto(
    string ActiveAcademicYear,
    IReadOnlyList<CycleOverviewDto> Cycles);

public sealed record CycleOverviewDto(
    Guid Id,
    string Code,
    string Name,
    int ClassCount,
    int StudentCount,
    int OpenAgreementCount,
    IReadOnlyList<TeacherOverviewDto> Teachers,
    IReadOnlyList<string> ClassCodes);

public sealed record TeacherOverviewDto(
    Guid Id,
    string FullName,
    string Email,
    bool IsManager);
