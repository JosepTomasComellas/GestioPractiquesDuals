using GestioPractiquesDuals.Domain.Entities;
using GestioPractiquesDuals.Infrastructure.Persistence;
using GestioPractiquesDuals.Shared.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GestioPractiquesDuals.Infrastructure.Identity;

public sealed class IdentitySeeder(
    DualsDbContext dbContext,
    RoleManager<IdentityRole<Guid>> roleManager,
    UserManager<ApplicationUser> userManager,
    ILogger<IdentitySeeder> logger)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
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
            email: "admin.duals@salesianssarria.test",
            displayName: "Administrador Duals",
            password: "Duals.Admin.2026!",
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
