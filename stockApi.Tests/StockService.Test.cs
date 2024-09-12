using Xunit;
using Moq;
using StockApi.Repositories;
using StockApi.Services;
using StockApi.Dto;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace StockApi.Tests.Services
{
    public class StockServiceTests
    {
        private readonly Mock<IStockRepository> _mockStockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StockService _stockService;

        public StockServiceTests()
        {
            _mockStockRepository = new Mock<IStockRepository>();
            _mockMapper = new Mock<IMapper>();
            _stockService = new StockService(_mockStockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetFilteredStocks_ReturnsMappedStocks_WhenStocksAreReturnedByRepository()
        {
            // Arrange
            var filters = new Filters
            {
                MinBudget = 1000,
                MaxBudget = 5000,
                FuelTypes = new List<int> { (int)FuelType.Petrol, (int)FuelType.Diesel }
            };

            var stocks = new List<Stock>
            {
                new Stock 
                { 
                    Id = 1, 
                    CarPrice = 1500, 
                    Fuel = (FuelType)1, // Updated to string
                    MakeYear = 2020,
                    CarName = "Car A",
                    Km = 9000,
                    CarImage = "imageA.jpg",
                },
                new Stock 
                { 
                    Id = 2, 
                    CarPrice = 3000, 
                    Fuel = (FuelType)1, // Updated to string
                    MakeYear = 2021,
                    CarName = "Car B",
                    Km = 15000,
                    CarImage = "imageB.jpg",
                }
            };

            var stockDtos = new List<StockDTO>
            {
                new StockDTO 
                { 
                    Id = 1, 
                    CarName = "Car A", 
                    FormattedPrice = "Rs. 1.5 Lakh",
                    Km = 9000,
                    CarImage = "imageA.jpg",
                    Fuel = "Petrol", // Updated to string
                    IsValueForMoney = true
                },
                new StockDTO 
                { 
                    Id = 2, 
                    CarName = "Car B", 
                    FormattedPrice = "Rs. 3 Lakh",
                    Km = 15000,
                    CarImage = "imageB.jpg",
                    Fuel = "Diesel", // Updated to string
                    IsValueForMoney = false
                }
            };

            _mockStockRepository
                .Setup(repo => repo.GetStocks(filters))
                .Returns(stocks);

            _mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<StockDTO>>(stocks))
                .Returns(stockDtos);

            // Act
            var result = _stockService.GetFilteredStocks(filters);

            // Assert
            Assert.Equal(stockDtos, result.ToList());
        }

        [Fact]
        public void GetFilteredStocks_ReturnsEmptyList_WhenNoStocksAreReturnedByRepository()
        {
            // Arrange
            var filters = new Filters
            {
                MinBudget = 10000,
                MaxBudget = 20000,
                FuelTypes = new List<int> { (int)FuelType.Electric }
            };

            var stocks = Enumerable.Empty<Stock>();
            var stockDtos = Enumerable.Empty<StockDTO>();

            _mockStockRepository
                .Setup(repo => repo.GetStocks(filters))
                .Returns(stocks);

            _mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<StockDTO>>(stocks))
                .Returns(stockDtos);

            // Act
            var result = _stockService.GetFilteredStocks(filters);

            // Assert
            Assert.Empty(result);
        }
    }
}
