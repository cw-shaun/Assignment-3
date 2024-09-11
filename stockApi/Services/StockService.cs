using StockApi.Repositories;
using StockApi.Dto;
using AutoMapper;

namespace StockApi.Services{
    public class StockService
{
    private readonly StockRepository _stockRepository;
    private readonly IMapper _mapper;

    public StockService(StockRepository stockRepository, IMapper mapper)
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