
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Core.Interfaces.Repositories
{
    /// <summary>
    ///     Repository operations interface.
    /// </summary>
    /// <typeparam name="TEntity">Derivative of base entity.</typeparam>
    /// <typeparam name="TEntityPrimaryKey">Primary key of entity.</typeparam>
    public interface IAsyncRepository<TEntity, TEntityPrimaryKey>
        where TEntity : class
        where TEntityPrimaryKey : IEquatable<TEntityPrimaryKey>, IComparable<TEntityPrimaryKey>
    {
        /// <summary>
        ///     Retrieve <typeparamref name="TEntity"/> by id.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns><typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetByIdAsync(TEntityPrimaryKey id);

        /// <summary>
        ///     Retrieve all <typeparamref name="TEntity"/>.
        /// </summary>
        /// <returns>List of <typeparamref name="TEntity"/>.</returns>
        Task<IEnumerable<TEntity>> ListAllAsync();
    }
}
