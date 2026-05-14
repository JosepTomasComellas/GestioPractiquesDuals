using System.Net.Http.Json;
using GestioPractiquesDuals.Application.Dashboard;

namespace GestioPractiquesDuals.Web.Services;

public sealed class DashboardApiClient(HttpClient httpClient)
{
    public async Task<DashboardSnapshotDto> GetTeacherPreviewAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<DashboardSnapshotDto>("api/dashboard/teacher-preview", cancellationToken)
            ?? new DashboardSnapshotDto(
                "Benvingut/da",
                Array.Empty<DashboardStatDto>(),
                ["Totes les classes"],
                ["Tots els mòduls"],
                Array.Empty<ActivityCardDto>(),
                Array.Empty<ActivityCardDto>());
    }
}
