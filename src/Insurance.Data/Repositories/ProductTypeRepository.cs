
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Insurance.Core.Entities;
using Insurance.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace Insurance.Data.Repositories
{
    /// <summary>
    ///     Product type repository.
    /// </summary>
    internal class ProductTypeRepository : RepositoryBase<ProductType, int>, IProductTypeRepository
    {
        private HttpClient _httpClient;
        private readonly ILogger _logger;

        public ProductTypeRepository(ILogger<ProductRepository> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>
        ///     Retrieve <see cref="ProductType"/> by id.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns><see cref="ProductType"/>.</returns>
        public override async Task<ProductType> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(string.Format("/product_types/{0:G}", id));
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode == false) {
                // Log.
                _logger.LogError("Unable to retrieve product type for id {0}", id);

                // Return.
                throw new ArgumentException(string.Format("Unable to retrieve product type for id {0}", id));
            }

            return JsonSerializer.Deserialize<ProductType>(responseString, new(JsonSerializerDefaults.Web));
        }

        /// <summary>
        ///     Retrieve all <see cref="ProductType"/>.
        /// </summary>
        /// <returns>List of <see cref="ProductType"/>.</returns>
        public override async Task<IEnumerable<ProductType>> ListAllAsync()
        {
            var response = await _httpClient.GetAsync("/product_types");

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<ProductType>>(responseStream, new(JsonSerializerDefaults.Web));
        }

        /// <summary>
        ///     Update <see cref="ProductType"/>.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        public async Task UpdateAsync(ProductType entity)
        {
            var content = JsonContent.Create(entity);

            // REMARK: Since there is no actual endpoint in the product API to upload
            // await _httpClient.PostAsync("/product_types", content);

            await Task.CompletedTask;
        }
    }
}
