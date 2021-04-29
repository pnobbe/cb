
namespace Insurance.Core.Entities
{
    /// <summary>
    ///     ProductType entity.
    /// </summary>
    public sealed class ProductType
    {
        /// <summary>
        ///     Product type identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Product type name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Determines if this product type can be insured or not.
        /// </summary>
        public bool CanBeInsured { get; set; } = false;

        /// <summary>
        ///     Additional surcharge costs for this product.
        /// </summary>
        public float Surcharge { get; set; } = 0f;
    }
}
