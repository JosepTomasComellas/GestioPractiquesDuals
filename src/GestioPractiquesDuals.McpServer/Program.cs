using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<GestioPractiquesDuals.McpServer.McpWorker>();

await builder.Build().RunAsync();
