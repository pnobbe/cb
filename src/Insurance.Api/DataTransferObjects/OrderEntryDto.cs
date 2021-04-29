using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Api.DataTransferObjects
{
    /// <summary>
    ///     OrderEntry DTO.
    /// </summary>
    public sealed record OrderEntryDto
    {
        /// <summary>
        ///     Product DTO reference.
        /// </summary>
        public ProductDto Product { get; init; }

        /// <summary>
        ///     Amount of specified product in this entry.
        /// </summary>
        /// <remarks>
        ///     A range constraint was used to validate the quantity of a product in an order is at least 1 or higher.
        /// </remarks>
        [Range(1, Int32.MaxValue, ErrorMessage = "Quantity must be 1 or higher.")]
        public int Quantity { get; init; }
    }
}
