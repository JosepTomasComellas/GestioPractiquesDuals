namespace GestioPractiquesDuals.Application.Overview;

public sealed record OverviewDto(
    int AcademicYears,
    int TrainingCycles,
    int ClassGroups,
    int Students,
    int Teachers,
    int Companies,
    int OpenAgreements);
