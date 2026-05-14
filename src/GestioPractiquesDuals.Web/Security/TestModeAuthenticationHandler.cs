using System.Security.Claims;
using System.Text.Encodings.Web;
using GestioPractiquesDuals.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace GestioPractiquesDuals.Web.Security;

public sealed class TestModeAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IOptions<TestModeOptions> testModeOptions)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string SchemeName = "TestMode";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var testMode = testModeOptions.Value;
        if (!testMode.Enabled)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, testMode.DisplayName),
            new(ClaimTypes.Email, testMode.Email),
            new(ClaimTypes.Role, testMode.Role),
            new("name", testMode.DisplayName)
        };

        var identity = new ClaimsIdentity(claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
