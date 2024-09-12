using Xunit;
using Moq;
using Dapper;
using MySql.Data.MySqlClient;
using StockApi.Repositories;
using StockApi.Dto;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace StockApi.Tests.Repositories
{
    public class StockRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly StockRepository _repository;

        public StockRepositoryTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration
                .Setup(config => config.GetConnectionString("DefaultConnection"))
                .Returns("server=localhost;database=StocSkDb;user=root;password=root;");

            // Create StockRepository with mock configuration
            _repository = new StockRepository(_mockConfiguration.Object);
        }
    }

}
