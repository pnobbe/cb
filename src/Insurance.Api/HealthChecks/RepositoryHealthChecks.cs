using System.Threading;
using System.Threading.Tasks;
using Insurance.Core.Interfaces.Repositories;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Insurance.Api.HealthChecks
{
    /// <summary>
    ///     Check if the API backend is alive.
    /// </summary>
    public class RepositoryHealthCheck : IHealthCheck
    {
        private readonly ITestRepository _testRepository;

        /// <summary>
        ///     Create a new instance.
        /// </summary>
        public RepositoryHealthCheck(ITestRepository testRepository)
            => _testRepository = testRepository;

        /// <summary>
        ///     Runs the health check, returning the status of the component being checked.
        /// </summary>
        /// <param name="context">A context object associated with the current execution.</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken that can be used to cancel the health check.</param>
        /// <returns>Instance of <see cref="HealthCheckResult"/>.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
            => await _testRepository.IsAliveAsync()
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy();
    }
}
