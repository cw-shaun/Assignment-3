using Microsoft.AspNetCore.Mvc;
using StockApi.Repositories;
using StockApi.Services;
using StockApi.Dto;
using AutoMapper;

namespace StockApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;
        private readonly IMapper _mapper;

        public StockController(StockService stockService, IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult SayHello()
        {
            return Ok("Hello");
        }


        [HttpGet("search")]
        public IActionResult GetStocks([FromQuery] StockSearchRequestDTO? request)
        {
            try
            {
                if (request.MinBudget.HasValue && request.MaxBudget.HasValue && request.MinBudget > request.MaxBudget)
                {
                    return BadRequest("Minimum price cannot be greater than maximum price.");
                }
                if (request.MinBudget < 0 || request.MaxBudget < 0)
                {
                    return BadRequest("Price values cannot be negative.");
                }


                var fuelTypes = request.FuelTypes?.Split(',')
                                .Select(int.Parse)
                                .ToList();

                var filters = _mapper.Map<Filters>(request);
                filters.FuelTypes = fuelTypes ?? new List<int>();
                // filters.FuelTypes = fuelTypes?.Select(f => (FuelType)f).ToList();
                // Console.WriteLine(filters.FuelTypes[0]);
                // Console.WriteLine(filters.FuelTypes[1]);
                // Console.WriteLine(filters.FuelTypes[2]);
                Console.WriteLine("Hello");
                var result = _stockService.GetFilteredStocks(filters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "Hello");
            }
        }
    }

}