using StockApi.Repositories;
using StockApi.Dto;
using AutoMapper;

namespace StockApi.Services{
    public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public StockService(IStockRepository stockRepository, IMapper mapper)
    {
        _stockRepository = stockRepository;
        _mapper = mapper;
    }

    public IEnumerable<StockDTO> GetFilteredStocks(Filters filters)
    {
        var stocks = _stockRepository.GetStocks(filters);
        return _mapper.Map<IEnumerable<StockDTO>>(stocks);
    }
}

}