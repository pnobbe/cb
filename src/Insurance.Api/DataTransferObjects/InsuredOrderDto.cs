using System.Collections.Generic;

namespace Insurance.Api.DataTransferObjects
{
    /// <summary>
    ///     Insured order entity.
    /// </summary>
    public sealed record InsuredOrderDto
    {

        public InsuredOrderDto(float insuranceValue)
        {
            InsuranceValue = insuranceValue;
        }

        /// <summary>
        ///     The entries contained in this order.
        /// </summary>
        public IEnumerable<InsuredOrderEntryDto> Entries { get; init; }

        /// <summary>
        ///     The total insurance value of this order.
        /// </summary>
        public float InsuranceValue { get; init; } = 0f;

        /// <summary>
        ///     The total order price of this order, excluding insurance value.
        /// </summary>
        public float OrderPrice { get; init; } = 0f;
    }
}
