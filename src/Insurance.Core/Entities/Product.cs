
namespace Insurance.Core.Entities
{
    /// <summary>
    ///     Product entity.
    /// </summary>
    public sealed class Product
    {
        /// <summary>
        ///     Product identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Product sales price.
        /// </summary>
        public float SalesPrice { get; set; } = 0f;

        /// <summary>
        ///     Product type identifier.
        /// </summary>
        public int ProductTypeId { get; set; }
    }
}
