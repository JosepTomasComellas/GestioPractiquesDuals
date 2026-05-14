namespace GestioPractiquesDuals.Shared.Security;

public static class UserRoleExtensions
{
    public static string ToRoleName(this UserRole role) =>
        role switch
        {
            UserRole.Administrator => "Administrator",
            UserRole.Manager => "Manager",
            UserRole.Teacher => "Teacher",
            UserRole.Student => "Student",
            _ => role.ToString()
        };

    public static string ToDisplayLabel(this UserRole role) =>
        role switch
        {
            UserRole.Administrator => "Administrador",
            UserRole.Manager => "Gestor",
            UserRole.Teacher => "Professor/Tutor",
            UserRole.Student => "Alumne",
            _ => role.ToString()
        };
}
