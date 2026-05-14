namespace GestioPractiquesDuals.Infrastructure.Options;

public sealed class TestModeOptions
{
    public const string SectionName = "TestMode";

    public bool Enabled { get; set; }
    public string Email { get; set; } = "test.duals@sarria.salesians.cat";
    public string DisplayName { get; set; } = "Usuari de proves";
    public string Role { get; set; } = "Administrator";
}
