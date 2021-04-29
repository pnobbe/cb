using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Insurance.Api.DataTransferObjects;
using Insurance.Core.Entities;
using Insurance.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Insurance.Api.Controllers
{
    [Route("insurance")]
    public class InsuranceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IProductTypeRepository _productTypeRepository;

        /// <summary>
        ///     Create new instance.
        /// </summary>
        public InsuranceController(
            IMapper mapper,
            ILogger<InsuranceController> logger,
            IProductTypeRepository productTypeRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productTypeRepository = productTypeRepository ?? throw new ArgumentNullException(nameof(productTypeRepository));
        }

        // POST: api/insurance/product
        /// <summary>
        ///     Calculate insurance cost for a single product.
        /// </summary>
        [HttpPost("product")]
        public async Task<IActionResult> CalculateInsuranceForProductAsync([FromBody] ProductDto input)
        {
            // Map.
            var product = _mapper.Map<Product>(input);

            // Act.
            try
            {
                var productType = await _productTypeRepository.GetByIdAsync(product.ProductTypeId);

                InsuredProduct insuredProduct = new()
                {
                    ProductId = product.Id,
                    ProductTypeName = productType.Name,
                    ProductTypeHasInsurance = productType.CanBeInsured,
                    ProductTypeSurcharge = productType.Surcharge,
                    SalesPrice = product.SalesPrice,
                };

                // Map.
                var output = _mapper.Map<InsuredProductDto>(insuredProduct);

                // Return.
                return Ok(output);
            }
            catch (ArgumentException e)
            {
                var message = string.Format("Unable to calculate insurance, {0}", e.Message);
                // Log
                _logger.LogError(message);

                // Return.
                return NotFound(message);
            }
        }

        // TASK 3 - FEATURE 1
        // POST: api/insurance/order
        /// <summary>
        ///     Calculate total insurance cost for an order.
        /// </summary>
        [HttpPost("order")]
        public async Task<IActionResult> CalculateInsuranceForOrderAsync([FromBody] IEnumerable<OrderEntryDto> input)
        {
            // Map.
            var order = _mapper.Map<IEnumerable<OrderEntry>>(input);
            var productTypes = await _productTypeRepository.ListAllAsync();

            try
            {
                // Act.
                InsuredOrder insuredOrder = new()
                {
                    Entries = order.Select(entry =>
                    {
                        ProductType productType = productTypes.First(type => type.Id == entry.Product.ProductTypeId);

                        return new InsuredOrderEntry()
                        {
                            InsuredProduct = new()
                            {
                                ProductId = entry.Product.Id,
                                ProductTypeName = productType.Name,
                                ProductTypeHasInsurance = productType.CanBeInsured,
                                ProductTypeSurcharge = productType.Surcharge,
                                SalesPrice = entry.Product.SalesPrice,
                            },
                            Quantity = entry.Quantity,
                        };
                    })
                };

                // Map.
                var output = _mapper.Map<InsuredOrderDto>(insuredOrder);

                // Return.
                return Ok(output);
            }
            catch (InvalidOperationException)
            {
                var message = string.Format("One or more products contain invalid product type ids");

                // Log
                _logger.LogError(message);

                // Return.
                return UnprocessableEntity(message);
            }
        }
    }
}
