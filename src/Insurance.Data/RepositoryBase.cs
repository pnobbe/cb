
using Insurance.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Data
{
    /// <summary>
    ///     Generic repository base.
    /// </summary>
    /// <typeparam name="TEntity">Derivative of base entity.</typeparam>
    /// <typeparam name="TEntityPrimaryKey">Primary key of entity.</typeparam>
    internal abstract class RepositoryBase<TEntity, TEntityPrimaryKey> : IAsyncRepository<TEntity, TEntityPrimaryKey>
        where TEntity : class
        where TEntityPrimaryKey : IEquatable<TEntityPrimaryKey>, IComparable<TEntityPrimaryKey>
    {

        /// <summary>
        ///     <see cref="IAsyncRepository{TEntry, TEntityPrimaryKey}.GetByIdAsync"/>
        /// </summary>
        public abstract Task<TEntity> GetByIdAsync(TEntityPrimaryKey id);

        /// <summary>
        ///     <see cref="IAsyncRepository{TEntity, TEntityPrimaryKey}.ListAllAsync"/>
        /// </summary>
        public abstract Task<IEnumerable<TEntity>> ListAllAsync();
    }
}
