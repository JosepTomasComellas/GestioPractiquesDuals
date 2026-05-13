using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GestioPractiquesDuals.McpServer;

internal sealed class McpWorker(ILogger<McpWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("MCP server scaffold ready. Functional tools will be added in a later iteration.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
