using BookStore.BL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data.SqlClient;

namespace BookStore.HealthChecks
{
    public class CustomHealthCheck : IHealthCheck
    {
        private readonly IAuthorService _authorService;

        public CustomHealthCheck(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {            
                try
                {

                }
                catch (Exception e)
                {
                    return HealthCheckResult.Unhealthy(e.Message);
                }
            

            return HealthCheckResult.Healthy("Author Service is OK");
        }
    }
}
