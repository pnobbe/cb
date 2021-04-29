using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Core.Entities
{
    /// <summary>
    ///     InsuredOrderEntry entity.
    /// </summary>
    public sealed class InsuredOrderEntry
    {
        /// <summary>
        ///     Insured product reference.
        /// </summary>
        [Required]
        public InsuredProduct InsuredProduct { get; set; }

        /// <summary>
        ///     Amount of specified product in this entry.
        /// </summary>
        /// <remarks>
        ///     A range constraint was used to validate the quantity of a product in an order is at least 1 or higher.
        /// </remarks>
        [Range(1, Int32.MaxValue, ErrorMessage = "Quantity must be 1 or higher.")]
        public int Quantity { get; set; } = 1;
    }
}
