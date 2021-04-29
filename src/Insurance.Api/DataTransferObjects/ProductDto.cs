
namespace Insurance.Api.DataTransferObjects
{
    /// <summary>
    ///     Product DTO.
    /// </summary>
    public sealed record ProductDto
    {
        /// <summary>
        ///     Product identifier.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        ///     Product name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        ///     Product sales price.
        /// </summary>
        public float SalesPrice { get; init; } = 0f;

        /// <summary>
        ///     Product type identifier.
        /// </summary>
        public int ProductTypeId { get; init; }
    }
}
