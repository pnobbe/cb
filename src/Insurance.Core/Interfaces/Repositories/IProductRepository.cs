
using Insurance.Core.Entities;

namespace Insurance.Core.Interfaces.Repositories
{
    /// <summary>
    ///     Operations for the Product repository.
    /// </summary>
    public interface IProductRepository : IAsyncRepository<Product, int>
    {
    }
}
