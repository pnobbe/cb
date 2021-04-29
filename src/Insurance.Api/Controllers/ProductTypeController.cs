using System;
using System.Threading.Tasks;
using AutoMapper;
using Insurance.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    [Route("product_type")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IProductTypeRepository _productTypeRepository;

        /// <summary>
        ///     Create new instance.
        /// </summary>
        public ProductTypeController(
            IMapper mapper,
            IProductTypeRepository productTypeRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
        }

        // TASK 5 - FEATURE 3
        // POST: api/product_type/
        /// <summary>
        ///     Set the surcharge rate for a specified product type.
        /// </summary>
        [HttpPost("{id:int}/set_surcharge")]
        public async Task<IActionResult> SetSurcharge(int id, [FromBody] float input)
        {
            // Act.
            var productType = await _productTypeRepository.GetByIdAsync(id);

            productType.Surcharge = input;

            await _productTypeRepository.UpdateAsync(productType);

            // Return.
            return NoContent();
        }
    }
}
