using Callisto.HealthCheck.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Text.Json;

namespace Callisto.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IEndpointConventionBuilder MapHealthChecksWithWriter(this IEndpointRouteBuilder endpoints, string pattern)
        {
            return endpoints.MapHealthChecks(pattern, new()
            {
                ResponseWriter = HealthCheckResponseWriter
            });
        }

        static async Task HealthCheckResponseWriter(HttpContext context, HealthReport result)
        {
            var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
            context.Response.ContentType = "application/json; charset=utf-8";
            var response = new HealthCheckResponse
            {
                Status = result.Status.ToString(),
                HealthChecks = result.Entries.Select(x => new IndividualHealthCheckResponse
                {
                    Component = x.Key,
                    Status = x.Value.Status.ToString(),
                    Description = x.Value.Description ?? "(Empty)"
                }),
                HealthCheckDuration = result.TotalDuration,
                EnvironmentName = environment.EnvironmentName,
                Version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "(Empty)"
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}