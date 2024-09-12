using StockApi.Dto;
namespace StockApi.Repositories;

public interface IStockRepository{
    IEnumerable<Stock> GetStocks(Filters filters);
}