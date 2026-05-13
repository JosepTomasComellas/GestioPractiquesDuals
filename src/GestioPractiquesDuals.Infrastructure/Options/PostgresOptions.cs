namespace GestioPractiquesDuals.Infrastructure.Options;

public sealed class PostgresOptions
{
    public const string SectionName = "Postgres";
    public string ConnectionString { get; set; } =
        "Host=localhost;Port=5432;Database=duals;Username=duals;Password=duals";
}
