using Xunit;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockApi.Controllers;
using StockApi.Dto;
using StockApi.Services;
using System.Collections.Generic;

namespace StockApi.Tests.Controllers
{
    public class StockControllerTests
    {
        private readonly Mock<IStockService> _mockStockService = new();
        private readonly Mock<IMapper> _mockMapper;
        private readonly StockController _controller;

        public StockControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _controller = new StockController(_mockStockService.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetStocks_ReturnsBadRequest_WhenMinBudgetIsGreaterThanMaxBudget()
        {
            // Arrange
            var request = new StockSearchRequestDTO
            {
                MinBudget = 10000,
                MaxBudget = 5000
            };

            // Act
            var result = _controller.GetStocks(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Minimum price cannot be greater than maximum price.", badRequestResult.Value);
        }

        [Fact]
        public void GetStocks_ReturnsBadRequest_WhenMinBudgetIsNegative()
        {
            // Arrange
            var request = new StockSearchRequestDTO
            {
                MinBudget = -1000,
                MaxBudget = 5000
            };

            // Act
            var result = _controller.GetStocks(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Price values cannot be negative.", badRequestResult.Value);
        }

        [Fact]
        public void GetStocks_ReturnsOk_WhenValidRequestIsProvided()
        {
            // Arrange
            var request = new StockSearchRequestDTO
            {
                MinBudget = 1000,
                MaxBudget = 5000,
                FuelTypes = "1,2"
            };

            var filters = new Filters
            {
                MinBudget = 1000,
                MaxBudget = 5000,
                FuelTypes = new List<int> { 1, 2 }
            };

            var stocks = new List<StockDTO>(); // Mocked empty result

            _mockMapper.Setup(m => m.Map<Filters>(request)).Returns(filters);
            _mockStockService.Setup(s => s.GetFilteredStocks(filters)).Returns(stocks);

            // Act
            var result = _controller.GetStocks(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(stocks, okResult.Value);
        }

        [Fact]
        public void GetStocks_Returns500_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new StockSearchRequestDTO
            {
                MinBudget = 1000,
                MaxBudget = 5000,
                FuelTypes = "1,2"
            };

            var filters = new Filters
            {
                MinBudget = 1000,
                MaxBudget = 5000,
                FuelTypes = new List<int> { 1, 2 }
            };

            _mockMapper.Setup(m => m.Map<Filters>(request)).Returns(filters);
            _mockStockService.Setup(s => s.GetFilteredStocks(filters)).Throws(new System.Exception("Database error"));

            // Act
            var result = _controller.GetStocks(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Contains("Database error", statusCodeResult.Value.ToString());
        }
    }
}
