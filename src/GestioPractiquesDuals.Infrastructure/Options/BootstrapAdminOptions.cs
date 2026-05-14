namespace GestioPractiquesDuals.Infrastructure.Options;

public sealed class BootstrapAdminOptions
{
    public const string SectionName = "BootstrapAdmin";

    public string Email { get; set; } = "admin.duals@sarria.salesians.cat";
    public string DisplayName { get; set; } = "Administrador Duals";
    public string Password { get; set; } = "Duals.Admin.2026!";
}
