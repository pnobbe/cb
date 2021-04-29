
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Insurance.Core.Entities;
using Insurance.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Insurance.Data.Repositories
{
    /// <summary>
    ///     Product repository.
    /// </summary>
    internal class ProductRepository : RepositoryBase<Product, int>, IProductRepository
    {
        private HttpClient _httpClient;
        private readonly ILogger _logger;

        public ProductRepository(ILogger<ProductRepository> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>
        ///     Retrieve <see cref="Product"/> by id.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns><see cref="Product"/>.</returns>
        public override async Task<Product> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(string.Format("/products/{0:G}", id));
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Product>(responseString, new(JsonSerializerDefaults.Web));
        }

        /// <summary>
        ///     Retrieve all <see cref="Product"/>.
        /// </summary>
        /// <returns>List of <see cref="Product"/>.</returns>
        public override async Task<IEnumerable<Product>> ListAllAsync()
        {
            var response = await _httpClient.GetAsync("/products");

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(responseStream, new(JsonSerializerDefaults.Web));
        }
    }
}
