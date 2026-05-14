namespace GestioPractiquesDuals.Application.Dashboard;

public sealed record DashboardSnapshotDto(
    string WelcomeMessage,
    IReadOnlyList<DashboardStatDto> Stats,
    IReadOnlyList<string> AvailableClasses,
    IReadOnlyList<string> AvailableModules,
    IReadOnlyList<ActivityCardDto> OpenActivities,
    IReadOnlyList<ActivityCardDto> ClosedActivities);

public sealed record DashboardStatDto(string Value, string Label, string Icon, string Color);

public sealed record ActivityCardDto(
    string Code,
    string Title,
    string Subtitle,
    string Description,
    IReadOnlyList<string> Chips,
    string? Highlight,
    bool IsOpen);
