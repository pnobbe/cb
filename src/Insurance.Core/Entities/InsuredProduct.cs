
namespace Insurance.Core.Entities
{
    /// <summary>
    ///     Insured product entity.
    /// </summary>
    public sealed class InsuredProduct
    {
        /// <summary>
        ///     Product identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        ///     The calculated insurance value for this product
        /// </summary>
        /// <remarks>
        ///     Property is derived and cannot be manually set.
        /// </remarks>
        public float InsuranceValue
        {
            get
            {
                var value = 0;
                if (ProductTypeHasInsurance)
                {
                    if (SalesPrice > 500 && SalesPrice < 2000)
                        value += 1000;

                    if (SalesPrice >= 2000)
                        value += 2000;

                    // TASK 1 - BUGFIX
                    if (ProductTypeName == "Laptops" || ProductTypeName == "Smartphones")
                        value += 500;

                }
                return value + ProductTypeSurcharge;
            }
        }

        /// <summary>
        ///     Product type name.
        /// </summary>
        public string ProductTypeName { get; set; }

        /// <summary>
        ///     Determines if this product type is insurable.
        /// </summary>
        public bool ProductTypeHasInsurance { get; set; } = false;

        /// <summary>
        ///     Additional surcharge costs for this product type.
        /// </summary>
        public float ProductTypeSurcharge { get; set; } = 0f;

        /// <summary>
        ///     Product sales price.
        /// </summary>
        public float SalesPrice { get; set; } = 0f;
    }
}
