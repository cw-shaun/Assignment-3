using StockApi.Dto;
namespace StockApi.Dto
{
    public class StockSearchRequestDTO
    {
        public int? MinBudget { get; set; }
        public int? MaxBudget { get; set; }
        public string FuelTypes { get; set; } 
    }
    public class StockDTO
    {
        public int Id { get; set; }
        public int MakeYear { get; set; }
        public string CarName { get; set; }
        public string FormattedPrice { get; set; }  // Ex - Rs. 2.5 Lakh
        public int Km { get; set; }
        public string CarImage { get; set; }
        public string Fuel { get; set; }  // FuelType in string format
        public bool IsValueForMoney { get; set; }  // If kms < 10000 and price < 2L
    }

}