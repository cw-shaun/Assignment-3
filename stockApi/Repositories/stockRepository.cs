using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MySql.Data.MySqlClient;
using StockApi.Dto;


namespace StockApi.Repositories
{
    public class StockRepository
    {
        private readonly string _connectionString;

        public StockRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Method to get stocks based on filters
        public IEnumerable<Stock> GetStocks(Filters filters)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                // var query="SELECT * FROM stocks ";
                // var query = @"SELECT * FROM stocks WHERE 
                //           (@MinBudget IS NULL OR CarPrice >= @MinBudget) AND 
                //           (@MaxBudget IS NULL OR CarPrice <= @MaxBudget) AND
                //           (FIND_IN_SET(Fuel, @FuelTypes) OR @FuelTypes IS NULL)";

                // var fuelTypes = filters.FuelTypes.Any() ? string.Join(",", filters.FuelTypes.Select(f => (int)f)) : null;

                // var stocks = connection.Query<Stock>(query, new
                // {
                //     MinBudget = filters.MinBudget,
                //     MaxBudget = filters.MaxBudget,
                //     FuelTypes = fuelTypes
                // });
                // var query = @"SELECT * FROM Stocks WHERE 
                //           (@MinBudget IS NULL OR CarPrice >= @MinBudget) AND 
                //           (@MaxBudget IS NULL OR CarPrice <= @MaxBudget) AND
                //           (@FuelTypes IS NULL OR Fuel IN @FuelTypes)";

                // var fuelTypes = filters.FuelTypes.Select(f => (int)f).ToList();
                // var stocks = connection.Query<Stock>(query, new
                var query = @"SELECT * FROM stocks WHERE 
                          (@MinBudget IS NULL OR CarPrice >= @MinBudget) AND 
                          (@MaxBudget IS NULL OR CarPrice <= @MaxBudget) ";
                if (filters.FuelTypes != null && filters.FuelTypes.Any())
                {
                    query += "AND Fuel IN @FuelTypes ";
                }
                var parameters = new
                {
                    MinBudget = filters.MinBudget,
                    MaxBudget = filters.MaxBudget,
                    FuelTypes = filters.FuelTypes?.Any() == true ? filters.FuelTypes : null
                };
                Console.WriteLine("World");
                Console.WriteLine(query);
                try{
                var stocks = connection.Query<Stock>(query.ToString(), parameters);
                return stocks.ToList();
                }
                catch(Exception e){
                    Console.WriteLine(e);
                    return connection.Query<Stock>(query.ToString(), parameters).ToList();
                };


            }
        }
    }
}