using System.Net.Http.Json;
using GestioPractiquesDuals.Application.AcademicStructure;

namespace GestioPractiquesDuals.Web.Services;

public sealed class AcademicStructureApiClient(HttpClient httpClient)
{
    public async Task<AcademicOverviewDto> GetOverviewAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<AcademicOverviewDto>("api/academic-structure/overview", cancellationToken)
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

    private static AcademicOverviewDto EmptyFallback()
    {
        return new AcademicOverviewDto(
            "Cap curs actiu",
            Array.Empty<AcademicYearDto>(),
            Array.Empty<ClassGroupOverviewDto>());
    }

    public async Task<bool> SetClassProfileEditingAsync(Guid classGroupId, bool canEditProfile, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(
                $"api/academic-structure/class-groups/{classGroupId}/profile-editing",
                new ClassProfileEditingUpdateRequest(canEditProfile),
                cancellationToken);

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            return false;
        }
        catch (TaskCanceledException)
        {
            return false;
        }
    }

    public async Task<CycleManagementOverviewDto> GetCycleManagementOverviewAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<CycleManagementOverviewDto>("api/academic-structure/cycles", cancellationToken)
                ?? EmptyCycleFallback();
        }
        catch (HttpRequestException)
        {
            return EmptyCycleFallback();
        }
        catch (TaskCanceledException)
        {
            return EmptyCycleFallback();
        }
    }

    private static CycleManagementOverviewDto EmptyCycleFallback()
    {
        return new CycleManagementOverviewDto("Cap curs actiu", Array.Empty<CycleOverviewDto>());
    }
}
