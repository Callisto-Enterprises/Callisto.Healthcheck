namespace Callisto.HealthCheck.Models
{
    public sealed class HealthCheckResponse
    {
        public string Status { get; set; } = null!;
        public IEnumerable<IndividualHealthCheckResponse> HealthChecks { get; set; } = null!;
        public TimeSpan HealthCheckDuration { get; set; }
        public string EnvironmentName { get; set; } = null!;
        public string Version { get; internal set; } = null!;
    }
}
