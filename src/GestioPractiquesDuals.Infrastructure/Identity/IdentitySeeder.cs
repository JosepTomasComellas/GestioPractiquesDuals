using GestioPractiquesDuals.Domain.Entities;
using GestioPractiquesDuals.Infrastructure.Persistence;
using GestioPractiquesDuals.Infrastructure.Options;
using GestioPractiquesDuals.Shared.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GestioPractiquesDuals.Infrastructure.Identity;

public sealed class IdentitySeeder(
    DualsDbContext dbContext,
    RoleManager<IdentityRole<Guid>> roleManager,
    UserManager<ApplicationUser> userManager,
    IOptions<BootstrapAdminOptions> bootstrapAdminOptions,
    IOptions<SchoolOptions> schoolOptions,
    ILogger<IdentitySeeder> logger)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var bootstrapAdmin = bootstrapAdminOptions.Value;
        var school = schoolOptions.Value;
        ValidateBootstrapAdminConfiguration(bootstrapAdmin);

        foreach (var role in Enum.GetValues<UserRole>())
        {
            var roleName = role.ToRoleName();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                EnsureSuccess(roleResult, $"No s'ha pogut crear el rol {roleName}.");
            }
        }

        var teacher = await dbContext.Teachers.FirstAsync(cancellationToken);
        await EnsureUserAsync(
            email: NormalizeAdminEmail(bootstrapAdmin.Email, school.EmailDomain),
            displayName: bootstrapAdmin.DisplayName,
            password: bootstrapAdmin.Password,
            roles: [UserRole.Administrator.ToRoleName()],
            teacherId: null,
            studentId: null);

        await EnsureUserAsync(
            email: teacher.SchoolEmail,
            displayName: $"{teacher.FirstName} {teacher.LastName}",
            password: "Duals.Teacher.2026!",
            roles: teacher.IsManager
                ? [UserRole.Manager.ToRoleName(), UserRole.Teacher.ToRoleName()]
                : [UserRole.Teacher.ToRoleName()],
            teacherId: teacher.Id,
            studentId: null);

        var student = await dbContext.Students.FirstAsync(cancellationToken);
        await EnsureUserAsync(
            email: student.SchoolEmail,
            displayName: $"{student.FirstName} {student.LastName}",
            password: "Duals.Student.2026!",
            roles: [UserRole.Student.ToRoleName()],
            teacherId: null,
            studentId: student.Id);

        logger.LogInformation("Sembrat inicial d'identitat completat.");
    }

    private static void ValidateBootstrapAdminConfiguration(BootstrapAdminOptions bootstrapAdmin)
    {
        var password = bootstrapAdmin.Password ?? string.Empty;
        var errors = new List<string>();

        if (password.Length < 10)
        {
            errors.Add("ha de tenir com a mínim 10 caràcters");
        }

        if (!password.Any(char.IsUpper))
        {
            errors.Add("ha de contenir almenys una majúscula");
        }

        if (!password.Any(char.IsLower))
        {
            errors.Add("ha de contenir almenys una minúscula");
        }

        if (!password.Any(char.IsDigit))
        {
            errors.Add("ha de contenir almenys un número");
        }

        if (errors.Count == 0)
        {
            return;
        }

        throw new InvalidOperationException(
            "La variable BOOTSTRAP_ADMIN_PASSWORD del fitxer .env no compleix la política d'accés: "
            + string.Join(", ", errors) + ".");
    }

    private static string NormalizeAdminEmail(string configuredEmail, string schoolDomain)
    {
        if (string.IsNullOrWhiteSpace(configuredEmail))
        {
            return $"admin.duals@{schoolDomain}";
        }

        return configuredEmail.Trim();
    }

    private async Task EnsureUserAsync(
        string email,
        string displayName,
        string password,
        IReadOnlyCollection<string> roles,
        Guid? teacherId,
        Guid? studentId)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                DisplayName = displayName,
                TeacherId = teacherId,
                StudentId = studentId
            };

            var createResult = await userManager.CreateAsync(user, password);
            EnsureSuccess(createResult, $"No s'ha pogut crear l'usuari {email}.");
        }
        else
        {
            user.DisplayName = displayName;
            user.TeacherId = teacherId;
            user.StudentId = studentId;
            user.EmailConfirmed = true;
            user.UserName = email;
            user.Email = email;

            var updateResult = await userManager.UpdateAsync(user);
            EnsureSuccess(updateResult, $"No s'ha pogut actualitzar l'usuari {email}.");
        }

        var currentRoles = await userManager.GetRolesAsync(user);
        var rolesToAdd = roles.Except(currentRoles).ToArray();
        if (rolesToAdd.Length > 0)
        {
            var addRolesResult = await userManager.AddToRolesAsync(user, rolesToAdd);
            EnsureSuccess(addRolesResult, $"No s'han pogut afegir rols a {email}.");
        }
    }

    private static void EnsureSuccess(IdentityResult result, string message)
    {
        if (result.Succeeded)
        {
            return;
        }

        throw new InvalidOperationException($"{message} {string.Join(" | ", result.Errors.Select(x => x.Description))}");
    }
}
