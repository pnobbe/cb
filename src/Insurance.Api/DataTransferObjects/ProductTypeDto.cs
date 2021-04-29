
namespace Insurance.Api.DataTransferObjects
{
    /// <summary>
    ///     ProductType DTO.
    /// </summary>
    public sealed record ProductTypeDto
    {
        /// <summary>
        ///     Product identifier.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        ///     Product type name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        ///     Determines if this product type can be insured or not.
        /// </summary>
        public bool CanBeInsured { get; init; } = false;

        /// <summary>
        ///     Additional surcharge costs for this product.
        /// </summary>
        public float Surcharge { get; set; } = 0f;
    }
}
