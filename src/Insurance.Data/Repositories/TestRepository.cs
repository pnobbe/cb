
using System.Net.Http;
using System.Threading.Tasks;
using Insurance.Core.Interfaces.Repositories;

namespace Insurance.Data.Repositories
{
    /// <summary>
    ///     Test repository.
    /// </summary>
    internal class TestRepository : ITestRepository
    {
        private HttpClient _httpClient;

        public TestRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        ///     Check if backend is online.
        /// </summary>
        /// <remarks>
        ///     Explicit check on result, not all commands are submitted
        ///     to the database.
        /// </remarks>
        public async Task<bool> IsAliveAsync()
        {
            var response = await _httpClient.GetAsync("/products");
            return response.IsSuccessStatusCode;
        }
    }
}
