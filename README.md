# Callisto.HealthCheck

Healthcheck utility for .NET

## Installation

Install the package from NuGet:

```bash
dotnet add package Callisto.HealthCheck
```

## Usage

### Basic

```csharp
// Program.cs
using Callisto.HealthCheck;

// Add Health checks
builder.Services.AddHealthChecks()
    .AddCheck("example", () => HealthCheckResult.Healthy("Example is healthy"));

// ...
var app = builder.Build();

// Add Health check middleware
app.UseEndpoints(endpoints =>
{
    //...
    endpoints.MapHealthChecksWithWriter("/api/health");
});
```
