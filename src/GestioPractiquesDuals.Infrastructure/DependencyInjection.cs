using GestioPractiquesDuals.Application.Overview;
using GestioPractiquesDuals.Application.Security;
using GestioPractiquesDuals.Infrastructure.Options;
using GestioPractiquesDuals.Infrastructure.Overview;
using GestioPractiquesDuals.Infrastructure.Persistence;
using GestioPractiquesDuals.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace GestioPractiquesDuals.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetSection(PostgresOptions.SectionName));

        var connectionString =
            configuration.GetSection(PostgresOptions.SectionName).Get<PostgresOptions>()?.ConnectionString
            ?? "Host=localhost;Port=5432;Database=duals;Username=duals;Password=duals";

        services.AddDbContext<DualsDbContext>(options => options.UseNpgsql(connectionString));

        var redisConnectionString = configuration.GetConnectionString("Redis") ?? "localhost:6379";
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConnectionString));
        services.AddScoped<IOverviewService, OverviewService>();
        services.AddScoped<IIdentityOverviewService, IdentityOverviewService>();

        return services;
    }
}
