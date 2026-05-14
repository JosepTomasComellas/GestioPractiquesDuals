using System.Net.Http.Json;
using GestioPractiquesDuals.Application.Dashboard;

namespace GestioPractiquesDuals.Web.Services;

public sealed class DashboardApiClient(HttpClient httpClient)
{
    public async Task<DashboardSnapshotDto> GetTeacherPreviewAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<DashboardSnapshotDto>("api/dashboard/teacher-preview", cancellationToken)
                ?? EmptyFallback();
        }
        catch (HttpRequestException)
        {
            return EmptyFallback();
        }
        catch (TaskCanceledException)
        {
            return EmptyFallback();
        }
    }

    private static DashboardSnapshotDto EmptyFallback()
    {
        return new DashboardSnapshotDto(
            "Benvingut/da",
            Array.Empty<DashboardStatDto>(),
            ["Totes les classes"],
            ["Tots els mòduls"],
            Array.Empty<ActivityCardDto>(),
            Array.Empty<ActivityCardDto>());
    }
}
