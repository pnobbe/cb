using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Insurance.Api;
using Insurance.Api.Controllers;
using Insurance.Api.DataTransferObjects;
using Insurance.Core.Entities;
using Insurance.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceTests
    {
        private static IMapper _mapper;
        private static ILogger<InsuranceController> _logger = new Mock<ILogger<InsuranceController>>().Object;

        public InsuranceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            }));
        }

        [Fact]
        public async Task CalculateInsuranceForProduct_GivenSalesPriceBelow500_ShouldHaveNoInsuranceValue()
        {
            // Arrange
            var mockRepo = new Mock<IProductTypeRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(0))
                .ReturnsAsync(new ProductType()
                {
                    CanBeInsured = true
                });

            var controller = new InsuranceController(_mapper, _logger, mockRepo.Object);

            var input = new ProductDto
            {
                SalesPrice = 299
            };

            // Act
            var result = await controller.CalculateInsuranceForProductAsync(input);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var output = Assert.IsType<InsuredProductDto>(requestResult.Value);
            Assert.Equal(
                expected: 0,
                actual: output.InsuranceValue
            );
        }

        [Fact]
        public async Task CalculateInsuranceForProduct_GivenSalesPriceBetween500And2000Euros_ShouldAdd1000ToInsuranceValue()
        {
            // Arrange
            var mockRepo = new Mock<IProductTypeRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(0))
                .ReturnsAsync(new ProductType()
                {
                    CanBeInsured = true,
                });

            var controller = new InsuranceController(_mapper, _logger, mockRepo.Object);

            var input = new ProductDto
            {
                SalesPrice = 666
            };

            // Act
            var result = await controller.CalculateInsuranceForProductAsync(input);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var output = Assert.IsType<InsuredProductDto>(requestResult.Value);
            Assert.Equal(
                expected: 1000,
                actual: output.InsuranceValue
            );
        }

        [Fact]
        public async Task CalculateInsuranceForProduct_GivenSalesPriceOver2000Euros_ShouldAdd2000ToInsuranceValue()
        {

            // Arrange.
            var productType = new ProductType()
            {
                CanBeInsured = true,
            };

            var mockRepo = new Mock<IProductTypeRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(0))
                .ReturnsAsync(productType);

            var controller = new InsuranceController(_mapper, _logger, mockRepo.Object);

            var input = new ProductDto
            {
                SalesPrice = 666
            };

            // Act.
            var result = await controller.CalculateInsuranceForProductAsync(input);

            // Assert.
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var output = Assert.IsType<InsuredProductDto>(requestResult.Value);
            Assert.Equal(
                expected: 1000,
                actual: output.InsuranceValue
            );
        }

        /// <summary>
        ///     Test for TASK 1 - BUGFIX
        /// </summary>
        [Fact]
        public async Task CalculateInsuranceForProduct_GivenProductTypeIsLaptop_ShouldAdd500ToInsuranceValue()
        {
            // Arrange.
            var productType = new ProductType()
            {
                CanBeInsured = true,
                Name = "Laptops"
            };

            var mockRepo = new Mock<IProductTypeRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(0))
                .ReturnsAsync(productType);

            var controller = new InsuranceController(_mapper,_logger, mockRepo.Object);
            var input = new ProductDto
            {
                SalesPrice = 0f
            };

            // Act
            var result = await controller.CalculateInsuranceForProductAsync(input);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var output = Assert.IsType<InsuredProductDto>(requestResult.Value);

            Assert.Equal(
                expected: 500,
                actual: output.InsuranceValue
            );
        }

        [Fact]
        public async Task CalculateInsuranceForProduct_GivenProductTypeIsSmartphone_ShouldAdd500ToInsuranceValue()
        {
            // Arrange
            var productType = new ProductType()
            {
                CanBeInsured = true,
                Name = "Smartphones"
            };

            var mockRepo = new Mock<IProductTypeRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(0))
                .ReturnsAsync(productType);

            var controller = new InsuranceController(_mapper, _logger, mockRepo.Object);
            var input = new ProductDto
            {
                SalesPrice = 0f
            };

            // Act
            var result = await controller.CalculateInsuranceForProductAsync(input);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var output = Assert.IsType<InsuredProductDto>(requestResult.Value);

            Assert.Equal(
                expected: 500,
                actual: output.InsuranceValue
            );
        }

        /// <summary>
        ///     Test for TASK 4 - FEATURE 2
        /// </summary>
        /// <remarks>
        ///     We set the quantity of the cameras in our order to 2, because we need to ensure the extra cost is only added once.
        /// </remarks>
        [Fact]
        public async Task CalculateInsuranceForOrder_GivenOrderEntriesIncludesCamera_ShouldAdd500ToTotalInsuranceValue()
        {
            // Arrange
            var orderEntries = Enumerable.Empty<ProductType>()
                .Append(new()
                {
                    Id = 0,
                    CanBeInsured = true,
                    Name = "Digital cameras"
                });

            var mockRepo = new Mock<IProductTypeRepository>();
            mockRepo.Setup(repo => repo.ListAllAsync())
                .ReturnsAsync(orderEntries);

            var controller = new InsuranceController(_mapper, _logger, mockRepo.Object);
            var input = Enumerable.Empty<OrderEntryDto>()
                .Append(new()
                {
                    Product = new()
                    {
                        ProductTypeId = 0,
                        SalesPrice = 0f
                    },
                    Quantity = 2
            });

            // Act
            var result = await controller.CalculateInsuranceForOrderAsync(input);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var output = Assert.IsType<InsuredOrderDto>(requestResult.Value);

            Assert.Equal(
                expected: 500,
                actual: output.InsuranceValue
            );
        }

        /// <summary>
        ///     Test for TASK 5 - FEATURE 3
        /// </summary>
        [Fact]
        public async Task CalculateInsuranceForProduct_GivenProductTypeHasSurcharge_ShouldAddSurchargeToInsuranceValue()
        {
        // Arrange
            var productType = new ProductType()
            {
                CanBeInsured = true,
                Surcharge = 200f,
            };

            var mockRepo = new Mock<IProductTypeRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(0))
                .ReturnsAsync(productType);

            var controller = new InsuranceController(_mapper, _logger, mockRepo.Object);
            var input = new ProductDto
            {
                SalesPrice = 0f,
            };

            // Act
            var result = await controller.CalculateInsuranceForProductAsync(input);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var output = Assert.IsType<InsuredProductDto>(requestResult.Value);

            Assert.Equal(
                expected: 200,
                actual: output.InsuranceValue
            );
        }

    }

}
