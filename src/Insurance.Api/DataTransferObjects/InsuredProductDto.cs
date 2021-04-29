
namespace Insurance.Api.DataTransferObjects
{
    /// <summary>
    ///     Insured product DTO.
    /// </summary>
    public sealed record InsuredProductDto
    {
        /// <summary>
        ///     Product identifier.
        /// </summary>
        public int ProductId { get; init; }

        /// <summary>
        ///     The calculated insurance value for this product
        /// </summary>
        public float InsuranceValue { get; init; }

        /// <summary>
        ///     Product type name.
        /// </summary>
        public string ProductTypeName { get; init; }

        /// <summary>
        ///     Determines if this product type is insurable.
        /// </summary>
        public bool ProductTypeHasInsurance { get; init; }

        /// <summary>
        ///     Additional surcharge costs for this product type.
        /// </summary>
        public float ProductTypeSurcharge { get; init; } = 0f;

        /// <summary>
        ///     Product sales price.
        /// </summary>
        public float SalesPrice { get; init; } = 0f;
    }
}
