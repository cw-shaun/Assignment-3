using StockApi.Dto;
namespace StockApi.Services;

public interface IStockService{
IEnumerable<StockDTO> GetFilteredStocks(Filters filters);
}