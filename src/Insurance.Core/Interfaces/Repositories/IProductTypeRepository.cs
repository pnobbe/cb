
using System.Threading.Tasks;
using Insurance.Core.Entities;

namespace Insurance.Core.Interfaces.Repositories
{
    /// <summary>
    ///     Operations for the ProductType repository.
    /// </summary>
    public interface IProductTypeRepository : IAsyncRepository<ProductType, int>
    {
        /// <summary>
        ///     Update <typeparamref name="ProductType"/>.
        /// </summary>
        Task UpdateAsync(ProductType entity);
    }
}
