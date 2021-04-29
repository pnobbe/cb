using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Insurance.Core.Entities
{
    /// <summary>
    ///     Insured order entity.
    /// </summary>
    public sealed class InsuredOrder
    {

        /// <summary>
        ///     The entries contained in this order.
        /// </summary>
        [Required]
        public IEnumerable<InsuredOrderEntry> Entries { get; init; }

        /// <summary>
        ///     The total insurance value of this order.
        /// </summary>
        /// <remarks>
        ///     Property is derived and cannot be manually set.
        /// </remarks>
        public float InsuranceValue
        {
            get
            {
                var value = Entries.Sum(entry => entry.InsuredProduct.InsuranceValue * entry.Quantity);

                // TASK 4 - FEATURE 2
                if (Entries.Any(entry => entry.InsuredProduct.ProductTypeName == "Digital cameras"))
                {
                    value += 500;
                }

                return value;
            }
        }

        /// <summary>
        ///     The total order price of this order, excluding insurance value.
        /// </summary>
        /// <remarks>
        ///     Property is derived and cannot be manually set.
        /// </remarks>
        public float OrderPrice
        {
            get
            {
                return Entries.Sum(entry => entry.InsuredProduct.SalesPrice * entry.Quantity);
            }
        }
    }
}
