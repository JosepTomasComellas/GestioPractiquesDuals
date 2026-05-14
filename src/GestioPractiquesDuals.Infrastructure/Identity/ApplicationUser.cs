using Microsoft.AspNetCore.Identity;

namespace GestioPractiquesDuals.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string DisplayName { get; set; } = string.Empty;
    public Guid? TeacherId { get; set; }
    public Guid? StudentId { get; set; }
}
