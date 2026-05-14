using GestioPractiquesDuals.Application.Dashboard;
using GestioPractiquesDuals.Domain.Enums;
using GestioPractiquesDuals.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestioPractiquesDuals.Infrastructure.Dashboard;

internal sealed class DashboardService(DualsDbContext dbContext) : IDashboardService
{
    public async Task<DashboardSnapshotDto> GetTeacherPreviewAsync(CancellationToken cancellationToken = default)
    {
        var classNames = await dbContext.ClassGroups
            .OrderBy(x => x.Name)
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        var openAgreements = await dbContext.InternshipAgreements
            .CountAsync(x => x.Status == AgreementStatus.Open, cancellationToken);

        var studentCount = await dbContext.Students.CountAsync(cancellationToken);
        var teacherCount = await dbContext.Teachers.CountAsync(cancellationToken);
        var cycleCount = await dbContext.TrainingCycles.CountAsync(cancellationToken);
        var companyCount = await dbContext.Companies.CountAsync(cancellationToken);

        var stats = new List<DashboardStatDto>
        {
            new(classNames.Count.ToString(), "Classes", "book", "#243452"),
            new(cycleCount.ToString(), "Cicles", "menu_book", "#243452"),
            new(studentCount.ToString(), "Alumnes", "groups", "#3b82f6"),
            new(companyCount.ToString(), "Empreses", "badge", "#d61121"),
            new(openAgreements.ToString(), "Convenis oberts", "lock_open", "#42c96d"),
            new(teacherCount.ToString(), "Tutors", "group_work", "#f59e0b")
        };

        var openActivities = new List<ActivityCardDto>
        {
            new(
                "CV-001",
                "Enviament de CV",
                "S1SX · Administració de sistemes · Tutor ASIX",
                "Seguiment inicial del procés d'enviament de currículums per a empreses actives.",
                ["1 grup", "1 alumne"],
                "1 / 1 revisat",
                true)
        };

        var closedActivities = new List<ActivityCardDto>
        {
            new("EMP-001", "Contacte inicial", "Empresa Demo", "Primera ronda de contacte completada.", Array.Empty<string>(), null, false),
            new("CONV-001", "Conveni tancat", "Empresa Demo", "Conveni de prova completat.", Array.Empty<string>(), null, false),
            new("REU-001", "Reunió interna", "Tutories", "Planificació d'obertura de formularis.", Array.Empty<string>(), null, false),
            new("DOC-001", "Documentació rebuda", "Alumnat", "CVs rebuts i revisats.", Array.Empty<string>(), null, false)
        };

        return new DashboardSnapshotDto(
            "Benvingut/da, Josep Tomàs Comellas",
            stats,
            ["Totes les classes", .. classNames],
            ["Tots els mòduls", "M01 · Sistemes", "M02 · Xarxes", "M03 · Projecte"],
            openActivities,
            closedActivities);
    }
}
