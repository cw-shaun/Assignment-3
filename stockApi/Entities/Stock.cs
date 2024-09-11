namespace StockApi.Dto;

public class Stock
{
    public int Id { get; set; }
    public int MakeYear { get; set; }
    public string CarName { get; set; }
    public decimal CarPrice { get; set; }
    public int Km { get; set; }
    public string CarImage { get; set; }
    public FuelType Fuel { get; set; }  // Using FuelType enum
}
