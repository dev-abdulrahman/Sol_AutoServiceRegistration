using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Sol_AutoServiceRegistration.CustomHealthChecks
{
    public static class HealthCheck
    {
        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHealthChecks()
                    .AddSqlServer(configuration["ConnectionStrings:DefaultConnection"], healthQuery: "select 1", name: "SQL servere", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" })
                    .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy)
                    .AddCheck<MemoryHealthCheck>($"Feedback Service Memory Check", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback Service" })
                    .AddUrlGroup(new Uri("https://localhost:7277/Home/Index"), name: "base URL", failureStatus: HealthStatus.Unhealthy)
                    .AddCheck<MyHealthCheck>("MyHealthCheck");

            //services.AddHealthChecksUI();
            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
                opt.SetApiMaxActiveRequests(1); //api requests concurrency    
                opt.AddHealthCheckEndpoint("feedback api", "/api/health"); //map health check api    

            }).AddInMemoryStorage();
        }
    }
}
